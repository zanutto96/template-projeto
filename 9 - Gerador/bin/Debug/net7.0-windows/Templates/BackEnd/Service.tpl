using Service.Validation;
using Domain.Entities;
using Domain.Filters; 
using Data.Repository;

namespace Service.Business
{
    public class #Entity#Service
    {
        private #Entity#Repository _rep;
        public #Entity#Service(#Entity#Repository rep)
        {
            this._rep = rep;
        }
        public async Task<dynamic> GetData(#Entity#Filter filters)
        {
            return await _rep.GetData(filters);
        }

        public async Task<dynamic> GetDataItem(#Entity#Filter filters)
        {
            return await _rep.GetDataItem(filters);
        }
        

        public async Task<#Entity#> GetOne(#Entity#Filter filters)
        {
            return await _rep.GetOne(filters);
        }

        public bool Validate(#Entity# entity, out List<string> erros)
        {
            erros = new List<string>();
            return new #Entity#Validation().ValidateModel(entity, out erros);
        }

        public async Task<dynamic> Save(#Entity# entity)
        { 
            return await _rep.Save(entity); 
        }

        public async Task<dynamic> SavePartial(#Entity# entity)
        {
            return await _rep.SavePartial(entity);
        }

        public async Task Remove(#Entity# entity)
        {
            await _rep.Remove(entity);
        }

        
    }
}
