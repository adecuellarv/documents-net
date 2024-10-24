using MediatR;
using Microsoft.Extensions.Configuration;
using requirements.Domain.Entities;
using requirements.Domain.Interfaces;
using requirements.Infrastructure.Data;
using requirements.Infrastructure.Data.Queries;

namespace requirements.Application
{
    public class DocumentosServicio
    {
        private readonly IDocumentosRepository _documentosRepository;

        public DocumentosServicio(IDocumentosRepository documentosRepository)
        {
            _documentosRepository = documentosRepository;
        }

        public async Task<Unit> AddDocumento(Documentos documentos)
        {
            return await _documentosRepository.AddDocumento(documentos);
        }

        public async Task<Unit> DeleteDocumento(int documentId)
        {
            return await _documentosRepository.DeleteDocumento(documentId);
        }
        public async Task<Documentos> GetDocumento(int documentId)
        {
            return await _documentosRepository.GetDocumento(documentId);
        }

        public async Task<IEnumerable<Documentos>> GetDocumentos()
        {
            return await _documentosRepository.GetDocumentos();
        }

        public async Task<Unit> UpdateDocumento(int id, Documentos documentos)
        {
            return await _documentosRepository.UpdateDocumento(id, documentos);
        }
    }
}
