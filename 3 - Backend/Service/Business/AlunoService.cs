using Service.Validation;
using Domain.Entities;
using Domain.Filters; 
using Data.Repository;

namespace Service.Business
{
    public class AlunoService
    {
        private AlunoRepository _rep;
        public AlunoService(AlunoRepository rep)
        {
            this._rep = rep;
        }
        public async Task<dynamic> GetData(AlunoFilter filters)
        {
            return await _rep.GetData(filters);
        }

        public async Task<dynamic> GetDataItem(AlunoFilter filters)
        {
            return await _rep.GetDataItem(filters);
        }
        

        public async Task<Aluno> GetOne(AlunoFilter filters)
        {
            return await _rep.GetOne(filters);
        }

        public bool Validate(Aluno entity, out List<string> erros)
        {
            erros = new List<string>();
            return new AlunoValidation().ValidateModel(entity, out erros);
        }

        public async Task<dynamic> Save(Aluno entity)
        { 
            return await _rep.Save(entity); 
        }

        public async Task<dynamic> SavePartial(Aluno entity)
        {
            return await _rep.SavePartial(entity);
        }

        public async Task Remove(Aluno entity)
        {
            await _rep.Remove(entity);
        }

        
    }
}
