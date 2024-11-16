--INSERT INTO "Usuario" ("UsuarioId", "NomeCompleto", "Email", "Senha", "SenhaSal", "Telefone", "Ativo") VALUES
--	(@NomeCompleto, @Email, @Senha, @SenhaSal, @Telefone, @Ativo);

INSERT INTO "Usuario" ("NomeCompleto", "Senha", "SenhaSal", "Email", "Telefone", "Ativo")
	VALUES (@NomeCompleto, @Senha, @SenhaSal, @Email, @Telefone, @Ativo);