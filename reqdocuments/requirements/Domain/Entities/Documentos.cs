namespace requirements.Domain.Entities
{
    public class Documentos
    {
        public int DocumentoId { get; set; }
        public int? RequisitoId { get; set; }
        public int? SolicitanteId { get; set; }
        public string? Url { get; set; }
        public string? FechaRegistro { get; set; }
        public int? UsuarioRegistro { get; set; }
        public string? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
    }
}
