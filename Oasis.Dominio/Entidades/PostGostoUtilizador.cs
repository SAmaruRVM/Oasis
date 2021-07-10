namespace Oasis.Dominio.Entidades
{
    public class PostGostoUtilizador
    {
        public ApplicationUser Utilizador { get; set; }
        public Post Post { get; set; }
        public Reacao Reacao { get; set; }

        public int ApplicationUserId { get; set; }
        public int PostId { get; set; }
        public int ReacaoId { get; set; }
    }
}
