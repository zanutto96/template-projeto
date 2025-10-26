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
    public class #Entity#Repository : BaseRepository
    {
        private readonly DataContext _dataContext;
        public #Entity#Repository(DataContext dataContext, ICurrentUser currentUser): base(currentUser)
        {
            _dataContext = dataContext;
        }

        public IQueryable<#Entity#> GetBySimplefilters(#Entity#Filter filters)
        {
            var querybase = this.GetAll()
                                .WithBasicFilters(filters)
                                .WithCustomFilters(filters)
                                .OrderByDomain(filters);
            return querybase;
        } 

        public async Task<dynamic> GetData(#Entity#Filter filters)
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

        public async Task<dynamic> GetDataItem(#Entity#Filter filters)
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
                    Id = _.#EntityRepositoryPrimaryKey#,
                    Name = _.#EntityRepositoryFirstString#
                }),
                Summary
            };
        }

        public async Task<#Entity#> GetOne(#Entity#Filter filters)
        {
            var entity = await _dataContext.#Entity#
                    .Where(_ => _.#EntityRepositoryPrimaryKey# == filters.#EntityRepositoryPrimaryKey#).FirstOrDefaultAsync();
            return entity;
        }

        public async Task Remove(#Entity# entity)
        {
            var existing = await GetOne(new #Entity#Filter { #EntityRepositoryPrimaryKey# = entity.#EntityRepositoryPrimaryKey# });
            _dataContext.Remove(existing);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<#Entity#> Save(#Entity# entity)
        {
            var result = this._dataContext.Add(entity);
            result.Entity.UpdateAudit(_currentUser);
            await this._dataContext.SaveChangesAsync();
            return result.Entity; 
        }

        public async Task<#Entity#> SavePartial(#Entity# entity)
        {
            if (entity == null)
                return null;

            var existing = await GetOne(new #Entity#Filter { #EntityRepositoryPrimaryKey# = entity.#EntityRepositoryPrimaryKey# });
            if(existing != null)
            {
                _dataContext.Entry(existing).CurrentValues.SetValues(entity);
                _dataContext.Entry(existing).Entity.UpdateAudit(_currentUser);
                _dataContext.Update(existing);
                await _dataContext.SaveChangesAsync();
            } 
            return existing;
        }

        private IQueryable<#Entity#> GetAll(params Expression<Func<#Entity#, object>>[] includes)
        {
            IQueryable<#Entity#> query = _dataContext.Set<#Entity#>(); 
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
