using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyect.Models;

public partial class ProyectContext : DbContext
{
    public ProyectContext()
    {
    }

    public ProyectContext(DbContextOptions<ProyectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abono> Abonos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleHabitacione> DetalleHabitaciones { get; set; }

    public virtual DbSet<DetallePaquete> DetallePaquetes { get; set; }

    public virtual DbSet<DetalleServicio> DetalleServicios { get; set; }

    public virtual DbSet<EstadoReserva> EstadoReservas { get; set; }

    public virtual DbSet<HabitacionMueble> HabitacionMuebles { get; set; }

    public virtual DbSet<Habitacione> Habitaciones { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Mueble> Muebles { get; set; }

    public virtual DbSet<Paquete> Paquetes { get; set; }

    public virtual DbSet<PaquetesHabitacione> PaquetesHabitaciones { get; set; }

    public virtual DbSet<PaquetesServicio> PaquetesServicios { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<RolesPermiso> PermisosRoles { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoHabitacione> TipoHabitaciones { get; set; }

    public virtual DbSet<TipoMueble> TipoMuebles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abono>(entity =>
        {
            entity.HasKey(e => e.IdAbono).HasName("PK__Abonos__A4693DA721191025");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaAbono)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Porcentaje).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Valordeuda).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Abonos)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Abonos__IdReserv__73BA3083");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.NroDocumento).HasName("PK__Clientes__CC62C91C0FD76D92");

            entity.Property(e => e.NroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Clientes__IdRol__440B1D61");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdTipoDocumento)
                .HasConstraintName("FK__Clientes__IdTipo__44FF419A");
        });

        modelBuilder.Entity<DetalleHabitacione>(entity =>
        {
            entity.HasKey(e => e.IdDetalleHabitacion).HasName("PK__DetalleH__15D58EF954CB6EC4");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.DetalleHabitaciones)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleHa__IdHab__02084FDA");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.DetalleHabitaciones)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleHa__IdRes__01142BA1");
        });

        modelBuilder.Entity<DetallePaquete>(entity =>
        {
            entity.HasKey(e => e.IdDetallePaquete).HasName("PK__DetalleP__4DD779D70D3D022B");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.DetallePaquetes)
                .HasForeignKey(d => d.IdPaquete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePa__IdPaq__7D439ABD");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.DetallePaquetes)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePa__IdRes__7C4F7684");
        });

        modelBuilder.Entity<DetalleServicio>(entity =>
        {
            entity.HasKey(e => e.IdDetalleServicio).HasName("PK__DetalleS__0BFF94E659B8F2D8");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.DetalleServicios)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleSe__IdRes__778AC167");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.DetalleServicios)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleSe__IdSer__787EE5A0");
        });

        modelBuilder.Entity<EstadoReserva>(entity =>
        {
            entity.HasKey(e => e.IdEstadoReserva).HasName("PK__EstadoRe__3E654CA87B7916B9");

            entity.ToTable("EstadoReserva");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estados)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HabitacionMueble>(entity =>
        {
            entity.HasKey(e => e.IdHabitacionMueble).HasName("PK__Habitaci__402E049D1A5D697A");

            entity.ToTable("HabitacionMueble");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.HabitacionMuebles)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__IdHab__5441852A");

            entity.HasOne(d => d.IdMuebleNavigation).WithMany(p => p.HabitacionMuebles)
                .HasForeignKey(d => d.IdMueble)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__IdMue__5535A963");
        });

        modelBuilder.Entity<Habitacione>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__8BBBF9012FE52C77");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdTipoHabitacionNavigation).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.IdTipoHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__IdTip__5165187F");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__MetodoPa__6F49A9BE5A1860C3");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Mueble>(entity =>
        {
            entity.HasKey(e => e.IdMueble).HasName("PK__Muebles__829D15E87274E0C5");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoMuebleNavigation).WithMany(p => p.Muebles)
                .HasForeignKey(d => d.IdTipoMueble)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Muebles__IdTipoM__4BAC3F29");
        });

        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("PK__Paquetes__DE278F8BB68EA14E");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<PaquetesHabitacione>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteHabitacion).HasName("PK__Paquetes__C0EF4AF0EE063B80");

            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.PaquetesHabitaciones)
                .HasForeignKey(d => d.IdHabitacion)
                .HasConstraintName("FK__PaquetesH__IdHab__5EBF139D");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.PaquetesHabitaciones)
                .HasForeignKey(d => d.IdPaquete)
                .HasConstraintName("FK__PaquetesH__IdPaq__5DCAEF64");
        });

        modelBuilder.Entity<PaquetesServicio>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteServicio).HasName("PK__Paquetes__DE5C2BC2D356DFBA");

            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.PaquetesServicios)
                .HasForeignKey(d => d.IdPaquete)
                .HasConstraintName("FK__PaquetesS__IdPaq__619B8048");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.PaquetesServicios)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("FK__PaquetesS__IdSer__628FA481");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__Permisos__0D626EC8C4FEAF3A");

            entity.Property(e => e.NomPermiso)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolesPermiso>(entity =>
        {
            entity.HasKey(e => e.IdRolPermiso).HasName("PK__Permisos__8C257ED9DEF51E71");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.PermisosRoles)
                .HasForeignKey(d => d.IdPermiso)
                .HasConstraintName("FK__PermisosR__IdPer__3C69FB99");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.PermisosRoles)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__PermisosR__IdRol__3B75D760");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reservas__0E49C69D9DCB5694");

            entity.Property(e => e.Descuento).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.FechaReserva)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("IVA");
            entity.Property(e => e.NroDocumentoCliente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdEstadoReservaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEstadoReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__IdEsta__6E01572D");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdMetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__IdMeto__6EF57B66");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdPaquete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__IdPaqu__6D0D32F4");

            entity.HasOne(d => d.NroDocumentoClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.NroDocumentoCliente)
                .HasConstraintName("FK__Reservas__NroDoc__6B24EA82");

            entity.HasOne(d => d.NroDocumentoUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.NroDocumentoUsuario)
                .HasConstraintName("FK__Reservas__NroDoc__6C190EBB");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584CE94D5DA1");

            entity.Property(e => e.NomRol)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__2DCCF9A2CC758038");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("PK__TipoDocu__3AB3332F3570EC58");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.NomTipoDcumento)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoHabitacione>(entity =>
        {
            entity.HasKey(e => e.IdTipoHabitacion).HasName("PK__TipoHabi__AB75E87C3DF2E2D9");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoMueble>(entity =>
        {
            entity.HasKey(e => e.IdTipoMueble).HasName("PK__TipoMueb__C451A954473656E7");

            entity.ToTable("TipoMueble");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.NroDocumento).HasName("PK__Usuarios__CC62C91C86562C09");

            entity.Property(e => e.NroDocumento).ValueGeneratedNever();
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuarios__IdRol__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
