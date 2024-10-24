using MediatR;
using requirements.Domain.Entities;

namespace requirements.Domain.Interfaces
{
    public interface IDocumentosRepository
    {
        Task<IEnumerable<Documentos>> GetDocumentos();
        Task<Documentos> GetDocumento(int documentId);
        Task<Unit> AddDocumento(Documentos documentos);
        Task<Unit> UpdateDocumento(int id, Documentos documentos);
        Task<Unit> DeleteDocumento(int documentId);
    }
}
