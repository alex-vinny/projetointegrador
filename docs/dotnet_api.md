### Passo a passo para criar uma web API em dotnet core
1. Seguindo o tutorial oficial da microsoft [aqui](https://docs.microsoft.com/en-us/ef/core/get-started/overview/install).
2. Instalar o Entity Framework (ORM) para SQL Server:
    * `$ dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
3. Instalar globalmente a ferramenta de comandos do _EF_:
    * `$ dotnet tool install --global dotnet-ef`
    * Adicionar ao PATH com: `$ export PATH="$PATH:/root/.dotnet/tools"`
4. Instalar pacotes do **Entity** para realizar a criação do banco de dados:
    * `$ dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design`
    * `$ dotnet add package Microsoft.EntityFrameworkCore.Design`
    * `$ dotnet add package Microsoft.EntityFrameworkCore.SqlServer`
5. Dropar / Deletar o database caso já exista:
    * `$ dotnet ef database drop`
6. Criar o __migrations__ para o banco de dados:
    * `$ dotnet ef migrations add InitialCreate`
    	* Desfazer o último __migrations__ comando EF: `$ dotnet ef migrations remove`
7. Atualizar efetivament o banco de dados com as mudanças:
    * `$ dotnet ef database update`
8. Instalar ferramenta para gerar automáticamente as _controllers_:
    * `$ dotnet tool install -g dotnet-aspnet-codegenerator`
9. Comando para criar uma __controller__ baseada em uma entidade do banco:
    * `$dotnet aspnet-codegenerator controller -name NameController -async -api -m Model -dc DbContext -outDir Controllers`
10. Executar o comando acima para todas as entidades:
    > * Endpoint `api/Categoria`
    >    * `$ dotnet aspnet-codegenerator controller -f -name CategoriasController -async -api -m Categoria -dc BancoContext -outDir Controllers`
    > * Endpoint `api/Palavras`
    >    * `$ dotnet aspnet-codegenerator controller -f -name PalavrasController -async -api -m Palavra -dc BancoContext -outDir Controllers`
    > * Endpoint `api/Cruzadas`
    >    * `$ dotnet aspnet-codegenerator controller -f -name CruzadasController -async -api -m Cruzada -dc BancoContext -outDir Controllers`
    > * Endpoint `api/ItensCruzada`
    >    * `$ dotnet aspnet-codegenerator controller -f -name ItensCruzadaController -async -api -m CruzadaItem -dc BancoContext -outDir Controllers`
    > * Endpoint `api/Sessoes`
    >    * `$ dotnet aspnet-codegenerator controller -f -name SessoesController -async -api -m Sessao -dc BancoContext -outDir Controllers`
    > * Endpoint `api/Usuarios`
    >    * `$ dotnet aspnet-codegenerator controller -f -name UsuariosController -async -api -m Usuario -dc BancoContext -outDir Controllers`
    > * Endpoint `api/Alunos`
    >    * `$ dotnet aspnet-codegenerator controller -f -name AlunosController -async -api -m Aluno -dc BancoContext -outDir Controllers`
    > * Endpoint `api/Professores`
    >    * `$ dotnet aspnet-codegenerator controller -f -name ProfessoresController -async -api -m Professor -dc BancoContext -outDir Controllers`

11. Após isso, continuar seguindo o tutorial para criar os valores padrões para os testes [aqui](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-5.0).

12. Instalar o EF para __Postgre_ pois este banco de dados é o padrão da plataforma ***Heroku***.
    * `$ dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL`
13. Instalar para o banco _SQLite_, pois é leve e facilita muito os testes.
    * `$ dotnet add package Microsoft.EntityFrameworkCore.Sqlite`
    * `$ dotnet add package Microsoft.EntityFrameworkCore.Sqlite.Design`
