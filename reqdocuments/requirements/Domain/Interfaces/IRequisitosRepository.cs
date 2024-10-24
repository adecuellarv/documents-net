using MediatR;
using requirements.Domain.Entities;

namespace requirements.Domain.Interfaces
{
    public interface IRequisitosRepository
    {
        Task<IEnumerable<Requesitos>> GetRequisitos();
        Task<Requesitos> GetRequisito(int solicitanteId);
        Task<Unit> AddRequisito(Requesitos usuarios);
        Task<Unit> UpdateRequisito(int id, Requesitos usuarios);
        Task<Unit> DeleteRequisito(int solicitanteId);
    }
}
