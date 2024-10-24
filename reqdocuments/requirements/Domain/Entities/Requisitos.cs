namespace requirements.Domain.Entities
{
    public class Requisitos
    {
        public int RequisitoId { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public int? Status { get; set; }
        public string? FechaRegistro { get; set; }
        public int UsuarioRegistro { get; set; }
        public string? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }

    }
}
