CREATE TABLE "Usuario" (
    "UsuarioId" SERIAL PRIMARY KEY,
    "Senha" bytea NOT NULL,
    "SenhaSal" bytea NOT NULL,
    "Email" Varchar(150),
    "Ativo" BOOLEAN,
    "NomeCompleto" Varchar(100),
    "Telefone" Varchar(15)
);

CREATE TABLE "Cliente" (
    "ClienteId" SERIAL PRIMARY KEY,
    "Email" Varchar(150),
    "Nome" Varchar(100),
    "CPF_CNPJ" Varchar(15),
    "Endereco" Varchar(200),
    "Telefone" Varchar(15)
);

CREATE TABLE "StatusCarrinho" (
    "StatusCarrinhoId" SERIAL PRIMARY KEY,
    "Descricao" Varchar(40)
);

CREATE TABLE "Produto" (
    "ProdutoId" SERIAL PRIMARY KEY,
    "ValorProduto" Numeric(8,2),
    "Descricao" Varchar(100)
);

CREATE TABLE "Carrinho" (
    "CarrinhoId" SERIAL PRIMARY KEY,
    "DataCadastro" Date,
    "MetodoPagamento" Varchar(50),
    "UsuarioId" integer,
    "ClienteId" integer,
    "StatusCarrinhoId" integer,
    FOREIGN KEY ("UsuarioId") REFERENCES "Usuario" ("UsuarioId"),
    FOREIGN KEY ("ClienteId") REFERENCES "Cliente" ("ClienteId"),
    FOREIGN KEY ("StatusCarrinhoId") REFERENCES "StatusCarrinho" ("StatusCarrinhoId")
);

CREATE TABLE "Estoque" (
    "EstoqueId" SERIAL PRIMARY KEY,
    "Quantidade" integer,
    "ProdutoId" integer,
    FOREIGN KEY ("ProdutoId") REFERENCES "Produto" ("ProdutoId")
);

CREATE TABLE "CarrinhoProduto" (
    "CarrinhoProdutoId" SERIAL PRIMARY KEY,
    "QuatidadeItem" integer,
    "ValorItem" Decimal(8,2),
    "ProdutoId" integer,
    "CarrinhoId" integer,
    FOREIGN KEY ("ProdutoId") REFERENCES "Produto" ("ProdutoId"),
    FOREIGN KEY ("CarrinhoId") REFERENCES "Carrinho" ("CarrinhoId")
);

SELECT * from "Usuario"