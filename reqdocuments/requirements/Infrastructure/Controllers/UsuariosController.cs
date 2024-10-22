using MediatR;
using Microsoft.AspNetCore.Mvc;
using requirements.Application;
using requirements.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace requirements.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosService _services;
        
        public UsuariosController(UsuariosService services)
        {
            _services = services;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public async Task<ActionResult<List<Usuarios>>> GetUsuarios()
        {
            var usuarios = await _services.GetUsuarios();
            return Ok(usuarios);
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarioById(int id)
        {
            var usuario = await _services.GetUsuario(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public async Task<ActionResult<Unit>> CreateUsuario([FromBody] Usuarios data)
        {
            if (data == null)
            {
                return BadRequest("Usario no puede ser nulo");
            }

            return Ok(await _services.AddUsuario(data));
        }
    }
}
