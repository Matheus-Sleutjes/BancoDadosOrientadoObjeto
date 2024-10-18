using GestorEstoque.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace GestorEstoque.Data
{
    public class GestorEstoqueContext : DbContext
    {
        public GestorEstoqueContext(DbContextOptions<GestorEstoqueContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
