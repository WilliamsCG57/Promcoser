using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PromcoserDOMAIN.Core.Entities;

namespace PromcoserDOMAIN.Data;

public partial class PromcoserDbContext : DbContext
{
    public PromcoserDbContext()
    {
    }

    public PromcoserDbContext(DbContextOptions<PromcoserDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<DetalleParteDiario> DetalleParteDiario { get; set; }

    public virtual DbSet<LugarTrabajo> LugarTrabajo { get; set; }

    public virtual DbSet<Maquinaria> Maquinaria { get; set; }

    public virtual DbSet<Marca> Marca { get; set; }

    public virtual DbSet<ParteDiario> ParteDiario { get; set; }

    public virtual DbSet<Personal> Personal { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-68O5AFS\\SQLEXPRESS;Database=PromcoserDB;Integrated Security=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D59466423F7348E2");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ruc)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("RUC");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetalleParteDiario>(entity =>
        {
            entity.HasKey(e => e.IdDetalleParteDiario).HasName("PK__DetalleP__520DE46AF0746D21");

            entity.Property(e => e.Ocurrencias)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TrabajoEfectuado)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdParteDiarioNavigation).WithMany(p => p.DetalleParteDiario)
                .HasForeignKey(d => d.IdParteDiario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePa__IdPar__4CA06362");
        });

        modelBuilder.Entity<LugarTrabajo>(entity =>
        {
            entity.HasKey(e => e.IdLugarTrabajo).HasName("PK__LugarTra__752BD4575F6F285B");

            entity.HasIndex(e => e.Descripcion, "UQ__LugarTra__92C53B6C51E0CD6D").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Maquinaria>(entity =>
        {
            entity.HasKey(e => e.IdMaquinaria).HasName("PK__Maquinar__E4649F30CA910BCB");

            entity.Property(e => e.HorometroActual).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HorometroCompra).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Placa)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TipoMaquinaria)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Maquinaria)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maquinari__IdMar__403A8C7D");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marca__4076A887EFFF00DB");

            entity.Property(e => e.NombreMarca)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ParteDiario>(entity =>
        {
            entity.HasKey(e => e.IdParteDiario).HasName("PK__ParteDia__7BA7DAE2452EB73D");

            entity.Property(e => e.CantidadAceite).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CantidadPetroleo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Firmas)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.HorometroFinal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HorometroInicio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Serie)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ParteDiar__IdCli__46E78A0C");

            entity.HasOne(d => d.IdLugarTrabajoNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdLugarTrabajo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ParteDiar__IdLug__48CFD27E");

            entity.HasOne(d => d.IdMaquinariaNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdMaquinaria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ParteDiar__IdMaq__49C3F6B7");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ParteDiar__IdPer__47DBAE45");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.IdPersonal).HasName("PK__Personal__05A9201B75602719");

            entity.HasIndex(e => e.Usuario, "UQ__Personal__E3237CF740E2A4B0").IsUnique();

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Personal__E3237CF740E2A4B1").IsUnique();

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Dni)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Personal)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Personal__IdRol__440B1D61");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C10E2F97A");

            entity.Property(e => e.DescripcionRol)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
