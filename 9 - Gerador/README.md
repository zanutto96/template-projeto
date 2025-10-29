# Gerador de Código CRUD

Uma ferramenta CLI para gerar código CRUD completo (Backend .NET + Frontend Angular) baseada em tabelas de banco de dados.

## Funcionalidades

- Geração automática de código para **Backend .NET** (Entity Framework Core):
  - Entities
  - Controllers
  - Services
  - Repositories
  - Filters
  - Validation
  - Configuration

- Geração automática de código para **Frontend Angular**:
  - Components (list e register)
  - Services
  - Modules
  - Routing
  - Templates HTML
  - Styles SCSS

- Suporte a **SQL Server** e **MySQL**
- Detecção automática do tipo de banco pela connection string
- Arquivo de instruções para integração manual

## Instalação

1. Clone ou baixe o projeto
2. Execute `dotnet restore` para instalar dependências
3. Execute `dotnet build` para compilar

## Configuração

Edite o arquivo `appsettings.json`:

```json
{
  "FrontPath": "c:\\caminho\\para\\projeto\\frontend",
  "BackPath": "c:\\caminho\\para\\projeto\\backend",
  "ConnectionString": "Data Source=servidor;Initial Catalog=banco;user id=usuario;password=senha;"
}
```

### Caminhos

- **FrontPath**: Caminho absoluto para a pasta raiz do projeto Angular (onde está o `angular.json`)
- **BackPath**: Caminho absoluto para o diretório raiz do projeto .NET
- **ConnectionString**: String de conexão do banco de dados (SQL Server ou MySQL)

## Uso

### Gerar código para todas as tabelas

```bash
dotnet run
```

### Gerar código para uma tabela específica

```bash
dotnet run -- --table NomeDaTabela
```

### Ver ajuda

```bash
dotnet run -- --help
```

## Exemplos de Connection Strings

### SQL Server
```
Data Source=.\SQLEXPRESS;Initial Catalog=MeuBanco;user id=api;password=api;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=True
```

### MySQL
```
Server=localhost;Database=MeuBanco;Uid=usuario;Pwd=senha;
```

## Arquivos Gerados

### Backend (por tabela)
- `Controllers/{Tabela}Controller.cs`
- `Service/Business/{Tabela}Service.cs`
- `Service/Validation/{Tabela}Validation.cs`
- `Domain/Entities/{Tabela}.cs`
- `Domain/Filters/{Tabela}Filter.cs`
- `Data/BasicExtension/{Tabela}FilterBasicExtension.cs`
- `Data/Configuration/{Tabela}Configuration.cs`
- `Data/Repository/{Tabela}Repository.cs`

### Frontend (por tabela)
- `{tabela}/`
  - `{tabela}.service.ts`
  - `{tabela}.module.ts`
  - `{tabela}.component.ts`
  - `{tabela}.component.html`
  - `{tabela}.component.scss`
  - `{tabela}-routing.module.ts`
  - `{tabela}-register/`
    - `{tabela}-register.component.ts`
    - `{tabela}-register.component.html`
    - `{tabela}-register.component.scss`
  - `{tabela}-list/`
    - `{tabela}-list.component.ts`
    - `{tabela}-list.component.html`
    - `{tabela}-list.component.scss`

## Instruções de Integração

Após a geração, um arquivo `Instructions.txt` é criado com as instruções para:

1. **Frontend**: Adicionar rotas no `app.routing.module.ts`
2. **Backend**: Registrar serviços no container de DI
3. **Backend**: Adicionar DbSet no `DataContext.cs`

## Requisitos

- .NET 8.0
- Acesso a banco de dados SQL Server ou MySQL
- Projeto Angular existente (opcional para geração frontend)

## Desenvolvimento

Para modificar templates, edite os arquivos em `Templates/BackEnd/` e `Templates/FrontEnd/`.

Os templates usam placeholders como `{{TableName}}`, `{{Columns}}`, etc., que são substituídos automaticamente.</content>
<parameter name="filePath">c:\WORK\ZALMS\TEMPLATES\template-projeto\9 - Gerador\README.md