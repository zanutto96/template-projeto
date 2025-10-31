using Domain.Entities;
using Domain.Filters; 
using System;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq.Dynamic.Core;


namespace Data.BasicExtensions
{
    public static class AlunoFilterBasicExtension
    {
        private static readonly MethodInfo OrderByMethod =
            typeof(Queryable).GetMethods().Single(method =>
           method.Name == "OrderBy" && method.GetParameters().Length == 2);
        public static IQueryable<Aluno> WithBasicFilters(this IQueryable<Aluno> queryBase, AlunoFilter filters)
        {
            var queryFilter = queryBase;


            if (filters.AlunoFormacaoId != null && filters.AlunoFormacaoId != 0)
                queryFilter = queryFilter.Where(_ => _.AlunoFormacaoId == filters.AlunoFormacaoId);
            if (filters.AlunoId != null && filters.AlunoId != 0)
                queryFilter = queryFilter.Where(_ => _.AlunoId == filters.AlunoId);
            if (filters.AlunoStatusId != null && filters.AlunoStatusId != 0)
                queryFilter = queryFilter.Where(_ => _.AlunoStatusId == filters.AlunoStatusId);
            if (!string.IsNullOrEmpty(filters.AreaAtuacaoEmpresa))
                queryFilter = queryFilter.Where(_ => _.AreaAtuacaoEmpresa.Contains(filters.AreaAtuacaoEmpresa));

            if (!string.IsNullOrEmpty(filters.AreaAtuacaoEmpresa))
                queryFilter = queryFilter.Where(_ => _.AreaAtuacaoEmpresa.Contains(filters.AreaAtuacaoEmpresa));

            if (filters.AreaAtuacaoId != null && filters.AreaAtuacaoId != 0)
                queryFilter = queryFilter.Where(_ => _.AreaAtuacaoId == filters.AreaAtuacaoId);
            if (!string.IsNullOrEmpty(filters.Bairro))
                queryFilter = queryFilter.Where(_ => _.Bairro.Contains(filters.Bairro));

            if (!string.IsNullOrEmpty(filters.Bairro))
                queryFilter = queryFilter.Where(_ => _.Bairro.Contains(filters.Bairro));

            if (!string.IsNullOrEmpty(filters.CargoQueOcupa))
                queryFilter = queryFilter.Where(_ => _.CargoQueOcupa.Contains(filters.CargoQueOcupa));

            if (!string.IsNullOrEmpty(filters.CargoQueOcupa))
                queryFilter = queryFilter.Where(_ => _.CargoQueOcupa.Contains(filters.CargoQueOcupa));

            if (!string.IsNullOrEmpty(filters.CEP))
                queryFilter = queryFilter.Where(_ => _.CEP.Contains(filters.CEP));

            if (!string.IsNullOrEmpty(filters.CEP))
                queryFilter = queryFilter.Where(_ => _.CEP.Contains(filters.CEP));

            if (!string.IsNullOrEmpty(filters.Comentario))
                queryFilter = queryFilter.Where(_ => _.Comentario.Contains(filters.Comentario));

            if (!string.IsNullOrEmpty(filters.Comentario))
                queryFilter = queryFilter.Where(_ => _.Comentario.Contains(filters.Comentario));

            if (!string.IsNullOrEmpty(filters.CPF))
                queryFilter = queryFilter.Where(_ => _.CPF.Contains(filters.CPF));

            if (!string.IsNullOrEmpty(filters.CPF))
                queryFilter = queryFilter.Where(_ => _.CPF.Contains(filters.CPF));

            if (filters.DataAlteracao != null)
            {
                DateTime Start = new DateTime(Convert.ToDateTime(filters.DataAlteracao).Year, Convert.ToDateTime(filters.DataAlteracao).Month, Convert.ToDateTime(filters.DataAlteracao).Day);
                DateTime End = Start.AddDays(1);
                queryFilter = queryFilter.Where(_ => _.DataAlteracao >= Start && _.DataAlteracao < End);
            }

            if (filters.DataCadastro != null)
            {
                DateTime Start = new DateTime(Convert.ToDateTime(filters.DataCadastro).Year, Convert.ToDateTime(filters.DataCadastro).Month, Convert.ToDateTime(filters.DataCadastro).Day);
                DateTime End = Start.AddDays(1);
                queryFilter = queryFilter.Where(_ => _.DataCadastro >= Start && _.DataCadastro < End);
            }

            if (filters.DataNascimento != null)
            {
                DateTime Start = new DateTime(Convert.ToDateTime(filters.DataNascimento).Year, Convert.ToDateTime(filters.DataNascimento).Month, Convert.ToDateTime(filters.DataNascimento).Day);
                DateTime End = Start.AddDays(1);
                queryFilter = queryFilter.Where(_ => _.DataNascimento >= Start && _.DataNascimento < End);
            }

            if (filters.EhEmail != null)
                queryFilter = queryFilter.Where(_ => _.EhEmail == filters.EhEmail);
            if (filters.EhExAluno != null)
                queryFilter = queryFilter.Where(_ => _.EhExAluno == filters.EhExAluno);
            if (filters.EhFalecido != null)
                queryFilter = queryFilter.Where(_ => _.EhFalecido == filters.EhFalecido);
            if (filters.EhMalaDireta != null)
                queryFilter = queryFilter.Where(_ => _.EhMalaDireta == filters.EhMalaDireta);
            if (filters.EhMkt != null)
                queryFilter = queryFilter.Where(_ => _.EhMkt == filters.EhMkt);
            if (filters.EhSMS != null)
                queryFilter = queryFilter.Where(_ => _.EhSMS == filters.EhSMS);
            if (!string.IsNullOrEmpty(filters.Email))
                queryFilter = queryFilter.Where(_ => _.Email.Contains(filters.Email));

            if (!string.IsNullOrEmpty(filters.Email))
                queryFilter = queryFilter.Where(_ => _.Email.Contains(filters.Email));

            if (!string.IsNullOrEmpty(filters.Endereco))
                queryFilter = queryFilter.Where(_ => _.Endereco.Contains(filters.Endereco));

            if (!string.IsNullOrEmpty(filters.Endereco))
                queryFilter = queryFilter.Where(_ => _.Endereco.Contains(filters.Endereco));

            if (!string.IsNullOrEmpty(filters.EnderecoComplemento))
                queryFilter = queryFilter.Where(_ => _.EnderecoComplemento.Contains(filters.EnderecoComplemento));

            if (!string.IsNullOrEmpty(filters.EnderecoComplemento))
                queryFilter = queryFilter.Where(_ => _.EnderecoComplemento.Contains(filters.EnderecoComplemento));

            if (!string.IsNullOrEmpty(filters.EnderecoNumero))
                queryFilter = queryFilter.Where(_ => _.EnderecoNumero.Contains(filters.EnderecoNumero));

            if (!string.IsNullOrEmpty(filters.EnderecoNumero))
                queryFilter = queryFilter.Where(_ => _.EnderecoNumero.Contains(filters.EnderecoNumero));

            if (!string.IsNullOrEmpty(filters.Estado))
                queryFilter = queryFilter.Where(_ => _.Estado.Contains(filters.Estado));

            if (!string.IsNullOrEmpty(filters.Estado))
                queryFilter = queryFilter.Where(_ => _.Estado.Contains(filters.Estado));

            if (filters.EstadoCivilId != null && filters.EstadoCivilId != 0)
                queryFilter = queryFilter.Where(_ => _.EstadoCivilId == filters.EstadoCivilId);
            if (!string.IsNullOrEmpty(filters.LocalNascimento))
                queryFilter = queryFilter.Where(_ => _.LocalNascimento.Contains(filters.LocalNascimento));

            if (!string.IsNullOrEmpty(filters.LocalNascimento))
                queryFilter = queryFilter.Where(_ => _.LocalNascimento.Contains(filters.LocalNascimento));

            if (filters.NivelFormacaoId != null && filters.NivelFormacaoId != 0)
                queryFilter = queryFilter.Where(_ => _.NivelFormacaoId == filters.NivelFormacaoId);
            if (!string.IsNullOrEmpty(filters.NomeCompleto))
                queryFilter = queryFilter.Where(_ => _.NomeCompleto.Contains(filters.NomeCompleto));

            if (!string.IsNullOrEmpty(filters.NomeCompleto))
                queryFilter = queryFilter.Where(_ => _.NomeCompleto.Contains(filters.NomeCompleto));

            if (!string.IsNullOrEmpty(filters.NomeCracha))
                queryFilter = queryFilter.Where(_ => _.NomeCracha.Contains(filters.NomeCracha));

            if (!string.IsNullOrEmpty(filters.NomeCracha))
                queryFilter = queryFilter.Where(_ => _.NomeCracha.Contains(filters.NomeCracha));

            if (!string.IsNullOrEmpty(filters.NomeEmpresaOndeTrabalha))
                queryFilter = queryFilter.Where(_ => _.NomeEmpresaOndeTrabalha.Contains(filters.NomeEmpresaOndeTrabalha));

            if (!string.IsNullOrEmpty(filters.NomeEmpresaOndeTrabalha))
                queryFilter = queryFilter.Where(_ => _.NomeEmpresaOndeTrabalha.Contains(filters.NomeEmpresaOndeTrabalha));

            if (!string.IsNullOrEmpty(filters.NomeMae))
                queryFilter = queryFilter.Where(_ => _.NomeMae.Contains(filters.NomeMae));

            if (!string.IsNullOrEmpty(filters.NomeMae))
                queryFilter = queryFilter.Where(_ => _.NomeMae.Contains(filters.NomeMae));

            if (!string.IsNullOrEmpty(filters.NomePai))
                queryFilter = queryFilter.Where(_ => _.NomePai.Contains(filters.NomePai));

            if (!string.IsNullOrEmpty(filters.NomePai))
                queryFilter = queryFilter.Where(_ => _.NomePai.Contains(filters.NomePai));

            if (!string.IsNullOrEmpty(filters.NomeSocial))
                queryFilter = queryFilter.Where(_ => _.NomeSocial.Contains(filters.NomeSocial));

            if (!string.IsNullOrEmpty(filters.NomeSocial))
                queryFilter = queryFilter.Where(_ => _.NomeSocial.Contains(filters.NomeSocial));

            if (!string.IsNullOrEmpty(filters.RG))
                queryFilter = queryFilter.Where(_ => _.RG.Contains(filters.RG));

            if (!string.IsNullOrEmpty(filters.RG))
                queryFilter = queryFilter.Where(_ => _.RG.Contains(filters.RG));

            if (filters.SexoId != null && filters.SexoId != 0)
                queryFilter = queryFilter.Where(_ => _.SexoId == filters.SexoId);
            if (!string.IsNullOrEmpty(filters.TelCelular))
                queryFilter = queryFilter.Where(_ => _.TelCelular.Contains(filters.TelCelular));

            if (!string.IsNullOrEmpty(filters.TelCelular))
                queryFilter = queryFilter.Where(_ => _.TelCelular.Contains(filters.TelCelular));

            if (!string.IsNullOrEmpty(filters.TelComercial))
                queryFilter = queryFilter.Where(_ => _.TelComercial.Contains(filters.TelComercial));

            if (!string.IsNullOrEmpty(filters.TelComercial))
                queryFilter = queryFilter.Where(_ => _.TelComercial.Contains(filters.TelComercial));

            if (!string.IsNullOrEmpty(filters.TelResidencial))
                queryFilter = queryFilter.Where(_ => _.TelResidencial.Contains(filters.TelResidencial));

            if (!string.IsNullOrEmpty(filters.TelResidencial))
                queryFilter = queryFilter.Where(_ => _.TelResidencial.Contains(filters.TelResidencial));

            if (filters.TemWhatsApp != null)
                queryFilter = queryFilter.Where(_ => _.TemWhatsApp == filters.TemWhatsApp);
            if (!string.IsNullOrEmpty(filters.URLFotoAluno))
                queryFilter = queryFilter.Where(_ => _.URLFotoAluno.Contains(filters.URLFotoAluno));

            if (!string.IsNullOrEmpty(filters.URLFotoAluno))
                queryFilter = queryFilter.Where(_ => _.URLFotoAluno.Contains(filters.URLFotoAluno));

            if (!string.IsNullOrEmpty(filters.URLInstagran))
                queryFilter = queryFilter.Where(_ => _.URLInstagran.Contains(filters.URLInstagran));

            if (!string.IsNullOrEmpty(filters.URLInstagran))
                queryFilter = queryFilter.Where(_ => _.URLInstagran.Contains(filters.URLInstagran));

            if (!string.IsNullOrEmpty(filters.URLLinkedin))
                queryFilter = queryFilter.Where(_ => _.URLLinkedin.Contains(filters.URLLinkedin));

            if (!string.IsNullOrEmpty(filters.URLLinkedin))
                queryFilter = queryFilter.Where(_ => _.URLLinkedin.Contains(filters.URLLinkedin));

            if (filters.UsuarioAlteracaoId != null && filters.UsuarioAlteracaoId != 0)
                queryFilter = queryFilter.Where(_ => _.UsuarioAlteracaoId == filters.UsuarioAlteracaoId);
            if (filters.UsuarioCadastroId != null && filters.UsuarioCadastroId != 0)
                queryFilter = queryFilter.Where(_ => _.UsuarioCadastroId == filters.UsuarioCadastroId);
            if (filters.UsuarioId != null && filters.UsuarioId != 0)
                queryFilter = queryFilter.Where(_ => _.UsuarioId == filters.UsuarioId);



            return queryFilter;
        }

        public static IQueryable<Aluno> WithCustomFilters(this IQueryable<Aluno> queryBase, AlunoFilter filters)
        {
            var queryFilter = queryBase;



            return queryFilter;
        }

        public static IQueryable<Aluno> OrderByDomain(this IQueryable<Aluno> queryBase, AlunoFilter filters)
        {
           if (!string.IsNullOrEmpty(filters.OrderBy))
            {
               if (filters.OrderByType == OrderByType.OrderByDescending)
                  return queryBase.AsQueryable().OrderBy(filters.OrderBy + " DESC");
               else if (filters.OrderByType == OrderByType.OrderBy)
                  return queryBase.AsQueryable().OrderBy(filters.OrderBy + " ASC");
            }
            return queryBase;
        }

        

        public static IQueryable<T> OrderByPropertyAscending<T>(this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return source;
            }
            var paramterExpression = Expression.Parameter(typeof(T));
            var orderByProperty = Expression.Property(paramterExpression, propertyName);
            var lambda = Expression.Lambda(orderByProperty, paramterExpression);
            var genericMethod = OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            var ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }

        private static string DefinePropertyName(string[] propertyName)
        {
            var _propertyName = propertyName.LastOrDefault();
            var _parentProperty = _propertyName.Split('.')[0];
            return _parentProperty;
        }
    }
}
