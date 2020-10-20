# CRUD-Clientes

Crud de Clientes - Feito no .NET 3.1 com EF Core

Utilizando a lib de validação FluentAPI -> como um filtro customizado do MVC
aplicando o conceito de Fail Fast Validação - se tiver algum dado errado no objeto recebido, nem entra no controller.

Queries todas separadas, mais fácil o controle pra saber quais indices utilizar tornando o plano de execução do SQL mais rápido

Conceito de reposítorio -> Desaclopando o contexto do framework, nos dando a possibilidade de trocar a qualquer momento

E uma camada de testes das Queries usadas no repositório, utilizando um FakeRepository

Mappers feitos na mão, pois já ouvi falar que a lib do AutoMapper não é tão vantajosa assim em desempenho..

Uma BaseResponse p/ facilitar a vida do outro lado da moeda, ou o famigerado front-end
