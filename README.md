
# Paybyrd - Desafio - Juliano Ramos

Este projeto foi implementado como desafio para ingresso na empresa Paybyrd. 




## Pré-requisitos

Para o correto funcionamento do projeto é necessário ter instalado no ambiente o **.NET 7.0**  e/ou o **Docker** (opcional)

```bash
  https://dotnet.microsoft.com/pt-br/download/dotnet/7.0
  https://docs.docker.com/get-docker/
```



## Rodando localmente

Clone o projeto

```bash
  git clone https://github.com/julianoramosf/paybyrd.git
```

Entre no diretório do projeto

```bash
  cd paybyrd
```

Restaure as dependências e faça o build da aplicação

```bash
  dotnet restore   
  dotnet build
```

Inicialize a aplicação:

**SDK**

```bash
  dotnet run --project ./Paybyrd.Proof.WebApi/Paybyrd.Proof.WebApi.csproj
```

**DOCKER**

```bash
  docker build -t paybyrd .  
  docker run -d -p 8001:80 paybyrd
```

**DOCKER COMPOSE**

```bash
  docker-compose up 
```

## Rodando os testes

Para rodar os testes, rode o seguinte comando

```bash
  dotnet test
```


## Documentação

Com o projeto rodando:

[http://localhost:8001/swagger/index.html](http://localhost:8001/swagger/index.html)
