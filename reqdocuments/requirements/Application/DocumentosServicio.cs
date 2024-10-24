using MediatR;
using Microsoft.Extensions.Configuration;
using requirements.Application.DTOs;
using requirements.Domain.Entities;
using requirements.Domain.Interfaces;
using requirements.Infrastructure.Data;
using requirements.Infrastructure.Data.Queries;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace requirements.Application
{
    public class DocumentosServicio
    {
        private readonly IDocumentosRepository _documentosRepository;

        public DocumentosServicio(IDocumentosRepository documentosRepository)
        {
            _documentosRepository = documentosRepository;
        }

        public async Task<Unit> AddDocumento(Documentos documentos, string scheme, string host, IFormFile archivo)
        {
            const long maxFileSize = 256 * 1024 * 1024; // 256 MB
            if (archivo.Length > maxFileSize)
            {
                throw new CustomException(401, "El tamaño del archivo no puede ser mayor a 256 MB.");
            }

            var todayDate = DateTime.Now.ToString("yyyyMMdd");
            var extension = Path.GetExtension(archivo.FileName);
            var urlFile = $"{scheme}://{host}/uploads/{todayDate}{extension}";

            var rutaLocal = Path.Combine("wwwroot", "uploads");

            if (!Directory.Exists(rutaLocal))
            {
                Directory.CreateDirectory(rutaLocal);
            }

            var rutaArchivo = Path.Combine(rutaLocal, $"{todayDate}{extension}");
            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var documentosDto = new Documentos
            {
                RequisitoId = documentos.RequisitoId,
                SolicitanteId = documentos.SolicitanteId,
                Url = urlFile,
                UsuarioRegistro = documentos.UsuarioRegistro
            };
            return await _documentosRepository.AddDocumento(documentosDto, scheme, host, archivo);
        }


        public async Task<Unit> DeleteDocumento(int documentId)
        {
            return await _documentosRepository.DeleteDocumento(documentId);
        }
        public async Task<IEnumerable<DocumentosDto>> GetDocumento(int documentId)
        {
            return await _documentosRepository.GetDocumento(documentId);
        }

        public async Task<IEnumerable<Documentos>> GetDocumentos()
        {
            return await _documentosRepository.GetDocumentos();
        }

        public async Task<Unit> UpdateDocumento(int id, Documentos documentos, string scheme, string host, IFormFile archivo)
        {
            const long maxFileSize = 256 * 1024 * 1024; // 256 MB
            if (archivo.Length > maxFileSize)
            {
                throw new CustomException(401, "El tamaño del archivo no puede ser mayor a 256 MB.");
            }

            var todayDate = $"archivo_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";
            var extension = Path.GetExtension(archivo.FileName);
            var urlFile = $"{scheme}://{host}/uploads/{todayDate}{extension}";

            var rutaLocal = Path.Combine("wwwroot", "uploads");

            if (!Directory.Exists(rutaLocal))
            {
                Directory.CreateDirectory(rutaLocal);
            }

            var rutaArchivo = Path.Combine(rutaLocal, $"{todayDate}{extension}");
            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var documentosDto = new Documentos
            {
                RequisitoId = documentos.RequisitoId,
                SolicitanteId = documentos.SolicitanteId,
                Url = urlFile,
                UsuarioRegistro = documentos.UsuarioRegistro
            };
            return await _documentosRepository.UpdateDocumento(id, documentosDto, scheme, host, archivo);
        }

    }
}
