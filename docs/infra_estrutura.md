opção para acessar o banco de dados:
RazorSQL

container para criar  novo projeto da api em dotnet:
docker run -it --rm -p 5000:5000 -v ${PWD}:/api --name net --link mysql --workdir /api mcr.microsoft.com/dotnet/sdk:5.0 /bin/bash
docker run -d --rm -p 5000:5000 -v ${PWD}:/api --name net --link mysql --workdir /api mcr.microsoft.com/dotnet/sdk:5.0 dotnet run
docker run -it --rm -p 5000:5000 -v ${PWD}:/api --name net --workdir /api mcr.microsoft.com/dotnet/core/sdk:3.1 /bin/bash

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
docker build -t aspnetapi -f Dockerfile .
docker run -d --rm -p 5000:5000 --name myapi aspnetapi
exemplo que funcionou:
docker run --rm -d -p 5000:80 --name demoapp dotnet-docker-heroku

testar pelo navegador:
http://localhost:5000/swagger/index.html

docker build -t angularapp .
docker run -d --rm -p 8080:80 --name myapp angularapp

testar pelo navegador:
http://localhost:8080/