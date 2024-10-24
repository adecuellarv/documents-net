using MediatR;
using Microsoft.AspNetCore.Mvc;
using requirements.Application;
using requirements.Application.DTOs;
using requirements.Domain.Entities;
using requirements.Infrastructure.Data.Queries;

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

        // GET api/<UsuariosController>/login
        [HttpGet("login")]
        public async Task<ActionResult<UsuariosDto>> GetUsuarioByCredentials(string userName, string password)
        {
            try
            {
                var usuario = await _services.GetUsuario(userName, password);
                if (usuario == null) return StatusCode(401, new { error = "No existe usuario" });
                else
                {
                    Response.Headers.Add("Authorization", $"{usuario.Token}");
                    Response.Headers.Add("Access-Control-Expose-Headers", "Authorization");

                    return Ok(new UsuariosDto
                    {
                        UsuarioId = usuario.UsuarioId,
                        Nombre = usuario.Nombre,
                        UserName = usuario.UserName
                    });
                }
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

        // POST api/<UsuariosController>
        [HttpPost]
        public async Task<ActionResult<Unit>> CreateUsuario([FromBody] Usuarios data)
        {
            try
            {
                return Ok(await _services.AddUsuario(data));
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
    }
}
