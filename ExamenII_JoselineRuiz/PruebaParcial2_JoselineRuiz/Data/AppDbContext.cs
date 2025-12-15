using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaParcial2_JoselineRuiz.Models;

namespace PruebaParcial2_JoselineRuiz.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PruebaParcial2_JoselineRuiz.Models.Tarea> Tarea { get; set; } = default!;
        public DbSet<PruebaParcial2_JoselineRuiz.Models.Meta> Meta { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //configuración de la relación Meta-Tarea
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Meta)           //una Tarea tiene una Meta
                .WithMany(m => m.Tareas)       //una Meta tiene muchas Tareas
                .HasForeignKey(t => t.MetaId)  //la clave foránea es MetaId
                .OnDelete(DeleteBehavior.Cascade); //eliminar Meta elimina sus Tareas

            //configuración para guardar Enums como string
            modelBuilder.Entity<Tarea>()
                .Property(t => t.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Tarea>()
                .Property(t => t.Dificultad)
                .HasConversion<string>();

            modelBuilder.Entity<Meta>()
                .Property(m => m.Categoria)
                .HasConversion<string>();

            modelBuilder.Entity<Meta>()
                .Property(m => m.Prioridad)
                .HasConversion<string>();

            modelBuilder.Entity<Meta>()
                .Property(m => m.Estado)
                .HasConversion<string>();
        }
    }
}
