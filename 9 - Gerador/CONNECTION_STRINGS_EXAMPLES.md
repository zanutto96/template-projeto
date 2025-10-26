# Exemplos de Connection Strings

Este gerador de código suporta tanto **SQL Server** quanto **MySQL**. A detecção do tipo de banco é feita automaticamente baseada na connection string.

## SQL Server

### SQL Server com autenticação SQL
```
Data Source=servidor;Initial Catalog=nome_banco;user id=usuario;password=senha;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=True
```

### SQL Server Express local
```
Data Source=.\SQLEXPRESS;Initial Catalog=MeuBanco;user id=api;password=api;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=True
```

### SQL Server com autenticação Windows
```
Data Source=servidor;Initial Catalog=nome_banco;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=True
```

## MySQL

### MySQL local
```
Server=localhost;Database=nome_banco;Uid=usuario;Pwd=senha;
```

### MySQL com porta específica
```
Server=localhost;Port=3306;Database=nome_banco;Uid=usuario;Pwd=senha;
```

### MySQL remoto
```
Server=192.168.1.100;Database=nome_banco;Uid=usuario;Pwd=senha;
```

### MySQL com SSL
```
Server=servidor;Database=nome_banco;Uid=usuario;Pwd=senha;SslMode=Required;
```

## Como o sistema detecta o tipo de banco:

- **SQL Server**: Connection strings que contêm "Data Source" e "Initial Catalog"
- **MySQL**: Connection strings que contêm "Server=" ou "Host="

## Notas importantes:

1. O gerador extrai automaticamente o nome do banco da connection string para criar o diretório de destino
2. Todas as funcionalidades de geração (Backend e Frontend) funcionam independente do tipo de banco
3. Os templates gerados são compatíveis com Entity Framework Core para ambos os bancos