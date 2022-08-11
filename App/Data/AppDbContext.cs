using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data {
    public class AppDbContext : DbContext{

        private IConfiguration _configuration;
        public AppDbContext(IConfiguration configuration) {
            this._configuration = configuration;
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vacina> Vacina { get; set; }
        public DbSet<CarteiraTrabalho> CarteirasTrabalho { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<ClienteVacina> ClientesVacinas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CONEXAO_BANCO"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Cliente
            modelBuilder.Entity<Cliente>()
                .ToTable("Clientes");

            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Nome)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Cliente>()
                .Property(c => c.DataNascimento);

            // mapeamento da relação de um pra um
            modelBuilder.Entity<Cliente>()
                .HasOne<CarteiraTrabalho>(cliente => cliente.CarteiraTrabalho)
                .WithOne(c => c.Cliente)
                .HasForeignKey<CarteiraTrabalho>(c => c.ClienteId);

            // Carteira de trabalho
            modelBuilder.Entity<CarteiraTrabalho>()
                .ToTable("CarteirasTrabalho");

            modelBuilder.Entity<CarteiraTrabalho>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CarteiraTrabalho>()
                .Property(c => c.PisPasep)
                .HasMaxLength(20)
                .IsRequired();

            // Exame
            modelBuilder.Entity<Exame>()
                .ToTable("Exames");

            modelBuilder.Entity<Exame>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Exame>()
                .Property(e => e.CodigoTuss)
                .HasMaxLength(20);

            modelBuilder.Entity<Exame>()
                .Property(e => e.DataAgendamento)
                .IsRequired();

            // mapeamento 1 pra N
            modelBuilder.Entity<Exame>()
                .HasOne<Cliente>(e => e.Cliente)
                .WithMany(c => c.Exames)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vacina
            modelBuilder.Entity<Vacina>()
                .ToTable("Vacinas");

            modelBuilder.Entity<Vacina>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<Vacina>()
                .Property(v => v.Nome)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Vacina>()
                .Property(v => v.NumeroDoses)
                .IsRequired()
                .HasDefaultValue(1);

            // Clientes Vacinas
            modelBuilder.Entity<ClienteVacina>()
                .ToTable("ClientesVacinas");

            modelBuilder.Entity<ClienteVacina>()
                .HasKey(cv => new { cv.ClienteId,cv.VacinaId });

            modelBuilder.Entity<ClienteVacina>()
                .Property(cv => cv.DataAplicacao)
                .IsRequired();

            // Relacionamento de N pra N 
            modelBuilder.Entity<ClienteVacina>()
                .HasOne<Cliente>(c => c.Cliente)
                .WithMany()
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClienteVacina>()
                .HasOne<Vacina>(c => c.Vacina)
                .WithMany()
                .HasForeignKey(c => c.VacinaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
