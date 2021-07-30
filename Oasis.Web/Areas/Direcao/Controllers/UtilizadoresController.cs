using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
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
using Oasis.Web.Areas.Direcao.ViewModels;
using Oasis.Web.Extensions;
using Oasis.Web.Http;

namespace Oasis.Web.Areas.Direcao.Controllers
{

    public class UtilizadoresController : BaseDirecaoController
    {
        private readonly OasisContext _context;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public UtilizadoresController(OasisContext context, RoleManager<IdentityRole<int>> roleManager,
        UserManager<ApplicationUser> userManager, IConfiguration configuration) => (_context, _roleManager, _userManager, _configuration) = (context, roleManager, userManager, configuration);


        [HttpGet]
        public async Task<ViewResult> Index()
        => View(model: new UtilizadoresDirecaoViewModel
        {
            UtilizadoresRoles = await _userManager.Users
                                                  .AsNoTracking()
                                                  .OrderBy(u => u.PrimeiroNome)
                                                  .ThenBy(u => u.Apelido)
                                                  .ThenBy(u => u.DataCriacao)
                                                  .ToListAsync(),

        });


        [HttpGet]
        public async Task<ViewResult> Criar() => View(model: new UtilizadoresDirecaoViewModel
        {
            TiposUtilizadorDropdownList = await _roleManager.Roles
                                                      .Where(role => role.Name == TipoUtilizador.Aluno.ToString() || role.Name == TipoUtilizador.Professor.ToString())
                                                      .Select(role => new SelectListItem
                                                      {
                                                          Value = role.Id.ToString(),
                                                          Text = role.Name
                                                      }).ToListAsync()
        });


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Criar([FromForm] UtilizadoresDirecaoViewModel utilizadorDirecaoViewModel)
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

                    utilizadorDirecaoViewModel.Utilizador.Email = utilizadorDirecaoViewModel.Email;
                    utilizadorDirecaoViewModel.Utilizador.SecurityStamp = guidGerado;
                    utilizadorDirecaoViewModel.Utilizador.TemaId = 1;
                    utilizadorDirecaoViewModel.Utilizador.EscolaId = (await _context.Utilizadores
                                                                                 .AsNoTracking()
                                                                                 .Include(utilizador => utilizador.Escola)
                                                                                 .SingleOrDefaultAsync(utilizador => utilizador.Email == User.Identity.Name)).Escola.Id;

                    utilizadorDirecaoViewModel.Utilizador.UserName = utilizadorDirecaoViewModel.Email;


                    var role = await _roleManager.Roles
                                                 .SingleOrDefaultAsync(role => role.Id == utilizadorDirecaoViewModel.TiposUtilizadorId);

                    await _userManager.CreateAsync(user: utilizadorDirecaoViewModel.Utilizador, password: guidGerado);

                    await _userManager.AddToRoleAsync(
                       user: utilizadorDirecaoViewModel.Utilizador,
                       role: role.Name
                    );


                    var administradores = await _userManager.GetUsersInRoleAsync(TipoUtilizador.Administrador.ToString());

                    _context.Notificacoes.AddRange(administradores.Select(administrador => new Notificacao
                    {
                        Titulo = "Uma nova disciplina foi criada",
                        LinkDestino = "teste!",
                        ApplicationUserId = administrador.Id
                    }));


                    await databaseTransaction.CommitAsync();


                    using SmtpClient client = new();


                    var urlConfirmacaoEmail = $"{Request.Scheme}://{Request.Host}/conta/confirmacao-email/{utilizadorDirecaoViewModel.Utilizador.Email.Encrypt()}";



                    await client.EnviarEmailAsync($"{_configuration["Projeto:Nome"]} - Criação de conta", $"Viva, {utilizadorDirecaoViewModel.Utilizador.PrimeiroNome} {utilizadorDirecaoViewModel.Utilizador.Apelido}! <br/> Foi criado na nossa plataforma pedagógica. <hr/> <h4>Para realizar o seu login, por favor confirme o seu email, clicando no link providenciado e de seguida autenticar-se com a password gerada. Recomendamos a alteração da password após o 1º login! Obrigado.</h4> <hr/> <strong>Password gerada:</strong> {guidGerado}<hr/><a href='{urlConfirmacaoEmail}'>Confirmar email </a>", utilizadorDirecaoViewModel.Utilizador.Email, client.ConfiguracoesEmail(_configuration));

                    var tipoUtilizador = role.Name.ToLower();
                    return Json(new Ajax
                    {
                        Titulo = $"Sucesso ao adicionar um {tipoUtilizador}!",
                        Descricao = $"O seguinte {tipoUtilizador} foi criado com sucesso. Já o poderá adicionar a uma disciplina e/ou grupo.",
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
                    Titulo = "Ocorreu um erro na inserção de um novo utilizador.",
                    Descricao = "Pedimos desculpa pela incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Atualizar([FromForm] Disciplina disciplina) => Json(string.Empty);
    }
}