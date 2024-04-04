CREATE DATABASE Unival;
USE Unival;

CREATE TABLE Usuarios (
	ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Email VARCHAR(255),
    Senha VARCHAR(255)
);    

CREATE TABLE Alunos (
	ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(255),
    CPF VARCHAR(255),
    Email VARCHAR(255),
    Celular VARCHAR(255),
    DataNascimento DATE
);

CREATE TABLE Professores (
	ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(255),
    CPF VARCHAR(255),
    Email VARCHAR(255),
    Celular VARCHAR(255),
    DataNascimento DATE
);

CREATE TABLE Materias (
	ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(255),
    Descricao VARCHAR(255),
    Professor INT NOT NULL,
    FOREIGN KEY (Professor) References Professores (ID)
);

CREATE TABLE DependenciasMateria (
	Materia INT NOT NULL,
    Dependencia INT NOT NULL,
    FOREIGN KEY (Materia) REFERENCES Materias (ID),
    FOREIGN KEY (Dependencia) REFERENCES Materias (ID)
);

CREATE TABLE Matriculas (
	ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Status VARCHAR(255), -- Cursando, Trancado ou Concluido
    Aluno INT NOT NULL,
	Materia INT NOT NULL,
    FOREIGN KEY (Aluno) REFERENCES Alunos (ID),
    FOREIGN KEY (Materia) REFERENCES Materias (ID)
);

INSERT INTO Alunos (Nome, CPF, Email, Celular, DataNascimento)
VALUES
('João Silva', '123.456.789-10', 'joao.silva@example.com', '(11) 99999-1234', '2000-01-15'),
('Maria Oliveira', '987.654.321-98', 'maria.oliveira@example.com', '(11) 98765-4321', '1999-05-20'),
('Pedro Santos', '456.789.123-20', 'pedro.santos@example.com', '(11) 87654-3210', '2001-11-10');

-- Inserindo dados na tabela Professores
INSERT INTO Professores (Nome, CPF, Email, Celular, DataNascimento)
VALUES
('Ana Souza', '111.222.333-44', 'ana.souza@example.com', '(11) 88888-7777', '1975-03-25'),
('Carlos Lima', '222.333.444-55', 'carlos.lima@example.com', '(11) 77777-6666', '1980-08-12'),
('Mariana Costa', '333.444.555-66', 'mariana.costa@example.com', '(11) 66666-5555', '1988-12-30');

-- Inserindo dados na tabela Materias
INSERT INTO Materias (Nome, Descricao, Professor)
VALUES
('Matemática', 'Curso introdutório de Matemática', 1),
('Física', 'Curso básico de Física', 2),
('Química', 'Curso avançado de Química', 3);

-- Inserindo dados na tabela DependenciasMateria
INSERT INTO DependenciasMateria (Materia, Dependencia)
VALUES
(2, 1), -- Física tem dependência de Matemática
(3, 2); -- Química tem dependência de Física

-- Inserindo dados na tabela Matriculas
INSERT INTO Matriculas (Status, Aluno, Materia)
VALUES
('Cursando', 1, 1), -- João está cursando Matemática
('Cursando', 2, 2), -- Maria está cursando Física
('Trancado', 3, 3); -- Pedro está com a matrícula trancada em Química

