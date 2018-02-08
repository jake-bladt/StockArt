using System.Linq;
using Microsoft.EntityFrameworkCore;
using StockArt.Domain;


namespace StockArt.Data
{
    public class StockArtDBContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ImageSet> ImageSets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=sart00;Integrated Security=True;Pooling=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageSet>().HasKey(s => s.Name);
            modelBuilder.Entity<ImageSetSubject>().HasKey(iss => new { iss.ImageSetName, iss.SubjectID });
            base.OnModelCreating(modelBuilder);
        }

        public ImageSet CanonicalImageSet(string name)
        {
            return ImageSets
                .Include(img => img.ImageSetSubjects)
                .ThenInclude(iss => iss.Subject)
                .FirstOrDefault(ims => ims.Name == name);
        }

    }
}
