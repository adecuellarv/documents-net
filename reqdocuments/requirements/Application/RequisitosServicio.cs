using MediatR;
using requirements.Domain.Entities;
using requirements.Domain.Interfaces;
using requirements.Infrastructure.Data;

namespace requirements.Application
{
    public class RequisitosServicio
    {
        private readonly IRequisitosRepository _requisitosRepository;

        public RequisitosServicio(IRequisitosRepository requisitosRepository)
        {
            _requisitosRepository = requisitosRepository;
        }

        public async Task<IEnumerable<Requisitos>> GetRequisitos() => await _requisitosRepository.GetRequisitos();

        public async Task<Requisitos> GetRequisito(int id)
        {
            var requisito = await _requisitosRepository.GetRequisito(id);
            if (requisito != null)
            {
                return requisito;
            }

            throw new Exception("Solicitante no encontrado");
        }

        public async Task<Unit> AddRequisito(Requisitos requisitos)
        {
            return await _requisitosRepository.AddRequisito(requisitos);
        }
        public async Task<Unit> UpdateRequisito(int id, Requisitos requisitos)
        {
            return await _requisitosRepository.UpdateRequisito(id, requisitos);
        }
        public async Task<Unit> DeleteRequisito(int id)
        {
            return await _requisitosRepository.DeleteRequisito(id);
        }
    }
}
