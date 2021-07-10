namespace Oasis.Dominio.Entidades
{
    public class PostUtilizadorGuardado
    {
        public ApplicationUser Utilizador { get; set; }
        public Post Post { get; set; }
        public int ApplicationUserId { get; set; }
        public int PostId { get; set; }
    }
}
