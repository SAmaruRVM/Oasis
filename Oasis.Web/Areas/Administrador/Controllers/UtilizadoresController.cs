using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Oasis.Aplicacao.Extensions;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using Oasis.Dominio.Enums;
using Oasis.Web.Areas.Administrador.ViewModels;
using Oasis.Web.Extensions;
using Oasis.Web.Http;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Administrador.Controllers
{
    public class UtilizadoresController : BaseAdministradorController
    {
        private readonly OasisContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public UtilizadoresController(OasisContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
            => (_context, _userManager, _configuration) = (context, userManager, configuration);

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            UtilizadoresAdministradorViewModel utilizadoresViewModel = new()
            {
                Utilizadores = await _context.Utilizadores
                                                  .AsNoTracking()
                                                  .OrderByDescending(user => user.DataCriacao)
                                                  .ToListAsync(),
                EscolasDropdownList = await _context.Escolas
                                                    .AsNoTracking()
                                                    .OrderBy(escola => escola.Nome)
                                                    .Select(escola => new SelectListItem { Value = escola.Id.ToString(), Text = $"{escola.Nome}, {escola.Distrito} - {escola.CodigoPostal}" }).ToListAsync()
            };
            return View(model: utilizadoresViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AdicionarMembroDirecao([FromForm] UtilizadoresAdministradorViewModel inserirMembroDirecaoViewModel)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new Ajax
                {
                    Titulo = "Os dados indicados não se encontram num formato válido!",
                    Descricao = "Por favor, introduza os dados corretamente.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            IDbContextTransaction databaseTransaction = null;
            try
            {
                using (databaseTransaction = await _context.Database.BeginTransactionAsync())
                {
                    var guidGerado = Guid.NewGuid().ToString();

                    ApplicationUser userDirecao = new()
                    {
                        Email = inserirMembroDirecaoViewModel.Email,
                        UserName = inserirMembroDirecaoViewModel.Email,
                        PrimeiroNome = inserirMembroDirecaoViewModel.MembroDirecao.PrimeiroNome,
                        Apelido = inserirMembroDirecaoViewModel.MembroDirecao.Apelido,
                        SecurityStamp = guidGerado,
                        EscolaId = inserirMembroDirecaoViewModel.IdEscola,
                        TemaId = 1
                    };

                    await _userManager.CreateAsync(userDirecao, password: guidGerado);
                    await _userManager.AddToRoleAsync(userDirecao, role: TipoUtilizador.Diretor.ToString());

                    await databaseTransaction.CommitAsync();

                    using SmtpClient client = new();

                  
                    var urlConfirmacaoEmail = $"{Request.Scheme}://{Request.Host}/conta/confirmacao-email/{userDirecao.Email.Encrypt()}";
                    await client.EnviarEmailAsync("Foste inscrito na oasis", $"Password gerada: {guidGerado}<hr/><a href='{urlConfirmacaoEmail}'>Confirmar email </a>", userDirecao.Email, client.ConfiguracoesEmail(_configuration));

                    return Json(new Ajax
                    {
                        Titulo = "Sucesso ao adicionar o utilizador e a sua respetiva associação à escola!",
                        Descricao = "Foi enviado um email para o utilizador, de modo a que possa confirmar a sua conta.",
                        OcorreuAlgumErro = false,
                        UrlRedirecionar = string.Empty
                    });
                }
            }
            catch (SqlException)
            {
                await databaseTransaction.RollbackAsync();
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro na inserção do membro da direção!",
                    Descricao = "Pedimos desculpa pelo incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> EditarUtilizador([FromForm] UtilizadoresAdministradorViewModel inserirMembroDirecaoViewModel)
        {
             if (!(ModelState.IsValid))
            {
                return Json(new Ajax
                {
                    Titulo = "Os dados indicados não se encontram num formato válido!",
                    Descricao = "Por favor, introduza os dados corretamente.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            ApplicationUser userDirecao = new()
            {
                Email = inserirMembroDirecaoViewModel.Email,
                UserName = inserirMembroDirecaoViewModel.Email,
                PrimeiroNome = inserirMembroDirecaoViewModel.MembroDirecao.PrimeiroNome,
                Apelido = inserirMembroDirecaoViewModel.MembroDirecao.Apelido,
                SecurityStamp = Guid.NewGuid().ToString(),
                EscolaId = inserirMembroDirecaoViewModel.IdEscola,
                TemaId = 1
            };

            IDbContextTransaction databaseTransaction = null;
            try
            {
                using (databaseTransaction = await _context.Database.BeginTransactionAsync())
                {
                    var passwordGerada = Guid.NewGuid().ToString();
                    await _userManager.CreateAsync(userDirecao, password: passwordGerada);
                    await _userManager.AddToRoleAsync(userDirecao, role: TipoUtilizador.Diretor.ToString());

                    await databaseTransaction.CommitAsync();

                    using SmtpClient client = new();

                  
                    var urlConfirmacaoEmail = $"{Request.Scheme}://{Request.Host}/conta/confirmacao-email/{userDirecao.Email.Encrypt()}";
                    await client.EnviarEmailAsync("Foste inscrito na oasis", $"Password gerada: {passwordGerada}<hr/><a href='{urlConfirmacaoEmail}'>Confirmar email </a>", userDirecao.Email, client.ConfiguracoesEmail(_configuration));

                    return Json(new Ajax
                    {
                        Titulo = "Sucesso ao adicionar o utilizador e a sua respetiva associação à escola!",
                        Descricao = "Foi enviado um email para o utilizador, de modo a que possa confirmar a sua conta.",
                        OcorreuAlgumErro = false,
                        UrlRedirecionar = string.Empty
                    });
                }
            }
            catch (SqlException)
            {
                await databaseTransaction.RollbackAsync();
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro na inserção do membro da direção!",
                    Descricao = "Pedimos desculpa pelo incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }
        }


       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Eliminar([FromForm] UtilizadoresAdministradorViewModel utilizadoresAdministradorViewModel)
        {
            var userParaEliminar = await _context.Utilizadores
                                                 .FindAsync(utilizadoresAdministradorViewModel.UtilizadorEliminarId);

            if (userParaEliminar is null)
            {
                return Json(new Ajax
                {
                    Titulo = "Ocorreu um erro na eliminação do utilizador!",
                    Descricao = "Pedimos desculpa pelo incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            await _userManager.DeleteAsync(userParaEliminar);

            return Json(new Ajax
            {
                Titulo = "Sucesso ao eliminar o utilizador!",
                Descricao = "O utilizador eliminado com sucesso.",
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            });
        }

        [HttpPost]
        public async Task<JsonResult> AtualizarTema([FromForm] int idTema) 
        {    
            var userLogado = await _context.GetLoggedInApplicationUser(User.Identity.Name);
            userLogado.TemaId = idTema;

            await _userManager.UpdateAsync(user: userLogado);

            return Json(new Ajax
            {
                Titulo = "O tema foi alterado com sucesso!",
                Descricao = string.Empty,
                OcorreuAlgumErro = false,
                UrlRedirecionar = string.Empty
            }); 
        }



        [HttpGet("[area]/[controller]/[action]/{id}")]
        public async Task<JsonResult> Id(int id) => Json(await _context.Utilizadores.FindAsync(id));
    }
}
