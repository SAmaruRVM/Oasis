using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using Oasis.Dominio.Enums;
using Oasis.Web.Areas.Direcao.ViewModels;
using Oasis.Web.Http;

namespace Oasis.Web.Areas.Direcao.Controllers
{

    public class UtilizadoresController : BaseDirecaoController
    {
        private readonly OasisContext _context;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UtilizadoresController(OasisContext context, RoleManager<IdentityRole<int>> roleManager,
        UserManager<ApplicationUser> userManager) => (_context, _roleManager, _userManager) = (context, roleManager, userManager);


        [HttpGet]
        public async Task<ViewResult> Index()
        {
            UtilizadoresDirecaoViewModel utilizadoresViewModel = new()
            {
                UtilizadoresRoles = await _userManager.Users
                                                  .AsNoTracking()
                                                  .OrderBy(user => user.UserName)
                                                  .ToListAsync(),
            };

            return View(model: utilizadoresViewModel);
        }


        [HttpGet]
        public ViewResult Criar() => View(model: new UtilizadoresDirecaoViewModel
        {
            TiposUtilizadorDropdownList = _roleManager.Roles
                                                      .Where(role => role.Name == TipoUtilizador.Aluno.ToString() || role.Name == TipoUtilizador.Professor.ToString())
                                                      .Select(role => new SelectListItem
                                                      {
                                                          Value = role.Id.ToString(),
                                                          Text = role.Name
                                                      })
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
                    var passwordGerada = Guid.NewGuid().ToString();

                    utilizadorDirecaoViewModel.Utilizador.Email = utilizadorDirecaoViewModel.Email;
                    utilizadorDirecaoViewModel.Utilizador.SecurityStamp = Guid.NewGuid().ToString();
                    utilizadorDirecaoViewModel.Utilizador.TemaId = 3;
                    utilizadorDirecaoViewModel.Utilizador.EscolaId = (await _context.Utilizadores
                                                                                 .AsNoTracking()
                                                                                 .Include(utilizador => utilizador.Escola)
                                                                                 .SingleOrDefaultAsync(utilizador => utilizador.Email == User.Identity.Name)).Id;
                    utilizadorDirecaoViewModel.Utilizador.UserName = utilizadorDirecaoViewModel.Email;


                    var role = await _roleManager.Roles.SingleOrDefaultAsync(role => role.Id == utilizadorDirecaoViewModel.TiposUtilizadorId);

                    await _userManager.CreateAsync(utilizadorDirecaoViewModel.Utilizador, password: passwordGerada);

                    await _userManager.AddToRoleAsync(
                       user: utilizadorDirecaoViewModel.Utilizador,
                       role: role.Name
                    );

                    await databaseTransaction.CommitAsync();

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