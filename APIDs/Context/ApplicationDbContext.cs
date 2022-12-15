using Microsoft.EntityFrameworkCore;
using APIDs.Entities;

namespace APIDs.Context

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Ground> Grounds { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<Creatures> Creatures { get; set; }
        public DbSet<CardColor> CardColors { get; set; }
    }
}
