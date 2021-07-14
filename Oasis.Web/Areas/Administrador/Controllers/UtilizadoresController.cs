﻿using Microsoft.AspNetCore.Identity;
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
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Oasis.Web.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class UtilizadoresController : Controller
    {
        private readonly OasisContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public UtilizadoresController(OasisContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
            => (_context, _userManager, _configuration) = (context, userManager, configuration);

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            UtilizadoresViewModel utilizadoresViewModel = new()
            {
                Utilizadores = await _context.Utilizadores
                                                  .AsNoTracking()
                                                  .OrderByDescending(user => user.DataCriacao)
                                                  .ToListAsync(),
                EscolasDropdownList = await _context.Escolas.AsNoTracking().OrderBy(escola => escola.Nome)
                                     .Select(escola => new SelectListItem { Value = escola.Id.ToString(), Text = $"{escola.Nome}, {escola.Distrito} - {escola.CodigoPostal}" }).ToListAsync()
            };
            return View(model: utilizadoresViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AdicionarMembroDirecao([FromForm] UtilizadoresViewModel inserirMembroDirecaoViewModel)
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
                Email = inserirMembroDirecaoViewModel.MembroDirecao.Email,
                UserName = inserirMembroDirecaoViewModel.MembroDirecao.Email,
                PrimeiroNome = inserirMembroDirecaoViewModel.MembroDirecao.PrimeiroNome,
                Apelido = inserirMembroDirecaoViewModel.MembroDirecao.Apelido,
                SecurityStamp = Guid.NewGuid().ToString(),
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

                    _context.DirecoesEscolas.Add(new DirecaoEscola
                    {
                        ApplicationUserId = userDirecao.Id,
                        EscolaId = inserirMembroDirecaoViewModel.IdEscola
                    });

                    await _context.SaveChangesAsync();

                    await databaseTransaction.CommitAsync();
                    SmtpClient client = new();

                    await client.EnviarEmailAsync("Foste inscrito na oasis", $"Password gerada: {passwordGerada}", userDirecao.Email, client.ConfiguracoesEmail(_configuration));

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
                    Descricao = "Pedimos desculpa pela incómodo. Já foi enviado a informação aos nossos técnicos. Por favor, tente novamente mais tarde.",
                    OcorreuAlgumErro = true,
                    UrlRedirecionar = string.Empty
                });
            }
        }
    }
}