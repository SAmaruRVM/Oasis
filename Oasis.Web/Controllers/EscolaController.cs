using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using Oasis.Dominio.Enums;
using Oasis.Web.Extensions;
using Oasis.Web.Http;
using Oasis.Web.ViewModels;

namespace Oasis.Web.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class EscolaController : Controller
    {
        private readonly OasisContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EscolaController(OasisContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
         => (_context, _userManager, _webHostEnvironment) = (context, userManager, webHostEnvironment);


        [HttpGet]
        [Route("[action]")]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var utilizadorLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);

            if (utilizadorLogado is null)
            {
                return Forbid();
            }

            IOrderedEnumerable<DisciplinaGruposAlunos> disciplinaGruposAlunos;
            if (await _userManager.IsInRoleAsync(utilizadorLogado, TipoUtilizador.Professor.ToString()))
            {
                disciplinaGruposAlunos = utilizadorLogado.GruposOndeEnsina
                                                .GroupBy(grupo => grupo.Disciplina.Nome)
                                                .Select(disciplina => new DisciplinaGruposAlunos
                                                {
                                                    NomeDisciplina = disciplina.Key,
                                                    GruposOndeEnsina = disciplina
                                                })
                                                .OrderBy(disciplina => disciplina.NomeDisciplina);
            }
            else
            {

                disciplinaGruposAlunos = utilizadorLogado.GruposOndeTemAulas
                                                        .GroupBy(grupo => grupo.Grupo.Disciplina.Nome)
                                                        .Select(disciplina => new DisciplinaGruposAlunos
                                                        {
                                                            NomeDisciplina = disciplina.Key,
                                                            GruposOndeEnsina = disciplina.Select(d => d.Grupo)
                                                        })
                                                          .OrderBy(disciplina => disciplina.NomeDisciplina);
            }



            return View(model: new EscolaViewModel
            {
                DisciplinaGruposAlunos = disciplinaGruposAlunos
            });
        }

        [HttpGet("[action]/{nomeGrupo}")]
        public async Task<IActionResult> Grupo(string nomeGrupo)
        {
            var nomeGrupoSql = string.Join(' ', nomeGrupo.Split('-'));

            var utilizadorLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);


            var grupo = await _context.Grupos
                                      .AsNoTracking()
                                      .Include(grupo => grupo.Posts)
                                      .ThenInclude(post => post.UtilizadoresQueGostaram)
                                      .Include(grupo => grupo.Posts)
                                      .ThenInclude(post => post.Comentarios)
                                      .Include(grupo => grupo.Posts)
                                      .ThenInclude(post => post.TipoPost)
                                      .Include(grupo => grupo.Posts)
                                      .ThenInclude(post => post.Criador)
                                      .Include(grupo => grupo.Disciplina)
                                      .Include(grupo => grupo.Flairs)
                                      .SingleOrDefaultAsync(grupo => grupo.Nome == nomeGrupoSql && grupo.Disciplina.Escola.Id == utilizadorLogado.Escola.Id);

            if (grupo is null)
            {
                return NotFound();
            }

            IOrderedEnumerable<DisciplinaGruposAlunos> disciplinaGruposAlunos;
            if (await _userManager.IsInRoleAsync(utilizadorLogado, TipoUtilizador.Professor.ToString()))
            {
                disciplinaGruposAlunos = utilizadorLogado.GruposOndeEnsina
                                                .GroupBy(grupo => grupo.Disciplina.Nome)
                                                .Select(disciplina => new DisciplinaGruposAlunos
                                                {
                                                    NomeDisciplina = disciplina.Key,
                                                    GruposOndeEnsina = disciplina
                                                })
                                                .OrderBy(disciplina => disciplina.NomeDisciplina);
            }
            else
            {

                disciplinaGruposAlunos = utilizadorLogado.GruposOndeTemAulas
                                                        .GroupBy(grupo => grupo.Grupo.Disciplina.Nome)
                                                        .Select(disciplina => new DisciplinaGruposAlunos
                                                        {
                                                            NomeDisciplina = disciplina.Key,
                                                            GruposOndeEnsina = disciplina.Select(d => d.Grupo)
                                                        })
                                                          .OrderBy(disciplina => disciplina.NomeDisciplina);
            }




            var tiposPostDropdownList = grupo.Flairs
                                             .OrderBy(flair => flair.Nome)
                                             .Select(flair => new SelectListItem(flair.Nome, flair.Id.ToString()));

            var postsGrupo = grupo.Posts
                                  .OrderByDescending(post => post.DataCriacao);



            return View(model: new GrupoDisciplinaViewModel
            {
                EscolaViewModel = new EscolaViewModel
                {
                    DisciplinaGruposAlunos = disciplinaGruposAlunos
                },
                Reacoes = await _context.Reacoes
                                        .AsNoTracking()
                                        .ToListAsync(),
                Grupo = grupo,
                TiposPostDropdownList = tiposPostDropdownList,
                PostsGrupo = postsGrupo
            });
        }




        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AdicionarTipoPost([FromForm] GrupoDisciplinaViewModel grupoDisciplinaViewModel)
        {
            var grupo = await _context.Grupos
                                       .FindAsync(grupoDisciplinaViewModel.TipoPostInserir.GrupoId);

            if (grupo is null)
            {
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro na inserção do tipo de post!",
                    Descricao = "Pedimos desculpa pela incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            _context.TiposPosts.Add(grupoDisciplinaViewModel.TipoPostInserir);
            await _context.SaveChangesAsync();


            return Json(new Ajax
            {
                Titulo = "O tipo de post foi inserido com sucesso!",
                Descricao = "De modo a que possa criar posts com o novo tipo inserido, terá que atualizar a página.",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }





        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> InserirConteudoPaginaEscola([FromForm] EscolaViewModel escolaViewModel)
        {
            var userLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);

            var escola = userLogado.Escola;


            if (escola.ConteudoPaginaPrincipal is null)
            {
                _context.ConteudoPaginaPrincipalEscolas.Add(escolaViewModel.PaginaPrincipal);
                await _context.SaveChangesAsync();
                escola.PaginaPrincipalId = escolaViewModel.PaginaPrincipal.Id;
            }
            else
            {
                escola.ConteudoPaginaPrincipal.ConteudoHtml = escolaViewModel.PaginaPrincipal.ConteudoHtml;
                escola.ConteudoPaginaPrincipal.DataUltimaAlteracao = DateTime.Now;
            }



            _context.Escolas.Update(escola);

            await _context.SaveChangesAsync();


            return Json(new Ajax
            {
                Titulo = "O tipo de post foi inserido com sucesso!",
                Descricao = "De modo a que possa criar posts com o novo tipo inserido, terá que atualizar a página.",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }



        [HttpGet("[action]")]
        [Authorize(Roles = "Professor")]
        public async Task<ViewResult> Grupos()
        {
            var userLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);

            var grupos = (await _context.GetLoggedInApplicationUser(User.Identity.Name))
                           .GruposOndeEnsina;

            var insercaoParticipantesGrupoViewModels = (await _userManager.GetUsersInRoleAsync(TipoUtilizador.Aluno.ToString()))
                                                                         .Where(user => user.EscolaId == userLogado.EscolaId)
                                                                         .OrderBy(user => user.Email)
                                                                         .Select(user => new InsercaoParticipantesGrupoViewModel
                                                                         {
                                                                             Email = user.Email,
                                                                             IdAluno = user.Id
                                                                         }).ToArray();

            GruposViewModel gruposViewModel = new()
            {
                Grupos = grupos,
                InsercaoParticipantesGrupoViewModels = insercaoParticipantesGrupoViewModels
            };

            return View(model: gruposViewModel);
        }


        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EditarGrupo([FromForm] GruposViewModel gruposViewModel)
        {
            var grupoParaAtualizar = await _context.Grupos.FindAsync(gruposViewModel.GrupoAlterar.Id);

            if (grupoParaAtualizar is null)
            {
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro na atualização do grupo selecionado.",
                    Descricao = "Pedimos desculpa pela incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }


            grupoParaAtualizar.Descricao = gruposViewModel.GrupoAlterar.Descricao;
            _context.Grupos.Update(grupoParaAtualizar);
            await _context.SaveChangesAsync();

            return Json(new
            {
                Ajax = new Ajax
                {
                    Titulo = "O grupo foi alterado com sucesso!",
                    Descricao = string.Empty,
                    OcorreuAlgumErro = false,
                    UrlRedirecionar = string.Empty
                },
                GrupoAtualizado = grupoParaAtualizar
            });
        }


        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AdicionarPost([FromForm] PostInserirViewModel postInserirViewModel)
        {
            if (!(ModelState.IsValid) || (await _context.TiposPosts.FindAsync(postInserirViewModel.Post.TipoPostId)) is null)
            {

                return Json(new
                {
                    Ajax = new Ajax
                    {
                        Titulo = "Os dados indicados não se encontram num formato válido!",
                        Descricao = "Por favor, introduza os dados corretamente.",
                        OcorreuAlgumErro = true,
                        UrlRedirecionar = string.Empty
                    },
                    Post = postInserirViewModel.Post
                });
            }

            var grupo = await _context.Grupos
                                      .Include(grupo => grupo.Professor)
                                      .SingleOrDefaultAsync(grupo => grupo.Id == postInserirViewModel.Post.GrupoId);



            postInserirViewModel.Post.ApplicationUserId = (await _context.GetLoggedInApplicationUser(User.Identity.Name)).Id;

          


            _context.Posts.Add(postInserirViewModel.Post);
            await _context.SaveChangesAsync();



            var link = Url.Action("Post", "Escola", new { idPost = postInserirViewModel.Post.Id }, "https", HttpContext.Request.Host.ToString(), string.Empty);

            grupo.Professor.Notificacoes.Add(new Notificacao
            {
                Titulo = $"Um novo post foi criado no grupo {grupo.Nome}",
                LinkDestino = link
            });

            await _context.SaveChangesAsync();

            return Json(new
            {
                Ajax = new Ajax
                {
                    Titulo = "O post foi inserido com sucesso!",
                    Descricao = string.Empty,
                    OcorreuAlgumErro = false,
                    UrlRedirecionar = string.Empty
                },
                Post = postInserirViewModel.Post
            });
        }





        [HttpGet("[action]/{idPost}")]
        public async Task<IActionResult> Post(int idPost)
        {
            var utilizadorLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);

            var post = await _context.Posts
                                     .AsNoTracking()
                                     .Include(post => post.Grupo)
                                     .ThenInclude(grupo => grupo.Professor)
                                     .Include(post => post.Comentarios)
                                     .ThenInclude(comentario => comentario.Utilizador)
                                     .Include(post => post.Criador)
                                     .ThenInclude(criador => criador.Escola)
                                     .Include(post => post.TipoPost)
                                     .SingleOrDefaultAsync(post => post.Id == idPost);

            if (post is null)
            {
                return NotFound();
            }

            if (post.Criador.Escola.Id != utilizadorLogado.Escola.Id)
            {
                return Forbid(); // 403
            }



            return View(model: new PostEspecificoViewModel
            {
                Post = post,
                Reacoes = await _context.Reacoes
                                                  .AsNoTracking()
                                                  .ToListAsync(),
            });
        }




        [HttpGet("[action]/{idGrupo}")]
        public async Task<JsonResult> ParticipantesGrupo([FromRoute] int idGrupo)
        {
            var userLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);

            var grupo = await _context.Grupos
                                      .AsNoTracking()
                                      .Include(grupo => grupo.Alunos)
                                      .ThenInclude(grupoAluno => grupoAluno.Aluno)
                                      .SingleOrDefaultAsync(grupo => grupo.Id == idGrupo);

            var insercaoParticipantesViewModel = (await _userManager.GetUsersInRoleAsync(TipoUtilizador.Aluno.ToString()))
                                                                    .Where(user => user.EscolaId == userLogado.EscolaId)
                                                                    .OrderBy(user => user.Email)
                                                                    .Select(user => new InsercaoParticipantesGrupoViewModel
                                                                    {
                                                                        Email = user.Email,
                                                                        IdAluno = user.Id,
                                                                        Inserir = grupo.Alunos
                                                                                            .Select(grupoAluno => grupoAluno.ApplicationUserId).Contains(user.Id)
                                                                    }).ToArray();


            return Json(data: insercaoParticipantesViewModel);
        }





        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> InserirParticipantesGrupo([FromForm] GruposViewModel gruposViewModel, [FromForm] int idGrupo)
        {
            IDbContextTransaction transaction = default;
            try
            {
                using (transaction = await _context.Database.BeginTransactionAsync())
                {
                    _context.GruposAlunos.RemoveRange(_context.GruposAlunos
                                                              .Where(grupoAluno => grupoAluno.GrupoId == idGrupo));

                    await _context.SaveChangesAsync();


                    var participantesViewModel = gruposViewModel.InsercaoParticipantesGrupoViewModels;

                    var gruposAlunosParaEliminar = await _context.GruposAlunos
                                                                 .AsNoTracking()
                                                                 .Where(grupoAluno => grupoAluno.GrupoId == idGrupo)
                                                                 .ToListAsync();

                    _context.GruposAlunos
                            .RemoveRange(entities:
                                gruposAlunosParaEliminar.Where(grupoAluno => participantesViewModel.Where(participante => !(participante.Inserir))
                                                                          .Select(participante => participante.IdAluno)
                                                                          .Contains(grupoAluno.ApplicationUserId))
                            );

                    _context.GruposAlunos
                            .AddRange(entities:
                            participantesViewModel.Where(participante => participante.Inserir)
                                                               .Select(participante => new GrupoAluno
                                                               {
                                                                   ApplicationUserId = participante.IdAluno,
                                                                   GrupoId = idGrupo
                                                               })
                     );



                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                return Json(new Ajax
                {
                    Titulo = $"As alterações relativas aos participantes do grupo selecionado foram realizadas com sucesso!",
                    Descricao = string.Empty,
                    OcorreuAlgumErro = false,
                    UrlRedirecionar = string.Empty
                });
            }
            catch (SqlException)
            {
                await transaction.RollbackAsync();
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro da nossa parte.",
                    Descricao = "Pedimos desculpa pela incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }
        }





        [HttpPost("[action]")]
        public async Task<JsonResult> MarcarPostComReacao([FromForm] int idPost, [FromForm] int? idReacao)
        {
            var userLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);


            var post = await _context.Posts
                                     .FindAsync(idPost);

            if (post is null)
            {
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro da nossa parte.",
                    Descricao = "Pedimos desculpa pela incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            Reacao reacao = default;
            var gpu = await _context.PostsGostosUtilizadores
                                                              .SingleOrDefaultAsync(pgu => pgu.ApplicationUserId == userLogado.Id && pgu.PostId == idPost);
            if (idReacao is null)
            {
                userLogado.PostsGostados.Remove(gpu);
            }
            else
            {
                reacao = await _context.Reacoes
                                       .FindAsync(idReacao.Value);




                if (reacao is null)
                {
                    return Json(new Ajax
                    {
                        Titulo = "Ocorreu um erro da nossa parte.",
                        Descricao = "Pedimos desculpa pela incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                        OcorreuAlgumErro = true,
                        UrlRedirecionar = string.Empty
                    });
                }



                if (gpu is not null)
                {
                    gpu.ReacaoId = reacao.Id;
                    _context.PostsGostosUtilizadores.Update(gpu);
                }
                else
                {
                    userLogado.PostsGostados.Add(new PostGostoUtilizador
                    {
                        PostId = post.Id,
                        ReacaoId = idReacao.Value
                    });
                }




            }

            await _context.SaveChangesAsync();

            await _context.Entry(post).Collection(post => post.Comentarios).LoadAsync();
            await _context.Entry(post).Collection(post => post.UtilizadoresQueGostaram).LoadAsync();

            return Json(new
            {
                Ajax = new Ajax
                {
                    Titulo = "O post foi reagido com sucesso!",
                    Descricao = string.Empty,
                    OcorreuAlgumErro = false,
                    UrlRedirecionar = string.Empty
                },
                Estatisticas = new
                {
                    NumeroReacoes = post.UtilizadoresQueGostaram.Count(),
                    NumeroComentarios = post.Comentarios.Count()
                },
                IconeReacao = reacao?.Icone is not null ? Path.Combine("/", reacao?.Icone) : string.Empty
            });
        }










        [HttpPost("[action]")]
        public async Task<JsonResult> GuardarPost([FromForm] int idPost)
        {
            var userLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);
            bool postGuardado = true;

            if (userLogado.PostsGuardados.Select(pg => pg.PostId).Contains(idPost))
            {
                userLogado.PostsGuardados.Remove(await _context.PostsUtilizadoresGuardados.SingleOrDefaultAsync(pug => pug.PostId == idPost && pug.ApplicationUserId == userLogado.Id));
                postGuardado = false;
            }
            else
            {
                userLogado.PostsGuardados.Add(new PostUtilizadorGuardado
                {
                    PostId = idPost
                });
            }

            await _userManager.UpdateAsync(user: userLogado);

            return Json(new Ajax
            {
                Titulo = $"O post foi {(postGuardado ? "guardado" : "desguardado")} com sucesso!",
                Descricao = string.Empty,
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }


        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> InserirComentarioPost([FromForm] string comentario, [FromForm] int idPost)
        {
            var postParaComentar = await _context.Posts
                                                 .FindAsync(idPost);

            if (postParaComentar is null)
            {
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro da nossa parte.",
                    Descricao = "Pedimos desculpa pela incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            var userLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);

            userLogado.Comentarios.Add(new ComentarioPostUtilizador
            {
                Comentario = comentario,
                PostId = idPost,
            });


            await _context.SaveChangesAsync();

            await _context.Entry(postParaComentar).Collection(post => post.UtilizadoresQueGostaram).LoadAsync();
            await _context.Entry(postParaComentar).Collection(post => post.Comentarios).LoadAsync();


            return Json(new
            {
                Ajax = new Ajax
                {
                    Titulo = $"O teu comentário foi inserido com sucesso!",
                    Descricao = string.Empty,
                    OcorreuAlgumErro = false,
                    UrlRedirecionar = string.Empty
                },
                Estatisticas = new
                {
                    NumeroReacoes = postParaComentar.UtilizadoresQueGostaram.Count(),
                    NumeroComentarios = postParaComentar.Comentarios.Count()
                },
            });
        }




        [HttpPost("[action]")]
        public async Task<JsonResult> EliminarPost([FromForm] int idPost)
        {
            var userLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);

            var postParaEliminar = await _context.Posts.SingleOrDefaultAsync(post => post.Id == idPost && post.ApplicationUserId == userLogado.Id);

            if (postParaEliminar is null)
            {
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro na eliminação do post selecionado.",
                    Descricao = "Pedimos desculpa pela incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            userLogado.PostsCriados.Remove(postParaEliminar);

            await _userManager.UpdateAsync(user: userLogado);
            return Json(new Ajax
            {
                Titulo = $"O post foi eliminado com sucesso!",
                Descricao = string.Empty,
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }



        [HttpGet("[action]/Id/{id}")]
        public async Task<JsonResult> Grupo(int id) => Json(await _context.Grupos.FindAsync(id));
    }
}