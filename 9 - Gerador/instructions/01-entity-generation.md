# Entity Generation Instructions

## 🎯 Objetivo
Gerar entidades .NET seguindo os padrões do projeto Gerador baseado na estrutura de tabela fornecida.

## 📝 Template de Entrada
```
Table: [NomeTabela]
Columns:
- [NomeColuna] ([Tipo], [Constraints])
- ...

Foreign Keys:
- [Coluna] → [TabelaReferenciada].[ColunaReferenciada]
- ...
```

## 🏗️ Estrutura da Entidade

### Padrões Básicos
- **Namespace**: `[NomeProjeto].Model`
- **Nome da Classe**: Nome da tabela em PascalCase
- **Herança**: Herdar de classes base quando apropriado
- **Atributos**: Usar Data Annotations do Entity Framework

### Template Base
```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace [NomeProjeto].Model
{
    [Table("[NomeTabela]")]
    public class [NomeEntidade]
    {
        // Propriedades aqui
    }
}
```

## 🔧 Mapeamento de Tipos

### SQL Server → C#
| SQL Server | C# | Nullable |
|------------|----|---------| 
| `int` | `int` | `int?` |
| `bigint` | `long` | `long?` |
| `varchar(n)` | `string` | `string` |
| `nvarchar(n)` | `string` | `string` |
| `datetime` | `DateTime` | `DateTime?` |
| `date` | `DateTime` | `DateTime?` |
| `bit` | `bool` | `bool?` |
| `decimal(p,s)` | `decimal` | `decimal?` |
| `float` | `double` | `double?` |
| `uniqueidentifier` | `Guid` | `Guid?` |

### MySQL → C#
| MySQL | C# | Nullable |
|-------|----|---------| 
| `int` | `int` | `int?` |
| `bigint` | `long` | `long?` |
| `varchar(n)` | `string` | `string` |
| `text` | `string` | `string` |
| `datetime` | `DateTime` | `DateTime?` |
| `date` | `DateTime` | `DateTime?` |
| `tinyint(1)` | `bool` | `bool?` |
| `decimal(p,s)` | `decimal` | `decimal?` |
| `double` | `double` | `double?` |
| `char(36)` | `Guid` | `Guid?` |

## 🏷️ Data Annotations

### Chave Primária
```csharp
[Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Se auto-increment
public int Id { get; set; }
```

### Propriedades de String
```csharp
[Required]
[StringLength(100)]
public string Nome { get; set; }

[StringLength(255)]
public string? Descricao { get; set; } // Nullable
```

### Propriedades de Data
```csharp
[Required]
public DateTime DataCriacao { get; set; }

public DateTime? DataAtualizacao { get; set; } // Nullable
```

### Chaves Estrangeiras
```csharp
[ForeignKey("Perfil")]
public int PerfilId { get; set; }
public virtual Perfil Perfil { get; set; }
```

## 📋 Exemplo Completo

### Entrada:
```
Table: Usuario
Columns:
- Id (int, PK, Identity, Not Null)
- Nome (varchar(100), Not Null)
- Email (varchar(255), Not Null)
- DataCriacao (datetime, Not Null)
- DataAtualizacao (datetime, Null)
- Ativo (bit, Not Null, Default: true)
- PerfilId (int, Not Null, FK → Perfil.Id)

Foreign Keys:
- PerfilId → Perfil.Id
```

### Saída:
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

## 🎯 Regras de Geração

1. **Sempre incluir** using statements necessários
2. **Sempre usar** atributo `[Table]` com nome da tabela
3. **Propriedades não-nulas** devem ter `[Required]`
4. **Strings** sempre incluir `[StringLength]` se especificado
5. **Chaves primárias** sempre usar `[Key]`
6. **Identity** sempre usar `[DatabaseGenerated(DatabaseGeneratedOption.Identity)]`
7. **Chaves estrangeiras** incluir propriedade de navegação virtual
8. **Valores padrão** definir na propriedade quando possível
9. **Nullable** usar `?` para tipos value que permitem null
10. **PascalCase** para nomes de propriedades

## 🔄 Propriedades de Navegação

### One-to-Many (1:N)
```csharp
// Na entidade "Many"
[ForeignKey("Usuario")]
public int UsuarioId { get; set; }
public virtual Usuario Usuario { get; set; }

// Na entidade "One"
public virtual ICollection<Pedido> Pedidos { get; set; }
```

### Many-to-Many (N:N)
```csharp
// Entidade de junção
public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; set; }
```

## ✅ Checklist de Validação

- [ ] Namespace correto
- [ ] Nome da classe em PascalCase
- [ ] Atributo [Table] presente
- [ ] Chave primária com [Key]
- [ ] Propriedades Required marcadas
- [ ] StringLength definido para strings
- [ ] Tipos nullable corretos
- [ ] Foreign Keys com navegação
- [ ] Using statements completos
- [ ] Formatação consistente