﻿using requirements.Domain.Entities;
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
        public async Task<IEnumerable<Usuarios>> GetAllUsuarios() => await _usuariosRepository.GetUsuarios();
        public async Task AddUsuario(Usuarios usuario) => await _usuariosRepository.AddUsuario(usuario);
    }

}
