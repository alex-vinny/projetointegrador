## Infraestrutura tanto para criar as templates dos projetos quanto para rodar as aplicações
---
#### C# .Net Core
Container para criar o novo projeto da api em **dotnet**:
* Versão 5.0 logando dentro do container
> `$ docker run -it --rm -p 5000:5000 -v ${PWD}:/api --name net --link mysql --workdir /api mcr.microsoft.com/dotnet/sdk:5.0 /bin/bash`
* Versão 5.0 para rodar a aplicação
> `$ docker run -d --rm -p 5000:5000 -v ${PWD}:/api --name net --link mysql --workdir /api mcr.microsoft.com/dotnet/sdk:5.0 dotnet run`
* Versão 3.0 já acessando pelo terminal
> `$ docker run -it --rm -p 5000:5000 -v ${PWD}:/api --name net --workdir /api mcr.microsoft.com/dotnet/core/sdk:3.1 /bin/bash`

De dentro do container executar os comandos para criar o projeto:
> `$ dotnet dev-certs https --trust`
`$ dotnet new webapi`
`$ dotnet clean`
`$ dotnet build`
`$ dotnet run`
---
#### Javascript Angular
Container para criar o novo projeto da spa em **angular**:
> `$ docker run -it --rm -v ${PWD}:/app --workdir /app node /bin/bash`

De dentro do container executar os comandos para criar o projeto:
> `$ ng new app`
`$ cd app`
`$ ng serve`

> ***Observação***:
>> Caso ocorra erro de falta de memória executar o seguinte comando: `export NODE_OPTIONS=--max_old_space_size=4096`
---
#### Imagens e comandos para build e deploy
* Comandos de buld ***C#***
> `$ docker build -t webapi .`
> _ou_
`$ docker build -t webapi -f Dockerfile .`
* Comando para executar a imagem de deploy
> `$ docker run -d --rm -p 5000:5000 --name myapi webapi`
Outro exemplo que funcionou:
`$ docker run --rm -d -p 5000:80 --link db --name api webapi`
* Testar pelo navegador: Acessar [localhost](http://localhost:5000/swagger/index.html).
* Comandos de buld ***Javascript***
> `$ docker build -t angularapp .`
`$ docker run -d --rm -p 8080:80 -e PORT=80 --name myapp angularapp`
* Testar pelo navegador: Acessar [localhost](http://localhost:8080/)
