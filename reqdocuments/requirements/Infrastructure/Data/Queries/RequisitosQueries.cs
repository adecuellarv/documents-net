using Dapper;
using MediatR;
using Npgsql;
using requirements.Domain.Entities;
using System.Data;

namespace requirements.Infrastructure.Data.Queries
{
    public class RequisitosQueries
    {
        private readonly IDbConnection _dbConnection;

        public RequisitosQueries(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<Unit> AddRequisito(Requisitos requesitos)
        {
            try
            {
                const string query = "SELECT public.insertar_requisito(@Nombre, @Extension, @Status, @UsuarioRegistro)";
                var parameters = new
                {
                    Nombre = requesitos.Nombre,
                    Extension = requesitos.Extension,
                    Status = requesitos.Status,
                    UsuarioRegistro = requesitos.UsuarioRegistro
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

        public async Task<Unit> DeleteRequisito(int id)
        {
            const string query = "DELETE FROM public.requisitos WHERE requisitoid = @id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Unit>(query, new { id = id });
        }

        public async Task<Requisitos> GetRequisito(int id)
        {
            const string query = "SELECT requisitoid, nombre, extension, status, fecharegistro, usuarioregistro, fechamodificacion, usuariomodificacion FROM public.requisitos WHERE requisitoid = @id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Requisitos>(query, new { id = id });
        }

        public async Task<IEnumerable<Requisitos>> GetRequisitos()
        {
            const string query = "SELECT requisitoid, nombre, extension, status, fecharegistro, usuarioregistro, fechamodificacion, usuariomodificacion FROM public.requisitos";
            return await _dbConnection.QueryAsync<Requisitos>(query);
        }

        public async Task<Unit> UpdateRequisito(int id, Requisitos requesitos)
        {
            try
            {
                const string query = "UPDATE public.requisitos SET Nombre = @Nombre, Extension = @Extension, Status = @Status, UsuarioModificacion = @UsuarioModificacion WHERE requisitoid = @Requisitoid";
                var parameters = new
                {
                    Requisitoid = id,
                    Nombre = requesitos.Nombre,
                    Extension = requesitos.Extension,
                    Status = requesitos.Status,
                    UsuarioModificacion = requesitos.UsuarioRegistro
                };
                await _dbConnection.ExecuteAsync(query, parameters);

                return Unit.Value;
            }
            catch (PostgresException ex)
            {
                if (ex.SqlState == "23505") // Error de duplicado
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
