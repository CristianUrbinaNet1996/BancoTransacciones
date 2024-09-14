using System;
using System.Collections.Generic;
using Core.Infraestructure.Utils;
using DTO.DTO.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Infraestructure.Models;

public partial class BcuentasContext : DbContext
{
    public BcuentasContext()
    {
    }

    public BcuentasContext(DbContextOptions<BcuentasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<EstadosCuentum> EstadosCuenta { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<ParametrosConfiguracion> ParametrosConfiguracions { get; set; }

    public virtual DbSet<TarjetasCredito> TarjetasCreditos { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }
    public virtual DbSet<sp_login> sp_LoginResult {  get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0A7415EC783");

            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        modelBuilder.Entity<EstadosCuentum>(entity =>
        {
            entity.HasKey(e => e.EstadoCuentaId).HasName("PK__EstadosC__B3859D897B03B443");

            entity.Property(e => e.EstadoCuentaId).HasColumnName("EstadoCuentaID");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.InteresBonificable).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PagoContadoConInteres).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PagoMinimo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SaldoMesActual).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SaldoMesAnterior).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TarjetaId).HasColumnName("TarjetaID");

            entity.HasOne(d => d.Tarjeta).WithMany(p => p.EstadosCuenta)
                .HasForeignKey(d => d.TarjetaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EstadosCuenta_TarjetasCredito");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Logs__3214EC074498270D");

            entity.Property(e => e.Level).HasMaxLength(128);
        });

        modelBuilder.Entity<ParametrosConfiguracion>(entity =>
        {
            entity.HasKey(e => e.ParametroId).HasName("PK__Parametr__2B3CE67233EB8F2A");

            entity.ToTable("ParametrosConfiguracion");

            entity.Property(e => e.ParametroId).HasColumnName("ParametroID");
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PorcentajeInteres).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PorcentajePagoMinimo).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<TarjetasCredito>(entity =>
        {
            entity.HasKey(e => e.TarjetaId).HasName("PK__Tarjetas__C8250696087A3E6E");

            entity.ToTable("TarjetasCredito");

            entity.HasIndex(e => e.NumeroTarjeta, "UQ__Tarjetas__BC163C0A2E868EF1").IsUnique();

            entity.Property(e => e.TarjetaId).HasColumnName("TarjetaID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Cvv)
                .HasMaxLength(3)
                .HasColumnName("CVV");
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("date");
            entity.Property(e => e.LimiteCredito).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NumeroTarjeta).HasMaxLength(16);
            entity.Property(e => e.SaldoActual).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SaldoDisponible)
                .HasComputedColumnSql("([LimiteCredito]-[SaldoActual])", false)
                .HasColumnType("decimal(11, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.TarjetasCreditos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TarjetasCredito_Clientes");

            entity.HasOne(d => d.ConfiguracionNavigation).WithMany(p => p.TarjetasCreditos)
                .HasForeignKey(d => d.Configuracion)
                .HasConstraintName("FK__TarjetasC__Confi__6383C8BA");
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.TransaccionId).HasName("PK__Transacc__86A849DE739C4768");

            entity.Property(e => e.TransaccionId).HasColumnName("TransaccionID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaTransaccion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TarjetaId).HasColumnName("TarjetaID");

            entity.HasOne(d => d.Tarjeta).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.TarjetaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Tarje__66603565");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
