using Dapper;
using MediatR;
using Npgsql;
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
            const string query = "SELECT usuarioid, nombre, username, password, fecharegistro FROM public.usuarios";
            return await _dbConnection.QueryAsync<Usuarios>(query);
        }

        public async Task<Usuarios> GetUsuarioById(int id)
        {
            const string query = "SELECT usuarioid, nombre, username, password, fecharegistro FROM public.usuarios WHERE usuarioid = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Usuarios>(query, new { Id = id });
        }

        public async Task<Unit> AddUsuario(Usuarios usuario)
        {
            try
            {
                const string query = "SELECT public.insertar_usuario(@Nombre, @UserName, @Password)";
                var parameters = new
                {
                    Nombre = usuario.Nombre,
                    UserName = usuario.UserName,
                    Password = usuario.Password
                };
                await _dbConnection.ExecuteAsync(query, parameters);

                return Unit.Value;
            }
            catch (PostgresException ex)
            {
                if (ex.SqlState == "23505")
                {
                    throw new CustomException(500, ex.Message);
                }
                else
                {
                    throw new CustomException(409, ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(409, ex.Message);
            }
        }

    }
}
