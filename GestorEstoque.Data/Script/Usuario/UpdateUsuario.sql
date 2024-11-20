UPDATE "Usuario"
SET "NomeCompleto"=@NomeCompleto,
	"Senha"=@Senha,
	"SenhaSal"=@SenhaSal,
	"Email"=@Email,
	"Ativo"=@Ativo,
	"Telefone"=@Telefone
WHERE "UsuarioId" = @UsuarioId