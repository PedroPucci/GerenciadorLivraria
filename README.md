# GerenciadorLivraria

## Documentação

- [Descrição do Desafio Técnico](docs/desafio-tecnico-livraria.pdf)

# **Descrição do projeto**
- A API GerenciadorLivraria é uma solução moderna para gerenciamento de livros de uma livraria.
Desenvolvida em .NET 8, a aplicação permite realizar operações de cadastro, consulta, atualização e remoção de livros, 
seguindo boas práticas de arquitetura e desenvolvimento.

Principais Recursos:
- CRUD completo de livros.
- Validações utilizando FluentValidation.
- Documentação automatizada com Swagger.
- Logs estruturados com Serilog.
- Testes unitários e BDD.
- Arquitetura organizada em camadas.
- Health Checks para monitoramento da aplicação.

A solução foi construída com foco em escalabilidade, organização, manutenibilidade e boas práticas de desenvolvimento backend.

# **Solução**
- API REST desenvolvida em .NET 8.0, utilizando Entity Framework Core e os padrões Unit of Work e Repository, com suporte a validações, tratamento de erros, logging e documentação via Swagger.
---
## **Tecnologias**
- .NET 8.0
- Entity Framework Core
- SQL Server

## **Ferramentas**
- Visual Studio 2022
- SQL Server Management Studio (SSMS)
- Git / Git Bash
- Postman
- ---
## **Recursos do Projeto**
- **Serilog**: Para geração e gerenciamento de logs estruturados da aplicação.
- **FluentValidation**: Para validação de dados e regras de negócio.
- **Entity Framework Core (ORM)**: Para mapeamento e interação com o banco de dados.
- **Repository Pattern**: Para abstração da camada de acesso a dados.
- **Unit of Work**: Padrão utilizado para gerenciamento de transações e persistência de dados.
- **Migrations**: Gerenciamento e versionamento das alterações do banco de dados.
- **xUnit + Moq**: Para criação de testes unitários utilizando o padrão AAA (Arrange, Act, Assert).
- **BDD com Reqnroll**: Testes de comportamento baseados em cenários utilizando Gherkin.
- **FluentAssertions**: Melhor legibilidade e fluidez nas validações dos testes.
- **ASP.NET Core Identity**: Gerenciamento de autenticação, autorização, usuários e perfis.
- **JWT Authentication**: Implementação de autenticação baseada em tokens JWT.
- **Health Checks**: Monitoramento da saúde da aplicação, verificando API, SQL Server e Redis.
- **Redis Cache**: Cache distribuído para otimização de consultas e melhoria de performance.
- **Swagger/OpenAPI**: Documentação automatizada e interativa dos endpoints da API.
- **Soft Delete**: Exclusão lógica de registros utilizando controle por status ativo/inativo.
- **Paginação, filtros e ordenação**: Suporte para consultas otimizadas e flexíveis.
- **Tratamento Global de Exceções**: Middleware centralizado para padronização de erros da API.
- **Logs Estruturados**: Registro detalhado de operações e exceções para auditoria e monitoramento.
- **Docker**: Containerização da aplicação para facilitar execução e deploy.
- **Autorização por Roles**: Controle de acesso baseado em perfis como Admin e User.
- **Rate Limiting**: Proteção da API contra excesso de requisições.
- **Correlation ID**: Rastreamento de requisições para observabilidade e troubleshooting.
---
## **Estrutura do projeto**
A aplicação foi estruturada utilizando separação em camadas:

- API
- Application
- Domain
- Infrastructure
- Shared

Seguindo princípios como:
- SOLID
- Clean Code
- Separation of Concerns
- Repository Pattern
- Unit of Work
---
## **Como Executar o Projeto**
### **1. Configuração Inicial do Banco de Dados**
1. Faça o clone do projeto.
2. Verifique se a pasta `Migrations` no projeto está vazia. Caso contrário, delete todos os arquivos dessa pasta.   
3. Execute os seguintes comandos no **Package Manager Console**:
   - Certifique-se de selecionar o projeto relacionado ao banco de dados no menu "Default project".
   - Execute:
     ```bash
     add-migration PrimeiraMigracao
     update-database
     ```
   - Isso criará e configurará o banco de dados no Microsoft SQL Server.
---
### **2. Executando o Projeto**
1. Abra o projeto no Visual Studio 2022.
2. Configure o projeto principal para execução:
   - Clique com o botão direito no projeto **GerenciadorLivraria** e selecione `Set as Startup Project`.
3. Clique no botão **HTTPS** no menu superior para iniciar a aplicação.

### **3. Banco de Dados**
- **Centralização de Exceções:**  
  Implementada a classe `ExceptionMiddleware` para unificar o tratamento de erros no sistema.
- **Alterações Realizadas:**  
  Ajustadas as classes `Program` e `RepositoryUoW` para integrar o middleware.
- **Mensagens de Erro:**  
  - Se o banco de dados não existir, os endpoints retornam:  
    ```text
    The database is currently unavailable. Please try again later.
    ```
  - Para erros inesperados na criação do banco, é exibido:  
    ```text
    An unexpected error occurred. Please contact support if the problem persists.
    ```
---
### **4. Configuração do Log**
- O sistema gera logs diários com informações sobre os processos executados no projeto.
- O log será salvo no diretório:  
  `C://Users//User//Downloads//Gerenciador-Livraria`.  
  **Nota**: É necessário criar a pasta manualmente nesse caminho ou alterar o diretório no código, caso deseje personalizá-lo.

  **Formato do arquivo de log criado**:
- Arquivo diário com informações estruturadas.
---
### **5. Finalização**
- Após seguir as etapas anteriores, o sistema será iniciado, e uma página com a interface **Swagger** será aberta automaticamente no navegador configurado no Visual Studio. Essa página permitirá explorar e testar os endpoints da API.
---
