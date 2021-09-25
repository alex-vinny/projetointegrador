opção para acessar o banco de dados:
RazorSQL

container para criar  novo projeto da api em dotnet:
docker run -it --rm -p 5000:5000 -v ${PWD}:/api --name net --link mysql --workdir /api mcr.microsoft.com/dotnet/sdk:5.0 /bin/bash
docker run -d --rm -p 5000:5000 -v ${PWD}:/api --name net --link mysql --workdir /api mcr.microsoft.com/dotnet/sdk:5.0 dotnet run

dentro do container:
dotnet dev-certs https --trust
dotnet new webapi
dotnet clean
dotnet build
dotnet run

criar projeto angular:
docker run -it --rm -v ${PWD}:/app --workdir /app node /bin/bash

dentro do container:
ng new app
cd app
ng serve

caso ocorra erro de falta de memória:
export NODE_OPTIONS=--max_old_space_size=4096

comandos das imagens oficiais para build e deploy:
docker build -t aspnetapi .
docker run -d --rm -p 5000:5000 --name myapi aspnetapi

testar pelo navegador:
http://localhost:5000/swagger/index.html

docker build -t angularapp .
docker run -d --rm -p 8080:80 --name myapp angularapp

testar pelo navegador:
http://localhost:8080/

banco de dados:
docker pull mcr.microsoft.com/mssql/server:2019-latest
docker run --rm -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Pr0j3to_1ntegRAD0R" -p 1434:1433 --name mysql -h mysql -d mcr.microsoft.com/mssql/server:2019-latest
docker exec -it mysql "bash"

testar no mssql studio com:
localhost,1434

obs: descobrir o ip, rodar o comando "ipconfig" e localizar: Ethernet adapter vEthernet
