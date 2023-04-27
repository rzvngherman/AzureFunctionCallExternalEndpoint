using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace AZCECoreWebApi.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecast { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>()
                .HasKey(b => b.WeatherForecastId);

            //modelBuilder
            //.Entity<WeatherForecast>(
            //    eb =>
            //    {
            //        //eb.HasNoKey();
            //        //eb.ToView("View_BlogPostCounts");
            //        //eb.Property(v => v.BlogName).HasColumnName("Name");
            //    });

            base.OnModelCreating(modelBuilder);
        }
    }
}
