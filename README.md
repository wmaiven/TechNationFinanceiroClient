#Requisitos: docker, .net core 8.0, rodar o backend, Passo a passo de como fazer isso: https://github.com/wmaiven/TechNationFinanceiroClient

  - Passo 1: clonar o projeto 
  - passo 2: compilar o projeto para instalar as dependencias
  - passo 3: Rodar o comando ```docker build -t technationfinanceiroclient .```  para realizar o build da aplicação
  - passo 4: Rodar o comando ```docker run -d -p 8200:8080 --name technationclient technationfinanceiroclient``` para criar uma imagem docker do front
  - passo 5: Rodar o comando ```dotnet dev-certs https --trust```
  - Passo 6: Rodar o projeto via docker ou via https
  - Passo 7: Entrar com o usuario "admin" senha "senha123"
