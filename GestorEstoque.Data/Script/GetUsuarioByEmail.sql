SELECT
u.Id,
u.NomeCompleto,
u.DataNascimento,
u.Email,
u.Senha,
u.SenhaSal
FROM "Usuario" u
WHERE u.Email = @Email