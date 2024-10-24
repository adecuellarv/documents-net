using MediatR;
using requirements.Domain.Entities;

namespace requirements.Domain.Interfaces
{
    public interface ISolicitantesRepository
    {
        Task<IEnumerable<Solicitantes>> GetSolicitantes();
        Task<Solicitantes> GetSolicitante(int solicitanteId);
        Task<Unit> AddSolicitante(Solicitantes usuarios);
        Task<Unit> DeleteSolicitante(int solicitanteId);
    }
}
