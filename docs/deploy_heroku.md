instalar o client
https://devcenter.heroku.com/articles/heroku-cli

site do banco de dados:
https://data.heroku.com/

página inicial:
https://dashboard.heroku.com/

comandos
heroku login

rodar o container
vincular o diretório ao aplicativo já criado
heroku git:remote -a your_app_name
heroku git:clone -a your_app_name

depois efetuar
heroku container:login

upload
heroku container:push web your_image
nome da imagem no site: https://{project-name}.herokuapp.com

release para deploy
heroku container:release web -a your_image

api publicada:
https://{project-name}.herokuapp.com/swagger/index.html
ou
https://{project-name}.herokuapp.com/index.html