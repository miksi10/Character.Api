using Microsoft.EntityFrameworkCore;
using CharacterApi.Models;

namespace CharacterApi.DbContext
{
    public class CharacterDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Character> Character {  get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<CharacterItem> CharacterItem { get; set; }
        public CharacterDbContext(DbContextOptions<CharacterDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasOne(ch => ch.Class)
                .WithMany(cl => cl.Characters)
                .HasForeignKey(ch => ch.ClassId);

            modelBuilder.Entity<Character>()
                .HasMany(c => c.Items)
                .WithMany(c => c.Characters)
                .UsingEntity<CharacterItem>();

            modelBuilder.Entity<Class>()
                .HasIndex(c => c.Name).IsUnique(true);
        }
    }
}
