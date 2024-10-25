using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Promcoser.DOMAIN.Core.Entities;

namespace Promcoser.DOMAIN.Data;

public partial class PromcoserContext : DbContext
{
    public PromcoserContext()
    {
    }

    public PromcoserContext(DbContextOptions<PromcoserContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<DetalleParteDiario> DetalleParteDiario { get; set; }

    public virtual DbSet<LugarTrabajo> LugarTrabajo { get; set; }

    public virtual DbSet<Maquinaria> Maquinaria { get; set; }

    public virtual DbSet<ParteDiario> ParteDiario { get; set; }

    public virtual DbSet<Personal> Personal { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-Q14I5L6M;Database=PROMCOSER;Integrated Security=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__cliente__677F38F55FF2FA2F");

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Apellido)
                .HasColumnType("text")
                .HasColumnName("apellido");
            entity.Property(e => e.CorreoElectronico)
                .HasColumnType("text")
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasColumnType("text")
                .HasColumnName("nombre");
            entity.Property(e => e.RazonSocial)
                .HasColumnType("text")
                .HasColumnName("razon_social");
            entity.Property(e => e.Ruc)
                .HasColumnType("text")
                .HasColumnName("ruc");
            entity.Property(e => e.Telefono)
                .HasColumnType("text")
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<DetalleParteDiario>(entity =>
        {
            entity.HasKey(e => e.IdDetalleParte).HasName("PK__detalle___BE4FF412943BBD8E");

            entity.ToTable("detalle_parte_diario");

            entity.Property(e => e.IdDetalleParte).HasColumnName("id_detalle_parte");
            entity.Property(e => e.CantidadAceite)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cantidad_aceite");
            entity.Property(e => e.CantidadPetroleo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cantidad_petroleo");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Horas)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("horas");
            entity.Property(e => e.IdParteDiario).HasColumnName("id_parte_diario");
            entity.Property(e => e.TrabajoEfectuado)
                .HasColumnType("text")
                .HasColumnName("trabajo_efectuado");

            entity.HasOne(d => d.IdParteDiarioNavigation).WithMany(p => p.DetalleParteDiario)
                .HasForeignKey(d => d.IdParteDiario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detalle_p__id_pa__46E78A0C");
        });

        modelBuilder.Entity<LugarTrabajo>(entity =>
        {
            entity.HasKey(e => e.IdLugarTrabajo).HasName("PK__lugar_tr__0E92C31AE75577EC");

            entity.ToTable("lugar_trabajo");

            entity.Property(e => e.IdLugarTrabajo).HasColumnName("id_lugar_trabajo");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Maquinaria>(entity =>
        {
            entity.HasKey(e => e.IdMaquinaria).HasName("PK__maquinar__8B61DA971856FECE");

            entity.ToTable("maquinaria");

            entity.Property(e => e.IdMaquinaria).HasColumnName("id_maquinaria");
            entity.Property(e => e.AnoFabricacion).HasColumnName("ano_fabricacion");
            entity.Property(e => e.Estado)
                .HasColumnType("text")
                .HasColumnName("estado");
            entity.Property(e => e.HorasFin)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("horas_fin");
            entity.Property(e => e.HorasInicio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("horas_inicio");
            entity.Property(e => e.HorometroFin)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("horometro_fin");
            entity.Property(e => e.HorometroInicio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("horometro_inicio");
            entity.Property(e => e.Marca)
                .HasColumnType("text")
                .HasColumnName("marca");
            entity.Property(e => e.Modelo)
                .HasColumnType("text")
                .HasColumnName("modelo");
            entity.Property(e => e.Placa)
                .HasColumnType("text")
                .HasColumnName("placa");
            entity.Property(e => e.TipoMaquinaria)
                .HasColumnType("text")
                .HasColumnName("tipo_maquinaria");
        });

        modelBuilder.Entity<ParteDiario>(entity =>
        {
            entity.HasKey(e => e.IdParte).HasName("PK__parte_di__3F12D5844BA59C19");

            entity.ToTable("parte_diario");

            entity.Property(e => e.IdParte).HasColumnName("id_parte");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Fin)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fin");
            entity.Property(e => e.Firmas)
                .HasColumnType("text")
                .HasColumnName("firmas");
            entity.Property(e => e.HorometroFinal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("horometro_final");
            entity.Property(e => e.HorometroInicio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("horometro_inicio");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdLugarTrabajo).HasColumnName("id_lugar_trabajo");
            entity.Property(e => e.IdMaquinaria).HasColumnName("id_maquinaria");
            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.Inicio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("inicio");
            entity.Property(e => e.Serie)
                .HasColumnType("text")
                .HasColumnName("serie");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__parte_dia__id_cl__412EB0B6");

            entity.HasOne(d => d.IdLugarTrabajoNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdLugarTrabajo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__parte_dia__id_lu__4316F928");

            entity.HasOne(d => d.IdMaquinariaNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdMaquinaria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__parte_dia__id_ma__440B1D61");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.ParteDiario)
                .HasForeignKey(d => d.IdPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__parte_dia__id_pe__4222D4EF");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.IdPersonal).HasName("PK__personal__418FB808483B6D80");

            entity.ToTable("personal");

            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.Apellido)
                .HasColumnType("text")
                .HasColumnName("apellido");
            entity.Property(e => e.CorreoElectronico)
                .HasColumnType("text")
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.Dni)
                .HasColumnType("text")
                .HasColumnName("dni");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasColumnType("text")
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasColumnType("text")
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Personal)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__personal__id_rol__47DBAE45");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__rol__6ABCB5E071634553");

            entity.ToTable("rol");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.DescripcionRol)
                .HasColumnType("text")
                .HasColumnName("descripcion_rol");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83F6C9715C2");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.Country)
                .HasColumnType("text")
                .HasColumnName("country");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasColumnType("text")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasColumnType("text")
                .HasColumnName("first_name");
            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasColumnType("text")
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("type");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPersonal)
                .HasConstraintName("FK_usuarios_personal");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
