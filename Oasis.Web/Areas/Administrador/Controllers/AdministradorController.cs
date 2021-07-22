using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

namespace Oasis.Web.Areas.Administrador.Controllers {
    public class AdministradorController : BaseAdministradorController {

        private readonly OasisContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AdministradorController(OasisContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
            => (_context, _userManager, _configuration) = (context, userManager, configuration);


        [HttpGet]
        public async Task<ViewResult> Index() {
            AdministradoresViewModel utilizadoresViewModel = new() {
                Utilizadores = await _context.Utilizadores
                                                  .AsNoTracking()
                                                  .OrderBy(user => user.PrimeiroNome)
                                                  .ToListAsync(),
            };
            return View(model: utilizadoresViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AdicionarAdministrador([FromForm] AdministradoresViewModel inserirAdministrador) {
            if (!(ModelState.IsValid)) {
                return Json(new Ajax {
                    Titulo = "Os dados indicados não se encontram num formato válido!",
                    Descricao = "Por favor, introduza os dados corretamente.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }

            ApplicationUser userAdministracao = new() {
                Email = inserirAdministrador.Email,
                UserName = inserirAdministrador.Email,
                PrimeiroNome = inserirAdministrador.Administrador.PrimeiroNome,
                Apelido = inserirAdministrador.Administrador.Apelido,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IDbContextTransaction databaseTransaction = null;
            try {
                using (databaseTransaction = await _context.Database.BeginTransactionAsync()) {
                    var passwordGerada = Guid.NewGuid().ToString();
                    await _userManager.CreateAsync(userAdministracao, password: passwordGerada);
                    await _userManager.AddToRoleAsync(userAdministracao, role: TipoUtilizador.Administrador.ToString());

                    await databaseTransaction.CommitAsync();

                    using SmtpClient client = new();


                    var urlConfirmacaoEmail = $"{Request.Scheme}://{Request.Host}/conta/confirmacao-email/{userAdministracao.Email.Encrypt()}";
                    await client.EnviarEmailAsync("Foste inscrito na oasis", $"Password gerada: {passwordGerada}<hr/><a href='{urlConfirmacaoEmail}'>Confirmar email </a>", userAdministracao.Email, client.ConfiguracoesEmail(_configuration));

                    return Json(new Ajax {
                        Titulo = "Sucesso ao adicionar administrador!",
                        Descricao = "Foi enviado um email para o utilizador, de modo a que possa confirmar a sua conta.",
                        OcorreuAlgumErro = false,
                        UrlRedirecionar = string.Empty
                    });
                }
            } catch (SqlException) {
                await databaseTransaction.RollbackAsync();
                return Json(new Ajax {
                    Titulo = "Ocorreu um erro na criação de administrador!",
                    Descricao = "Pedimos desculpa pelo incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }
        }
    }
}
