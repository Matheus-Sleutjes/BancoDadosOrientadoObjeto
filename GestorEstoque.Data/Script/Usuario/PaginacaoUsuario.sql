SELECT 
    u."UsuarioId",
    u."NomeCompleto",
    u."Email",
    u."Telefone",
    u."Ativo"
FROM "Usuario" u
WHERE @NomePesquisa = '' OR u."NomeCompleto" ILIKE '%' || @NomePesquisa || '%'
ORDER BY u."UsuarioId" Desc
LIMIT @TamanhoPagina OFFSET (@TamanhoPagina * (@Pagina - 1));