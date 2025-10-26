# Repository Generation Instructions

## 🎯 Objetivo
Gerar repositórios .NET seguindo o padrão Repository com Entity Framework baseado na estrutura de tabela fornecida.

## 📝 Template de Entrada
```
Table: [NomeTabela]
Entity: [NomeEntidade]
Primary Key: [TipoPK] [NomePK]
```

## 🏗️ Estrutura do Repository

### Interface do Repository
```csharp
using [NomeProjeto].Model;

namespace [NomeProjeto].Repository
{
    public interface I[NomeEntidade]Repository
    {
        Task<IEnumerable<[NomeEntidade]>> GetAllAsync();
        Task<[NomeEntidade]?> GetByIdAsync([TipoPK] id);
        Task<[NomeEntidade]> CreateAsync([NomeEntidade] entity);
        Task<[NomeEntidade]> UpdateAsync([NomeEntidade] entity);
        Task<bool> DeleteAsync([TipoPK] id);
        Task<bool> ExistsAsync([TipoPK] id);
        Task<IEnumerable<[NomeEntidade]>> GetByFilterAsync([NomeEntidade]Filter filter);
        Task<int> CountAsync([NomeEntidade]Filter filter);
    }
}
```

### Implementação do Repository
```csharp
using Microsoft.EntityFrameworkCore;
using [NomeProjeto].Model;

namespace [NomeProjeto].Repository
{
    public class [NomeEntidade]Repository : I[NomeEntidade]Repository
    {
        private readonly ApplicationDbContext _context;

        public [NomeEntidade]Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<[NomeEntidade]>> GetAllAsync()
        {
            return await _context.[NomeEntidade]s
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<[NomeEntidade]?> GetByIdAsync([TipoPK] id)
        {
            return await _context.[NomeEntidade]s
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.[NomePK] == id);
        }

        public async Task<[NomeEntidade]> CreateAsync([NomeEntidade] entity)
        {
            _context.[NomeEntidade]s.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<[NomeEntidade]> UpdateAsync([NomeEntidade] entity)
        {
            _context.[NomeEntidade]s.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync([TipoPK] id)
        {
            var entity = await _context.[NomeEntidade]s
                .FirstOrDefaultAsync(x => x.[NomePK] == id);
            
            if (entity == null)
                return false;

            _context.[NomeEntidade]s.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync([TipoPK] id)
        {
            return await _context.[NomeEntidade]s
                .AsNoTracking()
                .AnyAsync(x => x.[NomePK] == id);
        }

        public async Task<IEnumerable<[NomeEntidade]>> GetByFilterAsync([NomeEntidade]Filter filter)
        {
            var query = _context.[NomeEntidade]s.AsNoTracking();

            // Aplicar filtros aqui baseado na estrutura da tabela

            return await query.ToListAsync();
        }

        public async Task<int> CountAsync([NomeEntidade]Filter filter)
        {
            var query = _context.[NomeEntidade]s.AsNoTracking();

            // Aplicar mesmos filtros do GetByFilterAsync

            return await query.CountAsync();
        }
    }
}
```

## 📋 Exemplo Completo

### Entrada:
```
Table: Usuario
Entity: Usuario
Primary Key: int Id
Columns:
- Id (int, PK, Identity)
- Nome (varchar(100), Not Null)
- Email (varchar(255), Not Null)
- DataCriacao (datetime, Not Null)
- Ativo (bit, Not Null)
```

### Interface Gerada:
```csharp
using MinhaAplicacao.Model;

namespace MinhaAplicacao.Repository
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario> CreateAsync(Usuario entity);
        Task<Usuario> UpdateAsync(Usuario entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Usuario>> GetByFilterAsync(UsuarioFilter filter);
        Task<int> CountAsync(UsuarioFilter filter);
    }
}
```

### Implementação Gerada:
```csharp
using Microsoft.EntityFrameworkCore;
using MinhaAplicacao.Model;

namespace MinhaAplicacao.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario> CreateAsync(Usuario entity)
        {
            _context.Usuarios.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Usuario> UpdateAsync(Usuario entity)
        {
            _context.Usuarios.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (entity == null)
                return false;

            _context.Usuarios.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Usuario>> GetByFilterAsync(UsuarioFilter filter)
        {
            var query = _context.Usuarios.AsNoTracking();

            if (!string.IsNullOrEmpty(filter.Nome))
                query = query.Where(x => x.Nome.Contains(filter.Nome));

            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(x => x.Email.Contains(filter.Email));

            if (filter.Ativo.HasValue)
                query = query.Where(x => x.Ativo == filter.Ativo.Value);

            if (filter.DataCriacaoInicio.HasValue)
                query = query.Where(x => x.DataCriacao >= filter.DataCriacaoInicio.Value);

            if (filter.DataCriacaoFim.HasValue)
                query = query.Where(x => x.DataCriacao <= filter.DataCriacaoFim.Value);

            return await query.ToListAsync();
        }

        public async Task<int> CountAsync(UsuarioFilter filter)
        {
            var query = _context.Usuarios.AsNoTracking();

            if (!string.IsNullOrEmpty(filter.Nome))
                query = query.Where(x => x.Nome.Contains(filter.Nome));

            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(x => x.Email.Contains(filter.Email));

            if (filter.Ativo.HasValue)
                query = query.Where(x => x.Ativo == filter.Ativo.Value);

            if (filter.DataCriacaoInicio.HasValue)
                query = query.Where(x => x.DataCriacao >= filter.DataCriacaoInicio.Value);

            if (filter.DataCriacaoFim.HasValue)
                query = query.Where(x => x.DataCriacao <= filter.DataCriacaoFim.Value);

            return await query.CountAsync();
        }
    }
}
```

## 🎯 Regras de Geração

### Nomes e Convenções
1. **Interface**: `I[NomeEntidade]Repository`
2. **Classe**: `[NomeEntidade]Repository`
3. **DbSet**: `[NomeEntidade]s` (plural)
4. **Namespace**: `[NomeProjeto].Repository`

### Métodos Obrigatórios
1. **GetAllAsync**: Retorna todas as entidades
2. **GetByIdAsync**: Busca por chave primária
3. **CreateAsync**: Insere nova entidade
4. **UpdateAsync**: Atualiza entidade existente
5. **DeleteAsync**: Remove entidade por ID
6. **ExistsAsync**: Verifica se entidade existe
7. **GetByFilterAsync**: Busca com filtros
8. **CountAsync**: Conta registros com filtros

### Padrões de Query
- **AsNoTracking()** para consultas read-only
- **FirstOrDefaultAsync** para busca única
- **ToListAsync()** para listas
- **AnyAsync** para verificação de existência
- **CountAsync** para contagem

### Filtros Automáticos
Baseado no tipo da coluna:
- **String**: `Contains()` para busca parcial
- **DateTime**: Range com início e fim
- **Bool**: Comparação direta
- **Numeric**: Range ou comparação direta
- **Enum**: Comparação direta

## 🔄 Relacionamentos

### Include para Navigation Properties
```csharp
public async Task<Usuario?> GetByIdAsync(int id)
{
    return await _context.Usuarios
        .Include(x => x.Perfil) // Para FKs
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);
}
```

### Soft Delete
Se a tabela tem campo `Ativo` ou `Deleted`:
```csharp
public async Task<bool> DeleteAsync(int id)
{
    var entity = await _context.Usuarios
        .FirstOrDefaultAsync(x => x.Id == id);
    
    if (entity == null)
        return false;

    entity.Ativo = false; // Soft delete
    await _context.SaveChangesAsync();
    return true;
}
```

## ✅ Checklist de Validação

- [ ] Interface e implementação criadas
- [ ] Namespace correto
- [ ] Todos os métodos obrigatórios
- [ ] AsNoTracking em consultas
- [ ] Async/await em todos os métodos
- [ ] Tratamento de nulos apropriado
- [ ] Filtros baseados na estrutura da tabela
- [ ] Include para navigation properties
- [ ] Using statements completos