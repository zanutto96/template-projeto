using Service.Validation;
using Domain.Entities;
using Domain.Filters; 
using Data.Repository;

namespace Service.Business
{
    public class AlunoStatusService
    {
        private AlunoStatusRepository _rep;
        public AlunoStatusService(AlunoStatusRepository rep)
        {
            this._rep = rep;
        }
        public async Task<dynamic> GetData(AlunoStatusFilter filters)
        {
            return await _rep.GetData(filters);
        }

        public async Task<dynamic> GetDataItem(AlunoStatusFilter filters)
        {
            return await _rep.GetDataItem(filters);
        }
        

        public async Task<AlunoStatus> GetOne(AlunoStatusFilter filters)
        {
            return await _rep.GetOne(filters);
        }

        public bool Validate(AlunoStatus entity, out List<string> erros)
        {
            erros = new List<string>();
            return new AlunoStatusValidation().ValidateModel(entity, out erros);
        }

        public async Task<dynamic> Save(AlunoStatus entity)
        { 
            return await _rep.Save(entity); 
        }

        public async Task<dynamic> SavePartial(AlunoStatus entity)
        {
            return await _rep.SavePartial(entity);
        }

        public async Task Remove(AlunoStatus entity)
        {
            await _rep.Remove(entity);
        }

        
    }
}
