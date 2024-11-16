SELECT
u."UsuarioId",
u."NomeCompleto",
u."Email",
u."Senha",
u."SenhaSal",
u."Telefone",
u."Ativo"
FROM "Usuario" u
WHERE u."Email" = @Email