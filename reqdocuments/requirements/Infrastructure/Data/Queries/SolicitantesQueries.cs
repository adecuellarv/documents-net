using Dapper;
using MediatR;
using Npgsql;
using requirements.Application.DTOs;
using requirements.Domain.Entities;
using System.Data;

namespace requirements.Infrastructure.Data.Queries
{
    public class SolicitantesQueries
    {
        private readonly IDbConnection _dbConnection;

        public SolicitantesQueries(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Solicitantes>> GetSolicitantes()
        {
            const string query = "SELECT solicitanteid, nombre, fecharegistro, usuarioregistro, fechamodificacion, usuariomodificacion FROM public.solicitantes";
            return await _dbConnection.QueryAsync<Solicitantes>(query);
        }

        public async Task<Solicitantes> GetSolicitante(int id)
        {
            const string query = "SELECT solicitanteid, nombre, fecharegistro, usuarioregistro, fechamodificacion, usuariomodificacion FROM public.solicitantes WHERE solicitanteid = @id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Solicitantes>(query, new { id = id });
        }

        public async Task<Unit> AddSolicitante(Solicitantes solicitantes)
        {
            try
            {
                const string query = "SELECT public.insertar_solicitante(@Nombre, @UsuarioRegistro)";
                var parameters = new
                {
                    Nombre = solicitantes.Nombre,
                    UsuarioRegistro = solicitantes.UsuarioRegistro
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

        public async Task<Unit> DeleteSolicitante(int id)
        {
            const string query = "DELETE FROM public.solicitantes WHERE solicitanteid = @id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Unit>(query, new { id = id });
        }
    }
}
