# Controller Generation Instructions

## 🎯 Objetivo
Gerar controllers ASP.NET Core Web API baseado na estrutura de tabela fornecida.

## 📝 Template de Entrada
```
Table: [NomeTabela]
Entity: [NomeEntidade]
Primary Key: [TipoPK] [NomePK]
```

## 🏗️ Estrutura do Controller

### Controller Base
```csharp
using Microsoft.AspNetCore.Mvc;
using [NomeProjeto].Model;
using [NomeProjeto].Service;

namespace [NomeProjeto].Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class [NomeEntidade]Controller : ControllerBase
    {
        private readonly I[NomeEntidade]Service _service;

        public [NomeEntidade]Controller(I[NomeEntidade]Service service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<[NomeEntidade]>>> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<[NomeEntidade]>> GetById([TipoPK] id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<[NomeEntidade]>> Create([FromBody] [NomeEntidade] entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.CreateAsync(entity);
                return CreatedAtAction(nameof(GetById), new { id = result.[NomePK] }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<[NomeEntidade]>> Update([TipoPK] id, [FromBody] [NomeEntidade] entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != entity.[NomePK])
                    return BadRequest("ID mismatch");

                var result = await _service.UpdateAsync(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([TipoPK] id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> Exists([TipoPK] id)
        {
            try
            {
                var result = await _service.ExistsAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<[NomeEntidade]>>> GetByFilter([FromBody] [NomeEntidade]Filter filter)
        {
            try
            {
                var result = await _service.GetByFilterAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("count")]
        public async Task<ActionResult<int>> Count([FromBody] [NomeEntidade]Filter filter)
        {
            try
            {
                var result = await _service.CountAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
```

### Controller Gerado:
```csharp
using Microsoft.AspNetCore.Mvc;
using MinhaAplicacao.Model;
using MinhaAplicacao.Service;

namespace MinhaAplicacao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create([FromBody] Usuario entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.CreateAsync(entity);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Update(int id, [FromBody] Usuario entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != entity.Id)
                    return BadRequest("ID mismatch");

                var result = await _service.UpdateAsync(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("exists/{id}")]
        public async Task<ActionResult<bool>> Exists(int id)
        {
            try
            {
                var result = await _service.ExistsAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetByFilter([FromBody] UsuarioFilter filter)
        {
            try
            {
                var result = await _service.GetByFilterAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("count")]
        public async Task<ActionResult<int>> Count([FromBody] UsuarioFilter filter)
        {
            try
            {
                var result = await _service.CountAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
```

## 🎯 Endpoints Gerados

### CRUD Básico
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/[controller]` | Lista todos |
| GET | `/api/[controller]/{id}` | Busca por ID |
| POST | `/api/[controller]` | Criar novo |
| PUT | `/api/[controller]/{id}` | Atualizar existente |
| DELETE | `/api/[controller]/{id}` | Excluir por ID |

### Utilitários
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/[controller]/exists/{id}` | Verificar existência |
| POST | `/api/[controller]/filter` | Buscar com filtros |
| POST | `/api/[controller]/count` | Contar registros |

## 🔧 Funcionalidades Avançadas

### Paginação
```csharp
[HttpGet("paged")]
public async Task<ActionResult<PagedResult<Usuario>>> GetPaged(
    [FromQuery] int page = 1, 
    [FromQuery] int pageSize = 10)
{
    try
    {
        var filter = new UsuarioFilter();
        var total = await _service.CountAsync(filter);
        var items = await _service.GetPagedAsync(filter, page, pageSize);
        
        var result = new PagedResult<Usuario>
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling((double)total / pageSize)
        };
        
        return Ok(result);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### Autorização
```csharp
[Authorize]
[HttpPost]
public async Task<ActionResult<Usuario>> Create([FromBody] Usuario entity)
{
    // Implementação
}

[Authorize(Roles = "Admin")]
[HttpDelete("{id}")]
public async Task<ActionResult> Delete(int id)
{
    // Implementação
}
```

### Validação Customizada
```csharp
[HttpPost]
public async Task<ActionResult<Usuario>> Create([FromBody] Usuario entity)
{
    try
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { 
                    Field = x.Key, 
                    Errors = x.Value.Errors.Select(e => e.ErrorMessage) 
                });
            return BadRequest(errors);
        }

        var result = await _service.CreateAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### Response Padronizado
```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; }
}

[HttpGet]
public async Task<ActionResult<ApiResponse<IEnumerable<Usuario>>>> GetAll()
{
    try
    {
        var result = await _service.GetAllAsync();
        return Ok(new ApiResponse<IEnumerable<Usuario>>
        {
            Success = true,
            Message = "Dados carregados com sucesso",
            Data = result
        });
    }
    catch (Exception ex)
    {
        return BadRequest(new ApiResponse<IEnumerable<Usuario>>
        {
            Success = false,
            Message = "Erro ao carregar dados",
            Errors = new List<string> { ex.Message }
        });
    }
}
```

## 🎯 Regras de Geração

### Nomenclatura
1. **Controller**: `[NomeEntidade]Controller`
2. **Route**: `api/[controller]`
3. **Namespace**: `[NomeProjeto].Controllers`

### Atributos Obrigatórios
- `[ApiController]` na classe
- `[Route("api/[controller]")]` na classe
- `[HttpGet]`, `[HttpPost]`, etc. nos métodos
- `[FromBody]` para parâmetros complexos
- `[FromQuery]` para parâmetros de URL

### Status Codes
- **200 OK**: Operação bem-sucedida
- **201 Created**: Criação bem-sucedida
- **204 No Content**: Exclusão bem-sucedida
- **400 Bad Request**: Erro de validação/negócio
- **404 Not Found**: Registro não encontrado

### Tratamento de Erros
- Always use try-catch
- Return appropriate HTTP status codes
- Include error messages
- Validate ModelState for POST/PUT

## ✅ Checklist de Validação

- [ ] Atributo [ApiController] presente
- [ ] Route configurada corretamente
- [ ] Injeção de dependência do service
- [ ] Todos os métodos CRUD implementados
- [ ] Try-catch em todos os métodos
- [ ] Status codes apropriados
- [ ] Validação de ModelState
- [ ] Parâmetros com atributos corretos
- [ ] Using statements completos
- [ ] Namespace correto