USE [master];

GO
IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = 'gestproduto_user')
BEGIN
	CREATE LOGIN [gestproduto_user] WITH PASSWORD = 'gestproduto_user123', CHECK_POLICY = OFF;
	ALTER SERVER ROLE [sysadmin] ADD MEMBER [gestproduto_user];

END

GO
IF DB_ID('gestproduto') IS NULL
BEGIN
    CREATE DATABASE [gestproduto]; 
END

GO
USE [gestproduto];	

GO
IF OBJECT_ID('dbo.USUARIOS') IS NULL
BEGIN 
	CREATE TABLE [dbo].[USUARIOS] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar] (30) NOT NULL,
	[Email] [nvarchar] (100) NOT NULL,
	[Usuario] [nvarchar] (30) NOT NULL,
	[Senha] [nvarchar] (30) NOT NULL,
	[Perfil] [nvarchar] (30) NOT NULL,
	CONSTRAINT PK_USUARIOS PRIMARY KEY ([id])
	);
	INSERT INTO [dbo].[USUARIOS] VALUES ('Admin','admin@gestproduto.com.br', 'admin', 'admin', 'ADMIN');
	INSERT INTO [dbo].[USUARIOS] VALUES ('Juliana','juliana@gestproduto.com.br', 'juliana', '123', 'GESTOR');
	INSERT INTO [dbo].[USUARIOS] VALUES ('Funcionario1','funcionario@gestproduto.com.br', 'funcionario1', '456', 'FUNCIONARIO');
	INSERT INTO [dbo].[USUARIOS] VALUES ('Cliente1','cliente@gestproduto.com.br', 'cliente1', '789', 'CLIENTE');
END

GO
IF OBJECT_ID('dbo.CATEGORIAS') IS NULL 
BEGIN 
	CREATE TABLE [dbo].[CATEGORIAS] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar] (30) NOT NULL,
	[Descricao] [nvarchar] (100) NOT NULL,	
	CONSTRAINT PK_CATEGORIAS PRIMARY KEY ([id])
	);
	INSERT INTO [dbo].[CATEGORIAS] VALUES ('Alimentos Básicos','Alimentos não perecívies da cesta básica.');
	INSERT INTO [dbo].[CATEGORIAS] VALUES ('Bebidas Não Alcoólicas','Refrigerantes, sucos, agua, energético, etc.');
	INSERT INTO [dbo].[CATEGORIAS] VALUES ('Bebidas Alcoólicas','Cerveja, vinhos e destilados em Geral.');
	INSERT INTO [dbo].[CATEGORIAS] VALUES ('Frios','Produtos embutidos com necessidade de refrigeração.');
END

GO
IF OBJECT_ID('dbo.PRODUTOS') IS NULL
BEGIN
	CREATE TABLE [dbo].[PRODUTOS] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Nome] [nvarchar] (30) NOT NULL,
		[CategoriaId] [int] NOT NULL,
		[Quantidade] [int] NOT NULL,
		[ValorVenda] [money] NOT NULL,
		CONSTRAINT PK_PRODUTOS PRIMARY KEY ([Id])
	);
	ALTER TABLE [dbo].[PRODUTOS]  WITH CHECK ADD  CONSTRAINT [FK_PRODUTOS_CATEGORIA] FOREIGN KEY([CategoriaId]) REFERENCES [dbo].[CATEGORIAS]([Id]);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Macarrão', 1, 0, 0);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Feijão', 1, 0, 0);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Arroz', 1, 0, 0);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Água', 2, 0, 0);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Suco Laranja', 2, 0, 0);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Energético Monster', 2, 0, 0);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Cerveja Heineken', 3, 0, 0);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Vinho Canção', 3, 0, 0);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Vodka', 3, 0, 0);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Queijo', 4, 0, 0);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Presunto', 4, 0, 0);
	INSERT INTO [dbo].[PRODUTOS] VALUES ('Requeijão', 4, 0, 0);
END
