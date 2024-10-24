using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using requirements.Application;
using requirements.Application.DTOs;
using requirements.Domain.Entities;
using requirements.Infrastructure.Data.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace requirements.Infrastructure.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly DocumentosServicio _documentosServicio;
        public DocumentosController(DocumentosServicio documentosServicio)
        {
            _documentosServicio = documentosServicio;
        }
        // GET: api/<DocumentosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documentos>>> GetDocumentos()
        {
            var documentos = await _documentosServicio.GetDocumentos();
            return Ok(documentos);
        }

        // GET api/<DocumentosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DocumentosDto>>> GetDocumento(int id)
        {
            var documentos = await _documentosServicio.GetDocumento(id);
            if (documentos == null)
            {
                return NotFound();
            }
            return Ok(documentos);
        }

        // POST api/<DocumentosController>
        [HttpPost]
        public async Task<ActionResult<Unit>> AddDocumento([FromForm] Documentos documentos, [FromForm] IFormFile archivo)
        {
            
            try
            {
                if (documentos == null || archivo == null)
                {
                    return BadRequest("Los datos son inválidos.");
                }

                var scheme = Request.Scheme;
                var host = Request.Host.Value;
                return Ok(await _documentosServicio.AddDocumento(documentos, scheme, host, archivo));
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor." });
            }
        }

        // PUT api/<DocumentosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateDocumento(int id, [FromForm] Documentos documentos, [FromForm] IFormFile archivo)
        {
            try
            {
                if (documentos == null || archivo == null)
                {
                    return BadRequest("Los datos son inválidos.");
                }

                var scheme = Request.Scheme;
                var host = Request.Host.Value;
                return Ok(await _documentosServicio.UpdateDocumento(id, documentos, scheme, host, archivo));
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor." });
            }
        }

        // DELETE api/<DocumentosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumento(int id)
        {
            await _documentosServicio.DeleteDocumento(id);
            return NoContent();
        }
    }
}
