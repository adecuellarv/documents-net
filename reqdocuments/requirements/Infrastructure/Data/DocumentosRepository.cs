using MediatR;
using requirements.Domain.Entities;
using requirements.Domain.Interfaces;
using requirements.Infrastructure.Data.Queries;
using System.Data;

namespace requirements.Infrastructure.Data
{
    public class DocumentosRepository : IDocumentosRepository
    {
        private readonly DocumentosQueries _documentosQueries;

        public DocumentosRepository(IDbConnection dbConnection)
        {
            _documentosQueries = new DocumentosQueries(dbConnection);
        }

        public async Task<Unit> AddDocumento(Documentos documentos)
        {
            return await _documentosQueries.AddDocumento(documentos);
        }

        public async Task<Unit> DeleteDocumento(int documentId)
        {
            return await _documentosQueries.DeleteDocumento(documentId);
        }

        public async Task<Documentos> GetDocumento(int documentId)
        {
            return await _documentosQueries.GetDocumento(documentId);
        }

        public async Task<IEnumerable<Documentos>> GetDocumentos()
        {
            return await _documentosQueries.GetDocumentos();
        }

        public async Task<Unit> UpdateDocumento(int id, Documentos documentos)
        {
            return await _documentosQueries.UpdateDocumento(id, documentos);
        }
    }
}
