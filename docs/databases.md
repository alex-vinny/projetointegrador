## SQL Server

banco de dados:
docker pull mcr.microsoft.com/mssql/server:2019-latest
docker run --rm -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Pr0j3to_1ntegRAD0R" -p 1434:1433 --name db -h db -d mcr.microsoft.com/mssql/server:2019-latest
docker exec -it db "bash"

testar no mssql studio com:
localhost,1434

obs: descobrir o ip, rodar o comando "ipconfig" e localizar: Ethernet adapter vEthernet

## PostgreSQL

docker pull postgres:latest
docker run --rm --name db -p 5432:5432 -e POSTGRES_PASSWORD=postgres -d postgres
docker exec -it postgres db -U postgres

pgadmin via container também:
docker pull dpage/pgadmin4
docker run --rm --name dbadmin -p 5050:80 -e PGADMIN_DEFAULT_EMAIL=root@email.com -e PGADMIN_DEFAULT_PASSWORD=root --link db -d dpage/pgadmin4

## String de conexão
conexão ao banco de dados:
"Server=localhost,1434; User ID=sa; Password=Pr0j3to_1ntegRAD0R; Database=ProjetoIntegrador; Max Pool Size=1000; Application Name=WebAPI"
"Data Source=localhost,1434;Initial Catalog=ProjetoIntegrador; User Id=sa; Password=Pr0j3to_1ntegRAD0R;"