# Code Generator MCP Server

## Configuração Concluída ✅

Seu servidor MCP (Model Context Protocol) foi criado com sucesso! Agora você pode usar todas as funcionalidades do projeto Gerador diretamente no GitHub Copilot.

## Como Usar

### 1. Configuração no VS Code

O arquivo `mcp-config.json` já foi criado com a configuração correta:

```json
{
  "mcpServers": {
    "code-generator": {
      "command": "node",
      "args": ["C:\\WORK\\WILL\\Hoffman-novoch-dev\\Gerador\\node\\dist\\index.js"]
    }
  }
}
```

### 2. Ferramentas Disponíveis

Agora você pode usar estas ferramentas diretamente no Copilot:

#### 🏗️ `generate_code_from_database`
Gera código backend e frontend a partir de tabelas do banco:
- **connectionString**: String de conexão (SQL Server ou MySQL)
- **outputPath**: Caminho onde salvar o código gerado
- **selectedTables**: Array de tabelas (opcional, vazio = todas)
- **generateBackend**: true/false (padrão: true)
- **generateFrontend**: true/false (padrão: true) 
- **organizationMode**: "grouped" ou "by-table" (padrão: "grouped")

#### 📋 `list_database_tables`
Lista todas as tabelas do banco com informações:
- **connectionString**: String de conexão

#### 🔍 `get_table_structure`
Mostra estrutura detalhada de uma tabela:
- **connectionString**: String de conexão
- **tableName**: Nome da tabela

#### ✅ `validate_connection`
Testa conexão com o banco:
- **connectionString**: String de conexão

### 3. Exemplos de Uso no Copilot

```
@copilot /generate_code_from_database connectionString:"Server=localhost;Database=MyDB;Trusted_Connection=true;" outputPath:"C:/temp/generated"

@copilot /list_database_tables connectionString:"Server=localhost;Database=MyDB;Trusted_Connection=true;"

@copilot /validate_connection connectionString:"Server=localhost;Database=MyDB;Uid=user;Pwd=pass;"
```

### 4. Tipos de Banco Suportados

#### SQL Server
```
Server=localhost;Database=MyDB;Trusted_Connection=true;
Server=localhost;Database=MyDB;User ID=user;Password=pass;
```

#### MySQL
```
Server=localhost;Database=MyDB;Uid=user;Pwd=password;
Server=localhost;Port=3306;Database=MyDB;Uid=user;Pwd=pass;
```

## Funcionalidades Implementadas

✅ **Detecção Automática**: Identifica se é SQL Server ou MySQL
✅ **Geração Backend**: Controllers, Services, Repositories, Entities
✅ **Geração Frontend**: Componentes Angular (list, register, routing)
✅ **Templates Flexíveis**: Usa os mesmos templates do projeto C#
✅ **Organização de Arquivos**: Por tipo (grouped) ou por tabela (by-table)
✅ **Validação**: Testa conexão antes de gerar código
✅ **Instruções**: Gera arquivo INSTRUCTIONS.md com próximos passos

## Vantagens do MCP Server

🚀 **Integração Direta**: Funciona dentro do VS Code com Copilot
⚡ **Sem Interface Gráfica**: Comandos diretos e rápidos
🔄 **Mesma Qualidade**: Usa exatamente os mesmos templates do projeto C#
🌐 **Multiplataforma**: Roda em Windows, Mac, Linux
📝 **Histórico**: Copilot lembra das gerações anteriores

## Próximos Passos

1. **Teste a Conexão**: Use `validate_connection` primeiro
2. **Liste as Tabelas**: Use `list_database_tables` para ver o que está disponível  
3. **Gere o Código**: Use `generate_code_from_database` com suas configurações
4. **Revise o Output**: Verifique o arquivo INSTRUCTIONS.md gerado

## Status do Projeto

✅ **C# Original**: Funcionando com SQL Server + MySQL
✅ **Node.js MCP**: Funcionando e testado
✅ **Templates**: Copiados e funcionais
✅ **Banco de Dados**: Suporte dual SQL Server/MySQL
✅ **Compilação**: TypeScript compilado sem erros
✅ **Servidor**: Rodando corretamente

**🎯 Pronto para uso!** Agora você tem acesso a todas as funcionalidades do gerador de código diretamente no GitHub Copilot.