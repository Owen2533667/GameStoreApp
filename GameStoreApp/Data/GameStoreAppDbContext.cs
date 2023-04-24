using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GameStoreApp.Data
{
    public class GameStoreAppDbContext : DbContext
    {
        public GameStoreAppDbContext(DbContextOptions<GameStoreAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VoiceActor_Game>().HasKey(vag => new
            {
                vag.VoiceActorId,
                vag.GameId
            });

            modelBuilder.Entity<VoiceActor_Game>().HasOne(g => g.Game).WithMany(vag => vag.VoiceActors_Games).HasForeignKey(g => g.GameId);
            modelBuilder.Entity<VoiceActor_Game>().HasOne(g => g.VoiceActor).WithMany(vag => vag.VoiceActors_Games).HasForeignKey(g => g.VoiceActorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<VoiceActor> VoiceActors { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<VoiceActor_Game> VoiceActors_Games { get; set; }
        public DbSet<GameDeveloper> GameDevelopers { get; set; }
        public DbSet<GamePublisher> GamePublishers { get; set; }

        //orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
