using requirements.Domain.Entities;

namespace requirements.Domain.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<Usuarios>> GetUsuarios();
        Task<Usuarios> GetUsuario(int id);
        Task AddUsuario(Usuarios usuarios);
    }
}
