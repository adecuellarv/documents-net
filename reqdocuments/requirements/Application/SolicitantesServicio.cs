using MediatR;
using Microsoft.Extensions.Configuration;
using requirements.Application.DTOs;
using requirements.Domain.Entities;
using requirements.Domain.Interfaces;
using requirements.Infrastructure.Data;

namespace requirements.Application
{
    public class SolicitantesServicio
    {
        private readonly ISolicitantesRepository _solicitantesRepository;

        public SolicitantesServicio(ISolicitantesRepository solicitantesRepository)
        {
            _solicitantesRepository = solicitantesRepository;
        }

        public async Task<IEnumerable<Solicitantes>> GetSolicitantes() => await _solicitantesRepository.GetSolicitantes();

        public async Task<Solicitantes> GetSolicitante(int id)
        {
            var solicitante = await _solicitantesRepository.GetSolicitante(id);
            if (solicitante != null)
            {
                return solicitante;
            }

            throw new Exception("Solicitante no encontrado");
        }

        public async Task<Unit> AddSolicitante(Solicitantes solicitantes)
        {
            return await _solicitantesRepository.AddSolicitante(solicitantes);
        }

        public async Task<Unit> DeleteSolicitante(int id)
        {
            return await _solicitantesRepository.DeleteSolicitante(id);
        }

    }
}
