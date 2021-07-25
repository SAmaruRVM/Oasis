using System;
using Microsoft.EntityFrameworkCore;
using Oasis.Dados;
using Oasis.Dominio.Entidades;
using System.Threading.Tasks;
namespace Oasis.Web.Extensions
{
    public static class OasisContextExtensions
    {
        public static async Task<ApplicationUser> GetLoggedInApplicationUser(this OasisContext @this, string emailUtilizadorLogado)
             => await @this.Utilizadores
                           .Include(utilizador => utilizador.Notificacoes)
                           .Include(utilizador => utilizador.Tema)
                           .Include(utilizador => utilizador.Escola)
                           .ThenInclude(escola => escola.ConteudoPaginaPrincipal)
                           .Include(utilizador => utilizador.Escola)
                           .ThenInclude(escola => escola.Disciplinas)
                           .ThenInclude(disciplina => disciplina.CriadorDirecao)
                           .Include(utilizador => utilizador.GruposOndeEnsina)
                           .Include(utilizador => utilizador.GruposOndeTemAulas)
                           .SingleOrDefaultAsync(utilizador => utilizador.Email == emailUtilizadorLogado);

    }
}
