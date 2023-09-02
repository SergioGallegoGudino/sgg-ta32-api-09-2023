using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace TA32_1_sgallego.Models
{
    public class InvestigadorContext : DbContext
    {
        public InvestigadorContext(DbContextOptions<InvestigadorContext> options) : base(options) { }
        public DbSet<Investigador> Investigadores{ get; set; } = null;
        public DbSet<Equipo> Equipos{ get; set; } = null;
        public DbSet<Facultad> Facultades{ get; set; } = null;
        public DbSet<Reserva> Reservas{ get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Investigador>()
                .HasKey(e => new { e.Dni});

            modelBuilder.Entity<Equipo>()
                .HasKey(e => new { e.NumSerie});

            modelBuilder.Entity<Facultad>()
                            .HasKey(e => new { e.Codigo});
            
            modelBuilder.Entity<Reserva>()
                .HasKey(e => new { e.EquipoNum, e.InvestigadorDni});
            modelBuilder.Entity<Reserva>()
                .HasOne(e => e.Equipo)
                .WithMany(e => e.Reservas)
                .HasForeignKey(e => e.EquipoNum);
            modelBuilder.Entity<Reserva>()
                .HasOne(e => e.Investigador)
                .WithMany(e => e.Reservas)
                .HasForeignKey(e => e.InvestigadorDni);
            modelBuilder.Entity<Investigador>()
                .HasOne(e => e.Facultad)
                .WithMany(e => e.Investigadores)
                .HasForeignKey(e => e.FacultadCodigo);
            modelBuilder.Entity<Equipo>()
                .HasOne(e => e.Facultad)
                .WithMany(e => e.Equipos)
                .HasForeignKey(e => e.FacultadCodigo);
        }
    }
}
