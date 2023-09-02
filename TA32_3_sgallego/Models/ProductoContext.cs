using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace TA32_1_sgallego.Models
{
    public class ProductoContext : DbContext
    {
        public ProductoContext(DbContextOptions<ProductoContext> options) : base(options) { }
        public DbSet<Cajero> Cajeros { get; set; } = null;
        public DbSet<Maquina> Maquinas { get; set; } = null;
        public DbSet<Producto> Productos{ get; set; } = null;
        public DbSet<Venta> Ventas{ get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cajero>()
                .HasKey(e => new { e.Codigo});

            modelBuilder.Entity<Maquina>()
                .HasKey(e => new { e.Codigo});

            modelBuilder.Entity<Producto>()
                .HasKey(e => new { e.Codigo});

            modelBuilder.Entity<Venta>()
                .HasKey(e => new { e.MaquinaCodigo, e.ProductoCodigo, e.CajeroCodigo});
            modelBuilder.Entity<Venta>()
                .HasOne(e => e.Maquina)
                .WithMany(e => e.Ventas)
                .HasForeignKey(e => e.MaquinaCodigo);
            modelBuilder.Entity<Venta>()
                .HasOne(e => e.Producto)
                .WithMany(e => e.Ventas)
                .HasForeignKey(e => e.ProductoCodigo);
            modelBuilder.Entity<Venta>()
                .HasOne(e => e.Cajero)
                .WithMany(e => e.Ventas)
                .HasForeignKey(e => e.CajeroCodigo);
        }
    }
}
