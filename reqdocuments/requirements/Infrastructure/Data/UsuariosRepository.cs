using MediatR;
using requirements.Application.DTOs;
using requirements.Domain.Entities;
using requirements.Domain.Interfaces;
using requirements.Infrastructure.Data.Queries;
using System.Data;

namespace requirements.Infrastructure.Data
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly UsuariosQueries _usuariosQueries;

        public UsuariosRepository(IDbConnection dbConnection)
        {
            _usuariosQueries = new UsuariosQueries(dbConnection);
        }

        public async Task<IEnumerable<UsuariosDto>> GetUsuarios()
        {
            return await _usuariosQueries.GetAllUsuarios();
        }

        public async Task<Usuarios> GetUsuario(string username, string password)
        {
            return await _usuariosQueries.GetUsuarioByCredentials(username);
        }

        public async Task<Unit> AddUsuario(Usuarios usuario)
        {
            return await _usuariosQueries.AddUsuario(usuario);
        }
    }
}
