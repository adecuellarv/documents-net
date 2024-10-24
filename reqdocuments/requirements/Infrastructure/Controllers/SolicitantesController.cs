using Microsoft.AspNetCore.Mvc;
using requirements.Application;
using requirements.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace requirements.Infrastructure.Controllers
{
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SolicitantesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SolicitantesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SolicitantesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
