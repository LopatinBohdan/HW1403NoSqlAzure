using Microsoft.EntityFrameworkCore;

namespace HW1403NoSql.Models
{
    public class MyContext:DbContext
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Good>().ToContainer("Goods");
            modelBuilder.Entity<Category>().ToContainer("Category");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                "AccountEndpoint=https://lopatin-dbnosql-hw.documents.azure.com:443/;AccountKey=Z6a5AMWOK4U6U87xBsugqTykEyzW4zRbHRdUc533B1Ti8aUdxbceKm0LjAE4DfP9sjnoIY9iHyXeACDbWhrWww==;",
                "MyDB");
        }
    }
}
