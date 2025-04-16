using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion; // IMPORTANTE!
using Telecom.Entities;

namespace Telecom.DataBase;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<OperadoraServico> OperadorasServicos { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Fatura> Faturas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //O POSTGREE PRECISA DESSE COMANDO para padronizar datas 
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, DateTimeKind.Utc) : v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
        );

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
            }
        }

        // Configurações da tabela OperadoraServico
        modelBuilder.Entity<OperadoraServico>(entity =>
        {
            entity.ToTable("operadoras_servicos");
            entity.HasKey(o => o.Id);
            entity.Property(o => o.NomeOperadora).HasMaxLength(100).IsRequired();
            entity.Property(o => o.TipoServico).HasMaxLength(50).IsRequired();
            entity.Property(o => o.ContatoSuporte).HasMaxLength(100);
        });

        // Configurações da tabela Contrato
        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.ToTable("contratos");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();
            entity.Property(c => c.NomeFilial).HasMaxLength(100).IsRequired();
            entity.Property(c => c.PlanoContratado).HasMaxLength(100).IsRequired();
            entity.Property(c => c.ValorMensal).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(c => c.Status).IsRequired();

            entity.HasOne(c => c.OperadoraServico)
                  .WithMany()
                  .HasForeignKey(c => c.OperadoraId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configurações da tabela Fatura
        modelBuilder.Entity<Fatura>(entity =>
        {
            entity.ToTable("faturas");
            entity.Property(e => e.ContratoId).HasColumnName("contrato_id");
            entity.Property(e => e.DataEmissao).HasColumnName("data_emissao");
            entity.Property(e => e.DataVencimento).HasColumnName("data_vencimento");
            entity.Property(e => e.ValorCobrado).HasColumnName("valor_cobrado");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(f => f.Contrato)
                  .WithMany(c => c.Faturas)
                  .HasForeignKey(f => f.ContratoId);
        });
    }
}
