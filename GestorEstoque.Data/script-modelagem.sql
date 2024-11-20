CREATE TABLE "Usuario" (
    "UsuarioId" serial PRIMARY KEY,
    "NomeCompleto" Varchar(100),
    "Senha" bytea,
    "SenhaSal" bytea,
    "Email" Varchar(150),
    "Ativo" boolean,
    "Telefone" Varchar(15)
);

CREATE TABLE "Cliente" (
    "ClienteId" serial PRIMARY KEY,
    "Email" Varchar(150),
    "Nome" Varchar(100),
    "CPF_CNPJ" Varchar(15),
    "Endereco" Varchar(200),
    "Telefone" Varchar(15)
);

CREATE TABLE "StatusCarrinho" (
    "StatusCarrinhoId" serial PRIMARY KEY,
    "Descricao" varchar(40)
);

CREATE TABLE "Produto" (
    "ProdutoId" serial PRIMARY KEY,
    "Descricao" varchar(100),
    "ValorProduto" numeric(8,2),
    "TipoProduto" Char(1),
    "Quantidade" Integer
);

CREATE TABLE "Carrinho" (
    "CarrinhoId" serial PRIMARY KEY,
    "DataCadastro" Date,
    "ValorTotalCarrinho" Decimal(8,2),
    "UsuarioId" integer,
    "ClienteId" integer,
    "StatusCarrinhoId" integer,
    FOREIGN KEY("UsuarioId") REFERENCES "Usuario" ("UsuarioId"),
    FOREIGN KEY("ClienteId") REFERENCES "Cliente" ("ClienteId"),
    FOREIGN KEY("StatusCarrinhoId") REFERENCES "StatusCarrinho" ("StatusCarrinhoId")
);

CREATE TABLE "MetodoPagamento" (
    "MetodoPagamentoId" Serial PRIMARY KEY,
    "Descricao" Varchar(20)
);

CREATE TABLE "CarrinhoProduto" (
    "CarrinhoProdutoId" serial PRIMARY KEY,
    "QuatidadeItem" integer,
    "ValorTotalItem" Decimal(8,2),
    "ProdutoId" integer,
    "CarrinhoId" integer,
    FOREIGN KEY("ProdutoId") REFERENCES "Produto" ("ProdutoId"),
    FOREIGN KEY("CarrinhoId") REFERENCES "Carrinho" ("CarrinhoId")
);

CREATE TABLE "Pagamento" (
    "PagamentoId" Serial PRIMARY KEY,
    "DataPagamento" Date,
    "ValorPagamento" Decimal(8,2),
    "MetodoPagamentoId" integer,
    "CarrinhoId" integer,
    FOREIGN KEY("MetodoPagamentoId") REFERENCES "MetodoPagamento" ("MetodoPagamentoId"),
    FOREIGN KEY("CarrinhoId") REFERENCES "Carrinho" ("CarrinhoId")
);
