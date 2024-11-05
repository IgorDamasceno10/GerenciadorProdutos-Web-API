# Gerenciador de Pedidos API

Bem-vindo ao **Gerenciador de Pedidos API**, uma aplicação que nasceu com o objetivo de facilitar o gerenciamento de pedidos em lojas ou sistemas de e-commerce. Desenvolvido com a tecnologia **.NET Core**, esta API oferece uma maneira simples e eficiente de gerenciar **clientes**, **produtos** e **pedidos** de maneira centralizada e organizada.

## O Que É Este Projeto?

Este projeto foi criado com a intenção de fornecer uma solução prática e escalável para gerenciar pedidos, produtos e clientes de maneira eficaz. A ideia é criar uma API robusta que permita que uma loja ou sistema possa acompanhar seus pedidos e gerenciar as informações relacionadas a eles de forma simples e ágil.

### Por Que Este Projeto Foi Criado?

O propósito deste projeto é simplificar o gerenciamento de pedidos para pequenas e médias empresas, oferecendo um sistema fácil de usar, com endpoints bem estruturados para controlar clientes, produtos e pedidos, sem perder a flexibilidade necessária para personalizar o sistema de acordo com as necessidades do negócio.

Através da utilização do **Entity Framework Core** para trabalhar com o banco de dados e o **Swagger** para documentação interativa, o projeto também facilita a integração e o entendimento de quem precisa utilizá-lo, seja para testar ou expandir o sistema.

## Funcionalidades

A API cobre as funcionalidades essenciais para o gerenciamento de pedidos e produtos:

- **Clientes**: Cadastrar, listar, editar e excluir clientes.
- **Produtos**: Cadastrar, listar, editar e excluir produtos.
- **Pedidos**: Criar, listar, editar e excluir pedidos.
- **Pedido_Produto**: Associar produtos aos pedidos, adicionar novos produtos e excluir produtos de um pedido.

### Endpoints da API

#### Clientes

- **GET** `/v1/clientes`: Retorna todos os clientes.
- **GET** `/v1/clientes/{id}`: Retorna um cliente específico.
- **POST** `/v1/clientes`: Cria um novo cliente.
- **PUT** `/v1/clientes/{id}`: Atualiza um cliente existente.
- **DELETE** `/v1/clientes/{id}`: Exclui um cliente.

#### Produtos

- **GET** `/v1/produtos`: Retorna todos os produtos.
- **GET** `/v1/produtos/{id}`: Retorna um produto específico.
- **POST** `/v1/produtos`: Cria um novo produto.
- **PUT** `/v1/produtos/{id}`: Atualiza um produto existente.
- **DELETE** `/v1/produtos/{id}`: Exclui um produto.

#### Pedidos

- **GET** `/v1/pedidos`: Retorna todos os pedidos.
- **GET** `/v1/pedidos/{id}`: Retorna um pedido específico.
- **POST** `/v1/pedidos`: Cria um novo pedido.
- **PUT** `/v1/pedidos/{id}`: Atualiza um pedido existente.
- **DELETE** `/v1/pedidos/{id}`: Exclui um pedido.

#### Pedido_Produto

- **GET** `/v1/pedidos/{pedidoId}/produtos`: Retorna os produtos de um pedido.
- **POST** `/v1/pedidos/{pedidoId}/produtos`: Adiciona um produto a um pedido.
- **DELETE** `/v1/pedidos/{pedidoId}/produtos/{produtoId}`: Remove um produto de um pedido.

## Tecnologias Utilizadas

Este projeto foi desenvolvido usando as seguintes tecnologias:

- **.NET Core**: A base para construção da API.
- **Entity Framework Core**: Para fazer a interface com o banco de dados de forma simples e eficiente.
- **MySQL**: O banco de dados utilizado para armazenar informações de clientes, produtos e pedidos.
- **Swagger**: Para facilitar a documentação e o uso da API, permitindo testar os endpoints de forma interativa.

## Como Rodar o Projeto

### Pré-requisitos

Antes de rodar o projeto, você precisa ter as seguintes ferramentas instaladas:

- **.NET Core 6 ou superior**
- **MySQL** ou qualquer outro banco de dados configurado
- **Visual Studio** ou **VS Code** (para editar o código)

### Passos

1. **Clone o repositório**:

   ```bash
   git clone https://github.com/IgorDamasceno10/gerenciador-pedidos.git
