# Web API C#.NET 8.0 - Guia de Configuração

## Pré-requisitos

Antes de executar o projeto, é necessário aplicar as migrations no banco de dados.

## Autenticação e Uso da API

Para acessar as rotas `api/v1/todos`, siga os passos abaixo:

### 1. Cadastro de Usuário

Envie uma requisição POST para `/api/v1/users` com o seguinte corpo:

```json
{
    "username": "string",
    "password": "string"
}
```

### 2. Autenticação

Após criar o usuário, faça login enviando uma requisição POST para `/api/v1/login`:

```json
{
    "username": "string",
    "password": "string"
}
```

### 3. Configuração do Token

- Ao receber o token de autenticação no response do login, configure-o no seu cliente HTTP (ex: Postman)
- Adicione o token como Bearer Token nas requisições
- Agora você está pronto para acessar as rotas `api/v1/todos`

> **Nota**: Todas as requisições para `api/v1/todos` requerem um token válido no cabeçalho da requisição.
