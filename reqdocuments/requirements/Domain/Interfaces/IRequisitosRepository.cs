using MediatR;
using requirements.Domain.Entities;

namespace requirements.Domain.Interfaces
{
    public interface IRequisitosRepository
    {
        Task<IEnumerable<Requisitos>> GetRequisitos();
        Task<Requisitos> GetRequisito(int solicitanteId);
        Task<Unit> AddRequisito(Requisitos usuarios);
        Task<Unit> UpdateRequisito(int id, Requisitos usuarios);
        Task<Unit> DeleteRequisito(int solicitanteId);
    }
}
