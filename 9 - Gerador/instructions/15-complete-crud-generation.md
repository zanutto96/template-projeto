# Complete CRUD Generation Instructions

## 🎯 Objetivo
Guia completo para gerar um CRUD completo baseado apenas na estrutura da tabela fornecida.

## 📝 Como Usar Este Guia

### Passo 1: Forneça a Estrutura da Tabela
```
Tabela: Usuario
Colunas:
- Id (int, PK, Identity, Not Null)
- Nome (varchar(100), Not Null)
- Email (varchar(255), Not Null, Unique)
- DataCriacao (datetime, Not Null)
- DataAtualizacao (datetime, Null)
- Ativo (bit, Not Null, Default: true)
- PerfilId (int, Not Null, FK → Perfil.Id)

Chaves Estrangeiras:
- PerfilId → Perfil.Id

Regras de Negócio:
- Email deve ser único
- Nome é obrigatório
- Não pode excluir usuário com pedidos associados
```

### Passo 2: Solicite a Geração Completa
```
@copilot Gere um CRUD completo para a tabela Usuario com a estrutura acima.
Inclua: Entity, Repository, Service, Controller, Componentes Angular (list e register), Service Angular, Routing e Module.
```

## 🏗️ Estrutura Completa Gerada

### Backend (.NET)
1. **Entity** (`Usuario.cs`)
2. **Repository Interface** (`IUsuarioRepository.cs`)
3. **Repository Implementation** (`UsuarioRepository.cs`)
4. **Service Interface** (`IUsuarioService.cs`)
5. **Service Implementation** (`UsuarioService.cs`)
6. **Controller** (`UsuarioController.cs`)
7. **Filter** (`UsuarioFilter.cs`)
8. **Validation** (`UsuarioValidation.cs`)

### Frontend (Angular)
1. **Model** (`usuario.model.ts`)
2. **Filter Model** (`usuario-filter.model.ts`)
3. **Service** (`usuario.service.ts`)
4. **List Component** (`usuario-list.component.ts/html/css`)
5. **Register Component** (`usuario-register.component.ts/html/css`)
6. **Routing** (`usuario-routing.module.ts`)
7. **Module** (`usuario.module.ts`)

## 📋 Exemplo de Saída Esperada

### 1. Entity
```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaAplicacao.Model
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        [Required]
        public bool Ativo { get; set; } = true;

        [ForeignKey("Perfil")]
        public int PerfilId { get; set; }
        
        public virtual Perfil Perfil { get; set; }
    }
}
```

### 2. Repository
```csharp
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
```

### 3. Service
```csharp
public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<Usuario> CreateAsync(Usuario entity)
    {
        await ValidateForCreateAsync(entity);
        
        entity.DataCriacao = DateTime.Now;
        entity.Ativo = true;
        
        return await _repository.CreateAsync(entity);
    }
    
    // ... outros métodos
}
```

### 4. Controller
```csharp
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
    
    // ... outros endpoints
}
```

### 5. Angular Model
```typescript
export interface Usuario {
  id: number;
  nome: string;
  email: string;
  dataCriacao: Date;
  dataAtualizacao?: Date;
  ativo: boolean;
  perfilId: number;
  perfil?: Perfil;
}

export interface UsuarioFilter {
  nome?: string;
  email?: string;
  ativo?: boolean;
  dataCriacaoInicio?: Date;
  dataCriacaoFim?: Date;
  perfilId?: number;
}
```

### 6. Angular Service
```typescript
@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private apiUrl = `${environment.apiUrl}/usuario`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(this.apiUrl);
  }

  getById(id: number): Observable<Usuario> {
    return this.http.get<Usuario>(`${this.apiUrl}/${id}`);
  }

  create(usuario: Usuario): Observable<Usuario> {
    return this.http.post<Usuario>(this.apiUrl, usuario);
  }

  update(usuario: Usuario): Observable<Usuario> {
    return this.http.put<Usuario>(`${this.apiUrl}/${usuario.id}`, usuario);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getByFilter(filter: UsuarioFilter): Observable<Usuario[]> {
    return this.http.post<Usuario[]>(`${this.apiUrl}/filter`, filter);
  }

  count(filter: UsuarioFilter): Observable<number> {
    return this.http.post<number>(`${this.apiUrl}/count`, filter);
  }
}
```

## 🎯 Comandos Específicos para o Copilot

### Gerar Backend Completo
```
@copilot Com base na estrutura da tabela Usuario:
- Id (int, PK, Identity)
- Nome (varchar(100), Not Null)
- Email (varchar(255), Not Null, Unique)
- DataCriacao (datetime, Not Null)
- Ativo (bit, Not Null)

Gere: Entity, Repository (interface e implementação), Service (interface e implementação), Controller com todos os endpoints CRUD.
```

### Gerar Frontend Completo
```
@copilot Com base na entidade Usuario com campos: id, nome, email, dataCriacao, ativo:

Gere um módulo Angular completo com:
- Model e Filter
- Service com todos os métodos HTTP
- Componente de listagem com filtros e paginação
- Componente de cadastro/edição com validações
- Routing e Module
```

### Gerar Apenas Componente de Lista
```
@copilot Gere um componente Angular de listagem para Usuario com:
- Campos para exibir: nome, email, dataCriacao, ativo
- Filtros: nome (texto), email (texto), ativo (select), data criação (range)
- Paginação, loading, confirmação de exclusão
```

### Gerar Apenas Componente de Cadastro
```
@copilot Gere um componente Angular de cadastro/edição para Usuario com:
- Campos: nome (required), email (required, email), ativo (checkbox)
- Validações completas
- Máscaras e formatações
- Save/cancel com confirmação
```

## 🔄 Fluxo de Trabalho Recomendado

### 1. Preparação
- Defina a estrutura da tabela completa
- Identifique regras de negócio específicas
- Liste campos obrigatórios e validações

### 2. Backend First
- Gere Entity primeiro
- Depois Repository
- Em seguida Service
- Por último Controller

### 3. Frontend Second
- Gere Models
- Depois Service Angular
- Componente de listagem
- Componente de cadastro
- Routing e Module

### 4. Teste e Refinamento
- Teste cada componente individualmente
- Ajuste validações conforme necessário
- Refine UI/UX

## ✅ Checklist Final

### Backend
- [ ] Entity com todas as anotações
- [ ] Repository com interface e implementação
- [ ] Service com regras de negócio
- [ ] Controller com todos os endpoints
- [ ] Validações implementadas
- [ ] Tratamento de erros

### Frontend
- [ ] Models TypeScript tipados
- [ ] Service com todos os métodos HTTP
- [ ] Componente de listagem funcional
- [ ] Componente de cadastro com validações
- [ ] Routing configurado
- [ ] Module com todas as dependências

### Integração
- [ ] APIs funcionando corretamente
- [ ] Frontend consumindo APIs
- [ ] Validações client e server-side
- [ ] Error handling em ambos os lados
- [ ] Loading states implementados
- [ ] Confirmações de ações destrutivas

## 💡 Dicas Importantes

1. **Sempre forneça a estrutura completa** da tabela com tipos, constraints e relacionamentos
2. **Especifique regras de negócio** explicitamente
3. **Use nomes consistentes** entre backend e frontend
4. **Teste cada camada** individualmente antes de integrar
5. **Mantenha padrões** de nomenclatura e estrutura
6. **Documente alterações** e customizações específicas

Com essas instruções, qualquer modelo de IA pode gerar código consistente e funcional seguindo os mesmos padrões do projeto Gerador original.