namespace GestorEstoque.Domain.Dto
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; } = 0;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }
}
