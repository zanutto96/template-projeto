# Code Generator MCP Server

Este é um servidor MCP (Model Context Protocol) que replica as funcionalidades do projeto Gerador em C#, permitindo gerar código backend e frontend a partir de estruturas de banco de dados SQL Server e MySQL.

## 🎯 Funcionalidades

- ✅ **Geração de Código Backend**: Controllers, Services, Repositories, Entities
- ✅ **Geração de Código Frontend**: Componentes Angular completos com CRUD
- ✅ **Suporte Dual**: SQL Server e MySQL
- ✅ **Detecção Automática**: Identifica o tipo de banco pela connection string
- ✅ **Organização Flexível**: Por grupos (tipo) ou por tabela
- ✅ **Validação**: Valida connection strings e estruturas
- ✅ **Templates de Qualidade**: Mesma estrutura do projeto original

## 📦 Instalação

```bash
# Instalar dependências
npm install

# Build do projeto
npm run build

# Executar em desenvolvimento
npm run dev
```

## 🔧 Configuração no VS Code

Adicione esta configuração no seu settings.json do VS Code para usar o servidor MCP:

```json
{
  "mcp.servers": {
    "code-generator": {
      "command": "node",
      "args": ["dist/index.js"],
      "cwd": "caminho/para/este/projeto/node"
    }
  }
}
```

## 🚀 Como Usar no Copilot

### 1. Gerar Código Completo

```
@code-generator generate code from my SQL Server database with connection string "Data Source=localhost;Initial Catalog=MyDB;User ID=user;Password=pass;" and output to "./generated-code" for tables "Users, Products"
```

### 2. Listar Tabelas do Banco

```
@code-generator list all tables in my MySQL database "Server=localhost;Database=mydb;Uid=root;Pwd=password;"
```

### 3. Analisar Estrutura de Tabela

```
@code-generator get structure of table "Users" from "Server=localhost;Database=mydb;Uid=root;Pwd=pass;"
```

### 4. Validar Connection String

```
@code-generator validate connection string "Data Source=server;Initial Catalog=db;User ID=user;Password=pass;"
```

## 📋 Exemplos de Connection Strings

### SQL Server
```
Data Source=.\\SQLEXPRESS;Initial Catalog=MyDB;User ID=api;Password=api;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=True
```

### MySQL
```
Server=localhost;Database=MyDB;Uid=api;Pwd=api;
```

## 🏗️ Estrutura de Código Gerada

### Backend (.NET)
- **Controllers**: API RESTful completa
- **Services**: Camada de negócio
- **Repositories**: Acesso a dados com Dapper
- **Entities**: Modelos de domínio
- **Filters**: Filtros de consulta
- **Configurations**: Configurações Entity Framework
- **Validations**: Validações de negócio

### Frontend (Angular)
- **Components**: Componentes principais
- **List Components**: Listagem com filtros
- **Register Components**: Formulários de cadastro/edição
- **Services**: Serviços HTTP
- **Modules**: Módulos Angular
- **Routing**: Configuração de rotas
- **Templates**: HTML com Material Design
- **Styles**: CSS/SCSS

## 🎨 Modos de Organização

### Grouped (Padrão)
Organiza arquivos por tipo de funcionalidade:
```
/Controllers
/Services/Business
/Services/Validation
/Domain/Entities
/Domain/Filters
/Data/Repository
/SPAUI/src/app/main
```

### By Table
Organiza arquivos por tabela:
```
/Users
  - User.cs
  - UserController.cs
  - UserService.cs
  - UserRepository.cs
/Products
  - Product.cs
  - ProductController.cs
  - ProductService.cs
  - ProductRepository.cs
```

## 📊 Mapeamento de Tipos

### SQL Server → C#
- `varchar`, `nvarchar`, `char`, `text` → `string`
- `int`, `smallint` → `int`
- `bigint` → `long`
- `decimal`, `numeric` → `decimal`
- `float`, `real` → `double`
- `bit` → `bool`
- `datetime`, `date` → `DateTime`

### MySQL → C#
- `varchar`, `text`, `longtext`, `json` → `string`
- `int`, `mediumint` → `int`
- `bigint` → `long`
- `decimal`, `numeric` → `decimal`
- `float`, `double` → `double`
- `boolean`, `tinyint` → `bool`
- `datetime`, `timestamp` → `DateTime`

## 🔍 Ferramentas Disponíveis

1. **generate_code_from_database**: Gera código completo
2. **list_database_tables**: Lista tabelas do banco
3. **get_table_structure**: Analisa estrutura de tabela
4. **validate_connection_string**: Valida connection string

## 📝 Arquivo de Instruções

Cada geração cria um arquivo `INSTRUCTIONS.md` com:
- Configurações de dependency injection
- Configurações de routing (Angular)
- Configurações de DbContext (Entity Framework)
- Próximos passos para integração

## 🛠️ Desenvolvimento

```bash
# Modo watch para desenvolvimento
npm run watch

# Build
npm run build

# Executar
npm start
```

## 📚 Dependências

- `@modelcontextprotocol/sdk`: SDK do protocolo MCP
- `mssql`: Driver SQL Server
- `mysql2`: Driver MySQL
- `zod`: Validação de schemas

## 🎯 Compatibilidade

- ✅ SQL Server 2012+
- ✅ MySQL 5.7+
- ✅ MariaDB 10.2+
- ✅ Entity Framework Core
- ✅ Angular 12+
- ✅ .NET 6+

## 🤝 Contribuição

Este projeto replica as funcionalidades do Gerador original em C#, mantendo a mesma qualidade e estrutura de código.

---

**Feito com 💙 para integração com GitHub Copilot**