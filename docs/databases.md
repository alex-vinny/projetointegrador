# Banco de dados utilizados no desenvolvimento
## Container Docker para banco.
---
### SQL Server

> Baixar imagem e rodar:
`$ docker pull mcr.microsoft.com/mssql/server:2019-latest`
`$ docker run --rm -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=sqlserver" -p 1434:1433 --name db -h db -d mcr.microsoft.com/mssql/server:2019-latest`
`docker exec -it db "bash"`

Para testar no ***mssql studio*** acessar via: `localhost,1434`
> **OBS**: Para descobrir o ip, rodar o comando `ipconfig` e localizar a interface de rede _Ethernet adapter vEthernet_.
---
### PostgreSQL
> Baixar imagem e rodar:
`$ docker pull postgres:latest`
`$ docker run --rm --name db -p 5432:5432 -e POSTGRES_PASSWORD=postgres -d postgres`
`$ docker exec -it postgres db -U postgres`

Baixar e rodar imagem do __pgadmin__ via container também:
> `$ docker pull dpage/pgadmin4`
`$ docker run --rm --name dbadmin -p 5050:80 -e PGADMIN_DEFAULT_EMAIL=root@email.com -e PGADMIN_DEFAULT_PASSWORD=root --link db -d dpage/pgadmin4`
---
### Formato das strings de conexão
- SQL Server
  - `Server=localhost,1434; User ID=sa; Password=password; Database=DbName; Max Pool Size=1000; Application Name=WebAPI`
  - `Data Source=localhost,1434;Initial Catalog=DbName; User Id=sa; Password=password;`
---
### Opção de aplicativo para acessar o banco de dados:
* RazorSQL