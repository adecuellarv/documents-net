using MediatR;
using requirements.Domain.Entities;
using requirements.Domain.Interfaces;
using requirements.Infrastructure.Data.Queries;
using System.Data;

namespace requirements.Infrastructure.Data
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly IMediator _mediator;
        private readonly UsuariosQueries _usuariosQueries;

        public UsuariosRepository(IMediator mediator, IDbConnection dbConnection)
        {
            _mediator = mediator;
            _usuariosQueries = new UsuariosQueries(dbConnection);
        }

        public async Task<IEnumerable<Usuarios>> GetUsuarios()
        {
            return await _usuariosQueries.GetAllUsuarios();
        }

        public async Task<Usuarios> GetUsuario(int id)
        {
            return await _usuariosQueries.GetUsuarioById(id);
        }

        public async Task AddUsuario(Usuarios usuario)
        {
            await _usuariosQueries.AddUsuario(usuario);
        }
    }
}
