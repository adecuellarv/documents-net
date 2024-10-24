using Dapper;
using MediatR;
using Npgsql;
using requirements.Domain.Entities;
using System.Data;

namespace requirements.Infrastructure.Data.Queries
{
    public class DocumentosQueries
    {
        private readonly IDbConnection _dbConnection;
        public DocumentosQueries(IDbConnection dbConnection) {
            _dbConnection = dbConnection;
        }

        public async Task<Unit> AddDocumento(Documentos documentos)
        {
            try
            {
                const string query = "SELECT public.insertar_documento(@RequisitoId, @SolicitanteId, @Url, @UsuarioRegistro)";
                var parameters = new
                {
                    RequisitoId = documentos.RequisitoId,
                    SolicitanteId = documentos.SolicitanteId,
                    Url = documentos.Url,
                    UsuarioRegistro = documentos.UsuarioRegistro
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

        public async Task<Unit> DeleteDocumento(int documentId)
        {
            const string query = "DELETE FROM public.documentos WHERE documentoid = @id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Unit>(query, new { id = documentId });
        }

        public async Task<Documentos> GetDocumento(int documentId)
        {
            const string query = "SELECT documentoid, requisitoid, solicitanteid, url, fecharegistro, usuarioregistro, fechamodificacion, usuariomodificacion FROM public.documentos WHERE documentoid = @id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Documentos>(query, new { id = documentId });
        }

        public async Task<IEnumerable<Documentos>> GetDocumentos()
        {
            const string query = "SELECT documentoid, requisitoid, solicitanteid, url, fecharegistro, usuarioregistro, fechamodificacion, usuariomodificacion FROM public.documentos";
            return await _dbConnection.QueryAsync<Documentos>(query);
        }

        public async Task<Unit> UpdateDocumento(int id, Documentos documentos)
        {
            try
            {
                const string query = "UPDATE public.documentos SET RequisitoId = @RequisitoId, SolicitanteId = @SolicitanteId, Url = @Url, UsuarioModificacion = @UsuarioModificacion WHERE documentoid = @DocumentoId";
                var parameters = new
                {
                    DocumentoId = id,
                    RequisitoId = documentos.RequisitoId,
                    SolicitanteId = documentos.SolicitanteId,
                    Url = documentos.Url,
                    UsuarioModificacion = documentos.UsuarioRegistro
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
