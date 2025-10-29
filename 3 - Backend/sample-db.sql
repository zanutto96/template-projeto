-- Script para criar e popular banco de dados de exemplo para academia (MariaDB)
-- Sem chaves estrangeiras, sempre drop table if exists

-- Perfis de usuário
DROP TABLE IF EXISTS Perfis;
CREATE TABLE Perfis (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(50) NOT NULL
);

INSERT INTO Perfis (Nome) VALUES ('Administrador'), ('Instrutor'), ('Aluno'), ('Manutenção');

-- Usuários
DROP TABLE IF EXISTS Usuarios;
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Senha VARCHAR(255) NOT NULL,
    PerfilId INT NOT NULL
);

INSERT INTO Usuarios (Nome, Email, Senha, PerfilId) VALUES
('João Silva', 'joao@academia.com', 'senha123', 1),
('Maria Santos', 'maria@academia.com', 'senha123', 2),
('Pedro Oliveira', 'pedro@academia.com', 'senha123', 3),
('Ana Costa', 'ana@academia.com', 'senha123', 4);

-- Alunos
DROP TABLE IF EXISTS Alunos;
CREATE TABLE Alunos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    DataNascimento DATE NOT NULL,
    UsuarioId INT NOT NULL
);

INSERT INTO Alunos (Nome, DataNascimento, UsuarioId) VALUES
('Pedro Oliveira', '1990-05-15', 3),
('Lucas Pereira', '1985-10-20', 3),  -- Assuming another student, but using same user for simplicity
('Carla Mendes', '1995-03-10', 3);

-- Funcionários
DROP TABLE IF EXISTS Funcionarios;
CREATE TABLE Funcionarios (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Cargo VARCHAR(50) NOT NULL,
    UsuarioId INT NOT NULL
);

INSERT INTO Funcionarios (Nome, Cargo, UsuarioId) VALUES
('Maria Santos', 'Instrutora', 2),
('Ana Costa', 'Técnica de Manutenção', 4),
('Carlos Lima', 'Recepcionista', 2);  -- Assuming another employee

-- Agenda de alunos
DROP TABLE IF EXISTS AgendaAlunos;
CREATE TABLE AgendaAlunos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    AlunoId INT NOT NULL,
    Data DATE NOT NULL,
    Horario TIME NOT NULL,
    Atividade VARCHAR(100) NOT NULL
);

INSERT INTO AgendaAlunos (AlunoId, Data, Horario, Atividade) VALUES
(1, '2025-10-27', '08:00:00', 'Musculação'),
(1, '2025-10-28', '10:00:00', 'Pilates'),
(2, '2025-10-27', '14:00:00', 'Natação'),
(3, '2025-10-29', '16:00:00', 'Yoga');

-- Escala de funcionários
DROP TABLE IF EXISTS EscalaFuncionarios;
CREATE TABLE EscalaFuncionarios (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    FuncionarioId INT NOT NULL,
    Data DATE NOT NULL,
    HorarioInicio TIME NOT NULL,
    HorarioFim TIME NOT NULL
);

INSERT INTO EscalaFuncionarios (FuncionarioId, Data, HorarioInicio, HorarioFim) VALUES
(1, '2025-10-27', '08:00:00', '17:00:00'),
(1, '2025-10-28', '09:00:00', '18:00:00'),
(2, '2025-10-27', '07:00:00', '16:00:00'),
(3, '2025-10-29', '10:00:00', '19:00:00');

-- Tipos de equipamentos
DROP TABLE IF EXISTS TiposEquipamentos;
CREATE TABLE TiposEquipamentos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(50) NOT NULL
);

INSERT INTO TiposEquipamentos (Nome) VALUES
('Cardio'),
('Musculação'),
('Funcional'),
('Natação');

-- Equipamentos
DROP TABLE IF EXISTS Equipamentos;
CREATE TABLE Equipamentos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    TipoId INT NOT NULL,
    DataAquisicao DATE NOT NULL
);

INSERT INTO Equipamentos (Nome, TipoId, DataAquisicao) VALUES
('Esteira', 1, '2023-01-15'),
('Bicicleta Ergométrica', 1, '2023-02-20'),
('Halteres', 2, '2022-11-10'),
('Máquina de Remo', 3, '2024-05-05'),
('Raia de Natação', 4, '2020-08-30');

-- Manutenções
DROP TABLE IF EXISTS Manutencoes;
CREATE TABLE Manutencoes (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    EquipamentoId INT NOT NULL,
    DataManutencao DATE NOT NULL,
    Descricao TEXT NOT NULL
);

INSERT INTO Manutencoes (EquipamentoId, DataManutencao, Descricao) VALUES
(1, '2025-09-15', 'Troca de correia'),
(2, '2025-08-20', 'Calibração de resistência'),
(3, '2025-10-01', 'Limpeza e lubrificação'),
(4, '2025-07-10', 'Reparo no mecanismo'),
(5, '2025-06-25', 'Inspeção geral');
