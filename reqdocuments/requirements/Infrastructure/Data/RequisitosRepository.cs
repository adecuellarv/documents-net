using MediatR;
using requirements.Domain.Entities;
using requirements.Domain.Interfaces;
using requirements.Infrastructure.Data.Queries;
using System.Data;

namespace requirements.Infrastructure.Data
{
    public class RequisitosRepository : IRequisitosRepository
    {
        private readonly RequisitosQueries _requisitosQueries;

        public RequisitosRepository(IDbConnection dbConnection)
        {
            _requisitosQueries = new RequisitosQueries(dbConnection);
        }

        public async Task<Unit> AddRequisito(Requesitos usuarios)
        {
            return await _requisitosQueries.AddRequisito(usuarios);
        }

        public async Task<Unit> DeleteRequisito(int solicitanteId)
        {
            return await _requisitosQueries.DeleteRequisito(solicitanteId);
        }

        public async Task<Requesitos> GetRequisito(int solicitanteId)
        {
            return await _requisitosQueries.GetRequisito(solicitanteId);
        }

        public async Task<IEnumerable<Requesitos>> GetRequisitos()
        {
            return await _requisitosQueries.GetRequisitos();
        }

        public async Task<Unit> UpdateRequisito(int id, Requesitos usuarios)
        {
            return await _requisitosQueries.UpdateRequisito(id, usuarios);
        }
    }
}
