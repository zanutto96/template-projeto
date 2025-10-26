# Service Generation Instructions

## 🎯 Objetivo
Gerar serviços .NET com regras de negócio baseado na estrutura de tabela fornecida.

## 📝 Template de Entrada
```
Table: [NomeTabela]
Entity: [NomeEntidade]
Business Rules: [RegraNegocios]
```

## 🏗️ Estrutura do Service

### Interface do Service
```csharp
using [NomeProjeto].Model;

namespace [NomeProjeto].Service
{
    public interface I[NomeEntidade]Service
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

### Implementação do Service
```csharp
using [NomeProjeto].Model;
using [NomeProjeto].Repository;

namespace [NomeProjeto].Service
{
    public class [NomeEntidade]Service : I[NomeEntidade]Service
    {
        private readonly I[NomeEntidade]Repository _repository;

        public [NomeEntidade]Service(I[NomeEntidade]Repository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<[NomeEntidade]>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<[NomeEntidade]?> GetByIdAsync([TipoPK] id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<[NomeEntidade]> CreateAsync([NomeEntidade] entity)
        {
            // Validações de negócio aqui
            await ValidateForCreateAsync(entity);
            
            // Definir campos automáticos
            entity.DataCriacao = DateTime.Now;
            
            return await _repository.CreateAsync(entity);
        }

        public async Task<[NomeEntidade]> UpdateAsync([NomeEntidade] entity)
        {
            // Validações de negócio aqui
            await ValidateForUpdateAsync(entity);
            
            // Definir campos automáticos
            entity.DataAtualizacao = DateTime.Now;
            
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync([TipoPK] id)
        {
            // Validações de negócio para exclusão
            await ValidateForDeleteAsync(id);
            
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync([TipoPK] id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task<IEnumerable<[NomeEntidade]>> GetByFilterAsync([NomeEntidade]Filter filter)
        {
            return await _repository.GetByFilterAsync(filter);
        }

        public async Task<int> CountAsync([NomeEntidade]Filter filter)
        {
            return await _repository.CountAsync(filter);
        }

        #region Private Methods

        private async Task ValidateForCreateAsync([NomeEntidade] entity)
        {
            // Implementar validações específicas de criação
        }

        private async Task ValidateForUpdateAsync([NomeEntidade] entity)
        {
            // Implementar validações específicas de atualização
        }

        private async Task ValidateForDeleteAsync([TipoPK] id)
        {
            // Implementar validações específicas de exclusão
        }

        #endregion
    }
}
```

## 📋 Exemplo Completo

### Entrada:
```
Table: Usuario
Entity: Usuario
Primary Key: int Id
Business Rules:
- Email deve ser único
- Nome é obrigatório
- Não pode excluir usuário com pedidos
```

### Interface Gerada:
```csharp
using MinhaAplicacao.Model;

namespace MinhaAplicacao.Service
{
    public interface IUsuarioService
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

### Implementação com Regras de Negócio:
```csharp
using MinhaAplicacao.Model;
using MinhaAplicacao.Repository;

namespace MinhaAplicacao.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Usuario> CreateAsync(Usuario entity)
        {
            await ValidateForCreateAsync(entity);
            
            entity.DataCriacao = DateTime.Now;
            entity.Ativo = true;
            
            return await _repository.CreateAsync(entity);
        }

        public async Task<Usuario> UpdateAsync(Usuario entity)
        {
            await ValidateForUpdateAsync(entity);
            
            entity.DataAtualizacao = DateTime.Now;
            
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await ValidateForDeleteAsync(id);
            
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task<IEnumerable<Usuario>> GetByFilterAsync(UsuarioFilter filter)
        {
            return await _repository.GetByFilterAsync(filter);
        }

        public async Task<int> CountAsync(UsuarioFilter filter)
        {
            return await _repository.CountAsync(filter);
        }

        #region Private Methods

        private async Task ValidateForCreateAsync(Usuario entity)
        {
            if (string.IsNullOrEmpty(entity.Nome))
                throw new ArgumentException("Nome é obrigatório");

            if (string.IsNullOrEmpty(entity.Email))
                throw new ArgumentException("Email é obrigatório");

            // Verificar se email é único
            var existingUser = await _repository.GetByFilterAsync(
                new UsuarioFilter { Email = entity.Email });
            
            if (existingUser.Any())
                throw new ArgumentException("Email já está em uso");
        }

        private async Task ValidateForUpdateAsync(Usuario entity)
        {
            if (string.IsNullOrEmpty(entity.Nome))
                throw new ArgumentException("Nome é obrigatório");

            if (string.IsNullOrEmpty(entity.Email))
                throw new ArgumentException("Email é obrigatório");

            // Verificar se email é único (exceto para o próprio usuário)
            var existingUser = await _repository.GetByFilterAsync(
                new UsuarioFilter { Email = entity.Email });
            
            if (existingUser.Any(u => u.Id != entity.Id))
                throw new ArgumentException("Email já está em uso");
        }

        private async Task ValidateForDeleteAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado");

            // Verificar se usuário tem pedidos (regra de negócio)
            // Implementar verificação de relacionamentos
        }

        #endregion
    }
}
```

## 🎯 Regras de Geração

### Campos Automáticos
Baseado na estrutura da tabela:

- **DataCriacao**: Definir `DateTime.Now` no Create
- **DataAtualizacao**: Definir `DateTime.Now` no Update  
- **UsuarioCriacao**: Definir usuário atual no Create
- **UsuarioAtualizacao**: Definir usuário atual no Update
- **Ativo**: Definir `true` por padrão no Create

### Validações Comuns

#### Campos Obrigatórios
```csharp
if (string.IsNullOrEmpty(entity.Nome))
    throw new ArgumentException("Nome é obrigatório");
```

#### Unicidade
```csharp
var existing = await _repository.GetByFilterAsync(
    new Filter { Email = entity.Email });
if (existing.Any())
    throw new ArgumentException("Email já está em uso");
```

#### Relacionamentos
```csharp
// Verificar se FK existe
if (!await _relatedRepository.ExistsAsync(entity.RelatedId))
    throw new ArgumentException("Registro relacionado não encontrado");
```

### Regras de Negócio por Tipo

#### Email
- Sempre validar formato
- Verificar unicidade se aplicável
- Normalizar (ToLower)

#### CPF/CNPJ
- Validar formato
- Verificar unicidade
- Remover formatação

#### Telefone
- Normalizar formato
- Validar se necessário

#### Datas
- Validar ranges
- Não permitir datas futuras se aplicável

## 🔄 Padrões Avançados

### Transações
```csharp
using var transaction = await _context.Database.BeginTransactionAsync();
try
{
    // Operações
    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync();
    throw;
}
```

### Caching
```csharp
private readonly IMemoryCache _cache;

public async Task<Usuario?> GetByIdAsync(int id)
{
    var cacheKey = $"usuario_{id}";
    if (_cache.TryGetValue(cacheKey, out Usuario cachedUser))
        return cachedUser;

    var user = await _repository.GetByIdAsync(id);
    if (user != null)
        _cache.Set(cacheKey, user, TimeSpan.FromMinutes(30));

    return user;
}
```

### Logging
```csharp
private readonly ILogger<UsuarioService> _logger;

public async Task<Usuario> CreateAsync(Usuario entity)
{
    _logger.LogInformation("Criando usuário: {Email}", entity.Email);
    
    try
    {
        var result = await _repository.CreateAsync(entity);
        _logger.LogInformation("Usuário criado com sucesso: {Id}", result.Id);
        return result;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Erro ao criar usuário: {Email}", entity.Email);
        throw;
    }
}
```

## ✅ Checklist de Validação

- [ ] Interface e implementação criadas
- [ ] Namespace correto
- [ ] Injeção de dependência do repository
- [ ] Validações de negócio implementadas
- [ ] Campos automáticos definidos
- [ ] Tratamento de exceções apropriado
- [ ] Métodos privados de validação
- [ ] Logs quando necessário
- [ ] Transações para operações complexas
- [ ] Using statements completos