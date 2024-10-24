using MediatR;
using requirements.Application.DTOs;
using requirements.Domain.Entities;

namespace requirements.Domain.Interfaces
{
    public interface IDocumentosRepository
    {
        Task<IEnumerable<Documentos>> GetDocumentos();
        Task<IEnumerable<DocumentosDto>> GetDocumento(int documentId);
        Task<Unit> AddDocumento(Documentos documentos, string scheme, string host, IFormFile archivo);
        Task<Unit> UpdateDocumento(int id, Documentos documentos, string scheme, string host, IFormFile archivo);
        Task<Unit> DeleteDocumento(int documentId);
    }
}
