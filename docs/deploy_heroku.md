## Informações para utilização da plataforma Heroku

### Instalar o client para efetuar comandos:
* [Link official com passo a passo](https://devcenter.heroku.com/articles/heroku-cli).
* Portal para criação e administração de banco de dados:
    * [Clique aqui](https://data.heroku.com/).
* Página inicial ou dashboard:
    * [Clique aqui](https://dashboard.heroku.com/).

### Principais comandos:
> `$ heroku login`

`#` *Contruir ou rodar o container*
`#` _Vincular o diretório ao aplicativo **Heroku** já criado_
> `$ heroku git:remote -a <your_app_name>`
`$ heroku git:clone -a your_app_name`

`#` _Depois efetuar:_
> `heroku container:login`

`#` _Para o upload da imagem ao registry do **Heroku**:_
> `heroku container:push web your_image`

O aplicativo aparecerá como: `https://{project-name}.herokuapp.com`

`#` _Comando para Release com Deploy_
> `heroku container:release web -a your_image`

`*` _A __API__ estará api publicada em:_
[URI 1](https://{project-name}.herokuapp.com/swagger/index.html) ou [URI 2](https://{project-name}.herokuapp.com/index.html).