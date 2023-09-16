
using Azure;
using CashbackApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashbackApi.Data {
    public class DataContext : DbContext {
        public DbSet<Categories> Category { get; set; }
        public DbSet<CardInfos> CardInfo { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //many to many
            modelBuilder.Entity<Categories>(x => {
                x.HasKey(x => x.CategoryId);
                x.Property(x => x.CategoryId)
                .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CardInfos>(x => {
                x.HasKey(x => x.CardId);
                x.HasMany(x => x.Category)
                .WithMany(x => x.Cardinfo)
                .UsingEntity("CategoryCard",
                l => l.HasOne(typeof(Categories)).WithMany().HasForeignKey("CategoryId").HasPrincipalKey(nameof(Categories.CategoryId)),
                r => r.HasOne(typeof(CardInfos)).WithMany().HasForeignKey("CardId").HasPrincipalKey(nameof(CardInfos.CardId)),
                j => j.HasKey("CategoryId", "CardId"));

            });


        }

    }


}




