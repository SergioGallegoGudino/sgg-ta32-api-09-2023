using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace TA32_1_sgallego.Models
{
    public class PiezaContext : DbContext
    {
        public PiezaContext(DbContextOptions<PiezaContext> options) : base(options) { }
        public DbSet<Pieza> Piezas { get; set; } = null;
        public DbSet<Proveedor> Proveedores { get; set; } = null;

        public DbSet<Suministra> Suministras { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pieza>()
                .HasKey(e => new { e.Codigo });

            modelBuilder.Entity<Proveedor>()
                .HasKey(e => new { e.Id});


            modelBuilder.Entity<Suministra>()
                .HasKey(e => new { e.ProveedorId, e.PiezaCodigo});
            modelBuilder.Entity<Suministra>()
                .HasOne(e => e.Pieza)
                .WithMany(e => e.Suministra)
                .HasForeignKey(e => e.PiezaCodigo);
            modelBuilder.Entity<Suministra>()
                .HasOne(e => e.Proveedor)
                .WithMany(e => e.Suministra)
                .HasForeignKey(e => e.ProveedorId);
        }
    }
}
