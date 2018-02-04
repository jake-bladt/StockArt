using Microsoft.EntityFrameworkCore;

using StockArt.Domain;


namespace StockArt.Data
{
    public class StockArtDBContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=sart00;Integrated Security=True;Pooling=False");
        }

    }
}
