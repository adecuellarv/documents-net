using MediatR;
using Microsoft.IdentityModel.Tokens;
using requirements.Application.DTOs;
using requirements.Domain.Entities;
using requirements.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace requirements.Application
{
    public class UsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IConfiguration _configuration;

        public UsuariosService(IUsuariosRepository usuariosRepository, IConfiguration configuration)
        {
            _usuariosRepository = usuariosRepository;
            _configuration = configuration;
        }

        public async Task<UsuarioLoginDto> GetUsuario(string username, string password)
        {
            var usuario = await _usuariosRepository.GetUsuario(username, password);
            if (usuario != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, usuario.Password))
                {
                    return new UsuarioLoginDto
                    {
                        UsuarioId =usuario.UsuarioId,
                        Nombre = usuario.Nombre,
                        UserName = usuario.UserName,
                        Token = GenerateJwtToken(usuario)
                    };
                }
            }

            return null;
        }

        public async Task<IEnumerable<UsuariosDto>> GetUsuarios() => await _usuariosRepository.GetUsuarios();

        public async Task<Unit> AddUsuario(Usuarios usuario)
        {
            usuario.Password = EncryptPassword(usuario.Password); // Encriptar la contraseña
            var usuarioDto = new Usuarios
            {
                Nombre = usuario.Nombre,
                UserName = usuario.UserName,
                Password = usuario.Password
            };
            return await _usuariosRepository.AddUsuario(usuarioDto);
        }

        private string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); // Encriptar la contraseña
        }

        private string GenerateJwtToken(Usuarios usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
