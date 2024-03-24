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
        public DbSet<Noun> Nouns { get; set; }

        public DbSet<NewVerb> NewVerbs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            modelBuilder.Entity<Verb>(builder =>
            {
                builder.ToTable("ClassifiedVerbs").HasKey(x => x.Id);
            });

            modelBuilder.Entity<Noun>(builder =>
            {
                builder.ToTable("Nouns").HasKey(x => x.Id);
            });

            modelBuilder.Entity<NewVerb>(builder =>
            {
                builder.ToTable("NewVerbs").HasKey(x => x.Id);
            });
        }
    }
}
