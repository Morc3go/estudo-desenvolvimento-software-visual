using Microsoft.EntityFrameworkCore;

// DbContext = a "ponte" entre o C# e o banco de dados (ORM).
// Cada DbSet<T> representa uma TABELA.
public class AppDataContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    // Configura QUAL banco usar. SQLite = um arquivo local (Ecommerce.db),
    // não precisa instalar servidor de banco.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Ecommerce.db");
    }
}
