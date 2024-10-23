using MediatR;
using requirements.Application.DTOs;
using requirements.Domain.Entities;

namespace requirements.Domain.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<UsuariosDto>> GetUsuarios();
        Task<Usuarios> GetUsuario(string username, string password);
        Task<Unit> AddUsuario(Usuarios usuarios);
    }
}
