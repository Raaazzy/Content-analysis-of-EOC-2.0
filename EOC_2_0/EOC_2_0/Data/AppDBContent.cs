using Microsoft.EntityFrameworkCore;
using EOC_2_0.Data.Models;

namespace EOC_2_0.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<Verb> Verbs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            modelBuilder.Entity<Verb>(builder =>
            {
                builder.ToTable("ClassifiedVerbs").HasKey(x => x.Id);
            });

            
        }
    }
}
