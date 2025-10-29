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
    public class AlunoStatusRepository : BaseRepository
    {
        private readonly DataContext _dataContext;
        public AlunoStatusRepository(DataContext dataContext, ICurrentUser currentUser): base(currentUser)
        {
            _dataContext = dataContext;
        }

        public IQueryable<AlunoStatus> GetBySimplefilters(AlunoStatusFilter filters)
        {
            var querybase = this.GetAll()
                                .WithBasicFilters(filters)
                                .WithCustomFilters(filters)
                                .OrderByDomain(filters);
            return querybase;
        } 

        public async Task<dynamic> GetData(AlunoStatusFilter filters)
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

        public async Task<dynamic> GetDataItem(AlunoStatusFilter filters)
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
                    Id = _.AlunoStatusId,
                    Name = _.Descricao
                }),
                Summary
            };
        }

        public async Task<AlunoStatus> GetOne(AlunoStatusFilter filters)
        {
            var entity = await _dataContext.AlunoStatus
                    .Where(_ => _.AlunoStatusId == filters.AlunoStatusId).FirstOrDefaultAsync();
            return entity;
        }

        public async Task Remove(AlunoStatus entity)
        {
            var existing = await GetOne(new AlunoStatusFilter { AlunoStatusId = entity.AlunoStatusId.Value });
            _dataContext.Remove(existing);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<AlunoStatus> Save(AlunoStatus entity)
        {
            var result = this._dataContext.Add(entity);

            await this._dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<AlunoStatus> SavePartial(AlunoStatus entity)
        {
            if (entity == null)
                return null;

            var existing = await GetOne(new AlunoStatusFilter { AlunoStatusId = entity.AlunoStatusId.Value });
            if(existing != null)
            {
                _dataContext.Entry(existing).CurrentValues.SetValues(entity);
                _dataContext.Update(existing);
                await _dataContext.SaveChangesAsync();
            } 
            return existing;
        }

        private IQueryable<AlunoStatus> GetAll(params Expression<Func<AlunoStatus, object>>[] includes)
        {
            IQueryable<AlunoStatus> query = _dataContext.Set<AlunoStatus>(); 
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
