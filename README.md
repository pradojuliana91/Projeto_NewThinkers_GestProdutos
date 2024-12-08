# GestProduto

O **GestProduto** é um sistema de gerenciamento de produtos, que permite consultar todos os produtos cadastrados, consultar produtos em estoque e, de acordo com o perfil do usuário, excluir produtos e atualizar o estoque. O sistema foi construído utilizando **API Rest .NET 8** feita no padrão **MVC**. 
Para garantir segurança e controle de acesso, foi implementada autenticação baseada em perfis de usuário. Os perfis são:
- **Administrador**: possui acesso total ao sistema, incluindo operações de exclusão e atualização de estoque.
- **Gestor**: pode realizar operações de exclusão de produtos e atualização de estoque.
- **Funcionário**: tem permissões limitadas para realizar atualizações no estoque.
- **Cliente**: possui acesso apenas para consultar produtos cadastrados e verificar o estoque disponível.

## Funcionalidades

- **Gestão de Produtos**: Operações de pesquisa, cadastro, alteração e exclusão de produtos.
- **Autenticação**: Controle de acesso baseado em perfis de usuário.

## Tecnologias Utilizadas

- **C#** (.NET Framework 8.0)
- **API REST** (padrão MVC)
- **ADO.NET** para acesso aos dados
- **Injeção de Dependências** para gerenciamento de serviços e repositórios
- **JWT(JSON Web Token)** para autenticação, garantindo segurança no controle de acesso baseado em perfis.
- **SQL Server** (banco de dados)
- **Docker** para containerização do banco de dados
- **DBeaver** para interação com o banco de dados
- **JSON** para formatação de dados
- **xUnit e Moq** para testes unitários

## Estrutura de Banco de Dados

O projeto utiliza ADO.NET como tecnologia de acesso ao banco de dados e o SQL Server como sistema gerenciador. A estrutura do banco de dados foi projetada com as seguintes tabelas principais:

- **CATEGORIAS**: Armazena as categorias dos produtos.
- **PRODUTOS**: Gerencia os produtos disponíveis no sistema.
- **USUÁRIOS**:Gerencia os usuários do sistema, incluindo autenticação e controle de acesso por perfis.

## Docker Compose e Scripts SQL Utilizados

O banco de dados é configurado utilizando **Docker** e scripts SQL. Para subir o banco de dados, utilize o **Prompt de Comando do Desenvolvedor** no Visual Studio, apontando para a raiz do projeto, e execute os comandos:

- **Iniciar o banco de dados**:
  ```bash  
  docker-compose up -d
  
- **Desligar o banco de dados**:
  ```bash
  docker-compose down

O script SQL cria o banco de dados **gestproduto**, o login **gestproduto_user** e as permissões necessárias, além de criar as tabelas essenciais como **CATEGORIAS**, **PRODUTOS**, **USUARIOS**, e insere alguns dados iniciais.

## Autenticação JWT

A autenticação no sistema GestProduto é baseada em JWT (JSON Web Tokens), garantindo segurança e controle de acesso aos recursos da API.

Processo de Autenticação:

- O usuário faz login na API fornecendo suas credenciais (login e senha).
- A API valida as credenciais consultando a tabela USUÁRIOS no banco de dados.
- Após a validação bem-sucedida, a API gera um token JWT assinado, contendo informações como:
  -Identificação do usuário (ID)
  -Perfil de acesso (Administrador, Gestor, Funcionário ou Cliente)
  -Data de expiração do token
- O token é retornado ao cliente e deve ser enviado em cada requisição subsequente no cabeçalho HTTP:

	```bash  
  Authorization: Bearer <seu-token-jwt>

## Testes Unitários

A camada de serviços da API foi testada utilizando **xUnit** e **Moq**. Esses testes garantem que as regras de negócio foram implementadas corretamente e funcionam como esperado, sem a necessidade de uma conexão real com o banco de dados ou outras dependências externas.

## Como Executar o Projeto

1. Clone o repositório:
   ```bash
   git clone [https://github.com/pradojuliana91/Projeto_Final_NewThinkers_GestProdutos]

2. Suba o ambiente Docker:
    ```bash
   docker-compose up -d

3. Compile e execute o projeto API no Visual Studio.
4. Acesse o sistema no navegador e utilize as funcionalidades de gerenciamento de produtos e a autenticação baseada no perfil do usuário.

## Exemplo de Requisição para Cadastro de Produto ##

Ao realizar a operação de cadastro de um produto, a API espera receber um objeto JSON com os dados do produto. 
Abaixo está um exemplo de requisição feita via Postman:

- Método: POST
- URL: http://localhost:5083/login
- Headers:
  ![image](https://github.com/user-attachments/assets/6945544d-000e-4212-bad6-a5f62e5a739e)
- Body (Exemplo de JSON)
- Envie os seguintes dados na aba Body, selecionando a opção raw e configurando como JSON:
  ![image](https://github.com/user-attachments/assets/b87f6c13-d2bf-4d2f-86aa-34d5b2f8a637)
- Resposta Esperada
- Se as credenciais forem válidas, a API retornará um token JWT no seguinte formato:
  ![image](https://github.com/user-attachments/assets/8241332a-b7a4-47b1-a9b7-35f852524275)
- Passo Seguinte: Cadastro de Produto
- Método: POST
- URL: http://localhost:5083/produtos
- Depois de obter o token, insira-o no cabeçalho de autorização para realizar o cadastro de produtos, como descrito no próximo exemplo.
  ![image](https://github.com/user-attachments/assets/d706d593-21cf-415b-ae3c-4aa1c55686d4)
- Requisição de Cadastro de Produto
- No Body, você irá enviar os dados do produto a ser cadastrado.
- Exemplo de Body (Cadastro de Produto)
  ![image](https://github.com/user-attachments/assets/bb5d89e7-dbe7-4ad0-888c-b78136750fd6)
  ![image](https://github.com/user-attachments/assets/8872d0fb-470f-4f28-a390-e5bbc005e163)
   
## Contribuições   
  
Contribuições são bem-vindas! Para sugerir melhorias ou relatar problemas, abra uma issue ou envie um pull request.

## Créditos e Agradecimentos

Este projeto foi desenvolvido como parte do **PROGRAMA NEW THINKERS** de formação em **.NET** oferecido pela **SQUADRA DIGITAL**. Agradeço a oportunidade de aprendizado e desenvolvimento prático proporcionada pela empresa. Um agradecimento especial também aos colegas que contribuíram durante o processo de formação e desenvolvimento.

## Contato

Para perguntas ou sugestões, entre em contato:

- **Juliana do Prado Fernandes**
- Email: [pradojulianaf@gmail.com]
- GitHub: [https://github.com/pradojuliana91](https://github.com/pradojuliana91)


Obrigado por conferir o projeto **GestProduto**! Esperamos que ele seja útil para você!
