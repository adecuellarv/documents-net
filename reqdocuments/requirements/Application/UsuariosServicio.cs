using MediatR;
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

        public async Task<Usuarios> GetUsuario(int id) => await _usuariosRepository.GetUsuario(id);
        public async Task<IEnumerable<Usuarios>> GetUsuarios() => await _usuariosRepository.GetUsuarios();
        public async Task<Unit> AddUsuario(Usuarios usuario)
        {
            usuario.Password = EncryptPassword(usuario.Password);
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
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }

}
