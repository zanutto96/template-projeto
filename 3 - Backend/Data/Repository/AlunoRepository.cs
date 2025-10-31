using Common.Security.Identity;
using Data.BasicExtensions;
using Data.Context;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AlunoRepository : BaseRepository
    {
        private readonly DataContext _dataContext;
        public AlunoRepository(DataContext dataContext, ICurrentUser currentUser): base(currentUser)
        {
            _dataContext = dataContext;
        }

        public IQueryable<Aluno> GetBySimplefilters(AlunoFilter filters)
        {
            var querybase = this.GetAll().Include(_=>_.AlunoStatus)
                                .WithBasicFilters(filters)
                                .WithCustomFilters(filters)
                                .OrderByDomain(filters);
            return querybase;
        } 

        public async Task<dynamic> GetData(AlunoFilter filters)
        {
            var source = GetBySimplefilters(filters);
            var total = source.Count();
            var result = await source.Paging(filters).ToListAsync();
            var Summary = new
            {
                Total = total,
                PageSize = filters.PageSize
            };
            return new {
                DataList = result,
                Summary 
            };
        } 

        public async Task<dynamic> GetDataItem(AlunoFilter filters)
        {
            var source = GetBySimplefilters(filters);
            var total = source.Count();
            var result = await source.Paging(filters).ToListAsync();
            var Summary = new
            {
                Total = total,
                PageSize = filters.PageSize
            };
            return new
            {
                DataList = result.Select(_ => new
                {
                    Id = _.AlunoId,
                    Name = _.AreaAtuacaoEmpresa
                }),
                Summary
            };
        }

        public async Task<Aluno> GetOne(AlunoFilter filters)
        {
            var entity = await _dataContext.Aluno
                    .Where(_ => _.AlunoId == filters.AlunoId).FirstOrDefaultAsync();
            return entity;
        }

        public async Task Remove(Aluno entity)
        {
            var existing = await GetOne(new AlunoFilter { AlunoId = entity.AlunoId });
            _dataContext.Remove(existing);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Aluno> Save(Aluno entity)
        {
            var result = this._dataContext.Add(entity);
            // var auditEntity = result.Entity as Data.BasicExtensions.AuditBasic;
            // if (auditEntity != null)
            // {
            //     // auditEntity.UpdateAudit(_currentUser);
            // }
            await this._dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Aluno> SavePartial(Aluno entity)
        {
            if (entity == null)
                return null;

            var existing = await GetOne(new AlunoFilter { AlunoId = entity.AlunoId });
            if(existing != null)
            {
                _dataContext.Entry(existing).CurrentValues.SetValues(entity);
                // var auditEntity = _dataContext.Entry(existing).Entity as Data.BasicExtensions.AuditBasic;
                // if (auditEntity != null)
                // {
                //     // auditEntity.UpdateAudit(_currentUser);
                // }
                _dataContext.Update(existing);
                await _dataContext.SaveChangesAsync();
            } 
            return existing;
        }

        private IQueryable<Aluno> GetAll(params Expression<Func<Aluno, object>>[] includes)
        {
            IQueryable<Aluno> query = _dataContext.Set<Aluno>(); 
            return query.AsNoTracking(); 
        }


        #region Examples

        /********* Examples *************/ 
        //Example of DataListCusto using Linq and Left Join
        //public async Task<IEnumerable<dynamic>> GetDataListCustom(CidadeFilter filters)
        //{
        //    var list = await (from c in this._dataContext.Table1
        //                      from u in this._dataContext.Table2  
        //                      .Where(_ => _.Id == c.IdTable1).DefaultIfEmpty()
        //                      select new
        //                      {
        //                          c.Id,
        //                          c.Nome,
        //                          unid = u,
        //                          Contador = this._dataContext.UnidadeFederacao.Count() //example of subselect
        //                      }).ToListAsync();
        //    return list;
        //}

        //Example of DataListCusto using Linq and Inner join Join
        //public async Task<IEnumerable<dynamic>> GetDataListCustom(CidadeFilter filters)
        //{
        //    var list = await (from c in this._dataContext.Table1
        //                      join u in this._dataContext.UnidadeFederacao on c.IdTable2 equals u.Id
        //                      select new
        //                      {
        //                          c.Id,
        //                          c.Nome,
        //                          unid = u,
        //                          Contador = this._dataContext.UnidadeFederacao.Count() //example of subselect
        //                      }).ToListAsync();
        //    return list;
        //}
        #endregion
       
    }
}
