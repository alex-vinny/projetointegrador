## SQL Server

banco de dados:
docker pull mcr.microsoft.com/mssql/server:2019-latest
docker run --rm -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Pr0j3to_1ntegRAD0R" -p 1434:1433 --name mysql -h mysql -d mcr.microsoft.com/mssql/server:2019-latest
docker exec -it mysql "bash"

testar no mssql studio com:
localhost,1434

obs: descobrir o ip, rodar o comando "ipconfig" e localizar: Ethernet adapter vEthernet

## PostgreSQL

docker pull postgres:latest
docker run --rm --name psql -p 5432:5432 -e POSTGRES_PASSWORD=postgres -d postgres
docker exec -it postgres psql -U postgres