seguindo o tutorial oficial da microsoft em:
https://docs.microsoft.com/en-us/ef/core/get-started/overview/install

Instalar o Entity Framework (ORM) para SQL Server:
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

Instalar globalmente a ferramenta de comandos do EF:
dotnet tool install --global dotnet-ef
adicionar ao PATH
export PATH="$PATH:/root/.dotnet/tools"

pacotes do entity para realizar a criação do banco de dados:
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dropar database:
dotnet ef database drop

criar o banco de dados:
dotnet ef migrations add InitialCreate

desfazer algum comando ef:
ef migrations remove

atualizar o banco de dados com as mudanças:
dotnet ef database update

instalar para gerar as controller com:
dotnet tool install -g dotnet-aspnet-codegenerator
comando para criar a controller:
dotnet aspnet-codegenerator controller -name NameController -async -api -m Model -dc DbContext -outDir Controllers

vamos criar para todas as entidades:
api/Categoria
dotnet aspnet-codegenerator controller -f -name CategoriasController -async -api -m Categoria -dc BancoContext -outDir Controllers
api/Palavras
dotnet aspnet-codegenerator controller -f -name PalavrasController -async -api -m Palavra -dc BancoContext -outDir Controllers
api/Cruzadas
dotnet aspnet-codegenerator controller -f -name CruzadasController -async -api -m Cruzada -dc BancoContext -outDir Controllers
api/ItensCruzada
dotnet aspnet-codegenerator controller -f -name ItensCruzadaController -async -api -m CruzadaItem -dc BancoContext -outDir Controllers
api/Sessoes
dotnet aspnet-codegenerator controller -f -name SessoesController -async -api -m Sessao -dc BancoContext -outDir Controllers
api/Usuarios
dotnet aspnet-codegenerator controller -f -name UsuariosController -async -api -m Usuario -dc BancoContext -outDir Controllers
api/Alunos
dotnet aspnet-codegenerator controller -f -name AlunosController -async -api -m Aluno -dc BancoContext -outDir Controllers
api/Professores
dotnet aspnet-codegenerator controller -f -name ProfessoresController -async -api -m Professor -dc BancoContext -outDir Controllers

após isso, continuar para criar valores padrões para os testes:
https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-5.0

instalar o EF para postgre pois é o padrão do heroku
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

para o sqlite
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Sqlite.Design