using Dapper;
using requirements.Domain.Entities;
using System.Data;

namespace requirements.Infrastructure.Data.Queries
{
    public class UsuariosQueries
    {
        private readonly IDbConnection _dbConnection;

        public UsuariosQueries(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Usuarios>> GetAllUsuarios()
        {
            const string query = "SELECT * FROM Usuarios";
            return await _dbConnection.QueryAsync<Usuarios>(query);
        }

        public async Task<Usuarios> GetUsuarioById(int id)
        {
            const string query = "SELECT * FROM Usuarios WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Usuarios>(query, new { Id = id });
        }

        public async Task AddUsuario(Usuarios usuario)
        {
            const string query = "SELECT insertar_usuario(@Nombre, @UserName, @Password)";
            var parameters = new
            {
                Nombre = usuario.Nombre,
                UserName = usuario.UserName,
                Password = usuario.Password
            };
            await _dbConnection.ExecuteAsync(query, parameters);
        }

    }
}
