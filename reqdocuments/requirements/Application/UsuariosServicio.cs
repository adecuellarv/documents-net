using MediatR;
using requirements.Application.DTOs;
using requirements.Domain.Entities;
using requirements.Domain.Interfaces;

namespace requirements.Application
{
    public class UsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosService(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public async Task<Usuarios> GetUsuario(string username, string password)
        {
            var usuario = await _usuariosRepository.GetUsuario(username, password);
            if (usuario != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, usuario.Password))
                {
                    return new Usuarios
                    {
                        UsuarioId =usuario.UsuarioId,
                        Nombre = usuario.Nombre,
                        UserName = usuario.UserName
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
    }
}
