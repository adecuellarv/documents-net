namespace requirements.Domain.Entities
{
    public class Solicitantes
    {
        public int SolicitanteId { get; set; }
        public string Nombre { get; set; }
        public string FechaRegistro { get; set; }
        public int UsuarioRegistro { get; set; }
        public string FechaModificacion { get; set; }
        public int UsuarioModificacion { get; set; }
    }
}
