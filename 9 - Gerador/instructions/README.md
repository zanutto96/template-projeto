# Code Generation Instructions for AI Models

Este conjunto de instruções ensina modelos de IA (como GitHub Copilot) a gerar código backend e frontend seguindo os mesmos padrões do projeto Gerador, usando apenas a estrutura da tabela como entrada.

## 📋 Como Usar

1. **Forneça a estrutura da tabela** no formato especificado
2. **Escolha o tipo de componente** que deseja gerar
3. **O modelo gerará** código seguindo os padrões estabelecidos

## 📁 Arquivos de Instrução

### Backend (.NET)
- `01-entity-generation.md` - Como gerar entidades
- `02-repository-generation.md` - Como gerar repositórios
- `03-service-generation.md` - Como gerar serviços
- `04-controller-generation.md` - Como gerar controllers
- `05-validation-generation.md` - Como gerar validações
- `06-configuration-generation.md` - Como gerar configurações

### Frontend (Angular)
- `07-component-generation.md` - Como gerar componentes básicos
- `08-list-component-generation.md` - Como gerar componentes de listagem
- `09-register-component-generation.md` - Como gerar componentes de cadastro
- `10-service-frontend-generation.md` - Como gerar serviços Angular
- `11-routing-generation.md` - Como gerar rotas
- `12-module-generation.md` - Como gerar módulos

### Utilitários
- `13-filter-generation.md` - Como gerar filtros
- `14-extensions-generation.md` - Como gerar extensões
- `15-complete-crud-generation.md` - Como gerar CRUD completo

## 🎯 Formato de Entrada Padrão

```json
{
  "tableName": "Usuario",
  "columns": [
    {
      "name": "Id",
      "type": "int",
      "isPrimaryKey": true,
      "isIdentity": true,
      "isNullable": false
    },
    {
      "name": "Nome",
      "type": "varchar",
      "maxLength": 100,
      "isNullable": false
    },
    {
      "name": "Email",
      "type": "varchar",
      "maxLength": 255,
      "isNullable": false
    },
    {
      "name": "DataCriacao",
      "type": "datetime",
      "isNullable": false
    },
    {
      "name": "Ativo",
      "type": "bit",
      "isNullable": false,
      "defaultValue": true
    }
  ],
  "foreignKeys": [
    {
      "column": "PerfilId",
      "referencedTable": "Perfil",
      "referencedColumn": "Id"
    }
  ]
}
```

## 🚀 Exemplo de Uso

```
Gere uma entidade para a seguinte tabela:

Table: Usuario
Columns:
- Id (int, PK, Identity, Not Null)
- Nome (varchar(100), Not Null)
- Email (varchar(255), Not Null)
- DataCriacao (datetime, Not Null)
- Ativo (bit, Not Null, Default: true)

Foreign Keys:
- PerfilId → Perfil.Id
```

O modelo irá gerar automaticamente código seguindo os padrões estabelecidos nos arquivos de instrução.

## 🎨 Padrões Seguidos

- **Nomenclatura**: PascalCase para classes e propriedades, camelCase para variáveis
- **Estrutura**: Separação clara de responsabilidades (Entity, Repository, Service, Controller)
- **Validação**: FluentValidation para backend, validações Angular para frontend
- **Tipagem**: Uso correto de tipos .NET e TypeScript
- **Convenções**: Seguindo padrões estabelecidos no projeto original

## 📚 Benefícios

✅ **Consistência**: Mesmo padrão do gerador automático
✅ **Flexibilidade**: Apenas com prompts de texto
✅ **Rapidez**: Geração instantânea via Copilot
✅ **Aprendizado**: Modelo aprende os padrões do projeto
✅ **Manutenção**: Fácil de atualizar e melhorar