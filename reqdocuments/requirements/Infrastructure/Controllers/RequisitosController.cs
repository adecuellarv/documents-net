using MediatR;
using Microsoft.AspNetCore.Mvc;
using requirements.Application;
using requirements.Domain.Entities;
using requirements.Infrastructure.Data.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace requirements.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitosController : ControllerBase
    {
        private readonly RequisitosServicio _requisitosServicio;
        public RequisitosController(RequisitosServicio requisitosServicio)
        {
            _requisitosServicio = requisitosServicio;
        }
        // GET: api/<RequisitosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requesitos>>> GetRequisitos()
        {
            var requisitos = await _requisitosServicio.GetRequisitos();
            return Ok(requisitos);
        }

        // GET api/<RequisitosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requesitos>> GetRequisito(int id)
        {
            try
            {
                var requisito = await _requisitosServicio.GetRequisito(id);
                if (requisito == null)
                {
                    return NotFound();
                }
                return Ok(requisito);
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

        // POST api/<RequisitosController>
        [HttpPost]
        public async Task<ActionResult<Unit>> AddRequisito([FromBody] Requesitos data)
        {
            try
            {
                return Ok(await _requisitosServicio.AddRequisito(data));
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

        // PUT api/<RequisitosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateRequisito(int id, [FromBody] Requesitos data)
        {
            try
            {
                return Ok(await _requisitosServicio.UpdateRequisito(id, data));
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

        // DELETE api/<RequisitosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequisito(int id)
        {
            await _requisitosServicio.DeleteRequisito(id);
            return NoContent();
        }
    }
}
