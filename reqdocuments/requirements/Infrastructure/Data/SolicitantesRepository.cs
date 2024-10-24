using MediatR;
using requirements.Domain.Entities;
using requirements.Domain.Interfaces;
using requirements.Infrastructure.Data.Queries;
using System.Data;

namespace requirements.Infrastructure.Data
{
    public class SolicitantesRepository : ISolicitantesRepository
    {
        private readonly SolicitantesQueries _solicitantesQueries;

        public SolicitantesRepository(IDbConnection dbConnection)
        {
            _solicitantesQueries = new SolicitantesQueries(dbConnection);
        }
        public async Task<Unit> AddSolicitante(Solicitantes solicitantes)
        {
            return await _solicitantesQueries.AddSolicitante(solicitantes);
        }

        public async Task<Unit> DeleteSolicitante(int solicitanteId)
        {
            return await _solicitantesQueries.DeleteSolicitante(solicitanteId);
        }

        public async Task<Solicitantes> GetSolicitante(int id)
        {
            return await _solicitantesQueries.GetSolicitante(id);
        }

        public async Task<IEnumerable<Solicitantes>> GetSolicitantes()
        {
            return await _solicitantesQueries.GetSolicitantes();
        }
    }
}
