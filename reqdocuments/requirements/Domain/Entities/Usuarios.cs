namespace requirements.Domain.Entities
{
    public class Usuarios
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? FechaRegistro { get; set; }
    }
}
