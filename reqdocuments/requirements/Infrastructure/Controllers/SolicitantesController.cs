using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using requirements.Application;
using requirements.Domain.Entities;
using requirements.Infrastructure.Data.Queries;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace requirements.Infrastructure.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitantesController : ControllerBase
    {
        private readonly SolicitantesServicio _solicitantesServicio;

        public SolicitantesController(SolicitantesServicio solicitantesServicio)
        {
            _solicitantesServicio = solicitantesServicio;
        }

        // GET: api/solicitantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitantes>>> GetSolicitantes()
        {
            var solicitantes = await _solicitantesServicio.GetSolicitantes();
            return Ok(solicitantes);
        }

        // GET api/<SolicitantesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetSolicitante(int id)
        {
            try{ 
                var solicitante = await _solicitantesServicio.GetSolicitante(id);
                if (solicitante == null)
                {
                    return NotFound();
                }
                return Ok(solicitante);
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

        // POST api/<SolicitantesController>
        [HttpPost]
        public async Task<ActionResult<Unit>> AddSolicitante([FromBody] Solicitantes data)
        {
            try
            {
                return Ok(await _solicitantesServicio.AddSolicitante(data));
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

        // PUT api/<SolicitantesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateSolicitante(int id, [FromBody] Solicitantes data)
        {
            try
            {
                return Ok(await _solicitantesServicio.UpdateSolicitante(id, data));
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


        // DELETE api/<SolicitantesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitante(int id)
        {
            await _solicitantesServicio.DeleteSolicitante(id);
            return NoContent();
        }
    }
}
