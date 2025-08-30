namespace Taskflow.Domain.ModelsPortal;

public partial class PortalContext : DbContext
{
    public PortalContext()
    {
    }

    public PortalContext(DbContextOptions<PortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TAuditLog> TAuditLogs { get; set; }

    public virtual DbSet<TModulo> TModulos { get; set; }

    public virtual DbSet<TPermiso> TPermisos { get; set; }

    public virtual DbSet<TPersona> TPersonas { get; set; }

    public virtual DbSet<TRecoveryToken> TRecoveryTokens { get; set; }

    public virtual DbSet<TRole> TRoles { get; set; }

    public virtual DbSet<TRolesPermiso> TRolesPermisos { get; set; }

    public virtual DbSet<TTipoDocumento> TTipoDocumentos { get; set; }

    public virtual DbSet<TUsuario> TUsuarios { get; set; }

    public virtual DbSet<TUsuariosRole> TUsuariosRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IEntityAuditable).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(CreateFilterExpression(entityType.ClrType));
            }
        }

        modelBuilder.HasDefaultSchema("PORTAL");

        modelBuilder.Entity<TAuditLog>(entity =>
        {
            entity.HasKey(e => e.IdLog).HasName("T_AUDIT_LOG_PK");

            entity.ToTable("T_AUDIT_LOG");

            entity.Property(e => e.IdLog)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_LOG");
            entity.Property(e => e.Accion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ACCION");
            entity.Property(e => e.FecBaja)
                .HasColumnType("DATE")
                .HasColumnName("FEC_BAJA");
            entity.Property(e => e.FecIng)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("FEC_ING");
            entity.Property(e => e.FecMod)
                .HasColumnType("DATE")
                .HasColumnName("FEC_MOD");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.RegistroAfectado)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("REGISTRO_AFECTADO");
            entity.Property(e => e.TablaAfectada)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TABLA_AFECTADA");
            entity.Property(e => e.UsrBaja)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_BAJA");
            entity.Property(e => e.UsrIng)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_ING");
            entity.Property(e => e.UsrMod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_MOD");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TAuditLogs)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("T_AUDIT_LOG_USUARIO_FK");
        });

        modelBuilder.Entity<TModulo>(entity =>
        {
            entity.HasKey(e => e.IdModulo).HasName("T_MODULO_PK");

            entity.ToTable("T_MODULO");

            entity.HasIndex(e => e.Descripcion, "T_MODULO_DESCRIPCION_UQ").IsUnique();

            entity.Property(e => e.IdModulo)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_MODULO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.FecBaja)
                .HasColumnType("DATE")
                .HasColumnName("FEC_BAJA");
            entity.Property(e => e.FecIng)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("FEC_ING");
            entity.Property(e => e.FecMod)
                .HasColumnType("DATE")
                .HasColumnName("FEC_MOD");
            entity.Property(e => e.UsrBaja)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_BAJA");
            entity.Property(e => e.UsrIng)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_ING");
            entity.Property(e => e.UsrMod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_MOD");
        });

        modelBuilder.Entity<TPermiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("T_PERMISOS_PK");

            entity.ToTable("T_PERMISOS");

            entity.HasIndex(e => e.NombrePermiso, "T_PERMISOS_NOMBRE_UQ").IsUnique();

            entity.Property(e => e.IdPermiso)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_PERMISO");
            entity.Property(e => e.Activo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'S' ")
                .IsFixedLength()
                .HasColumnName("ACTIVO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.FecBaja)
                .HasColumnType("DATE")
                .HasColumnName("FEC_BAJA");
            entity.Property(e => e.FecIng)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("FEC_ING");
            entity.Property(e => e.FecMod)
                .HasColumnType("DATE")
                .HasColumnName("FEC_MOD");
            entity.Property(e => e.IdModulo)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_MODULO");
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PERMISO");
            entity.Property(e => e.UsrBaja)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_BAJA");
            entity.Property(e => e.UsrIng)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_ING");
            entity.Property(e => e.UsrMod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_MOD");

            entity.HasOne(d => d.IdModuloNavigation).WithMany(p => p.TPermisos)
                .HasForeignKey(d => d.IdModulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("T_PERMISOS_MODULO_FK");
        });

        modelBuilder.Entity<TPersona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("T_PERSONAS_PK");

            entity.ToTable("T_PERSONAS");

            entity.Property(e => e.IdPersona)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_PERSONA");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Cuil)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CUIL");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FecBaja)
                .HasColumnType("DATE")
                .HasColumnName("FEC_BAJA");
            entity.Property(e => e.FecIng)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("FEC_ING");
            entity.Property(e => e.FecMod)
                .HasColumnType("DATE")
                .HasColumnName("FEC_MOD");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_NACIMIENTO");
            entity.Property(e => e.IdTipoDocumento)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_TIPO_DOCUMENTO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.NroDocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NRO_DOCUMENTO");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
            entity.Property(e => e.UsrBaja)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_BAJA");
            entity.Property(e => e.UsrIng)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_ING");
            entity.Property(e => e.UsrMod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_MOD");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.TPersonas)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("T_PERSONAS_TIPODOC_FK");
        });

        modelBuilder.Entity<TRecoveryToken>(entity =>
        {
            entity.HasKey(e => e.IdToken).HasName("T_RECOVERY_TOKENS_PK");

            entity.ToTable("T_RECOVERY_TOKENS");

            entity.Property(e => e.IdToken)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_TOKEN");
            entity.Property(e => e.CodigoVerificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CODIGO_VERIFICACION");
            entity.Property(e => e.FecBaja)
                .HasColumnType("DATE")
                .HasColumnName("FEC_BAJA");
            entity.Property(e => e.FecIng)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("FEC_ING");
            entity.Property(e => e.FecMod)
                .HasColumnType("DATE")
                .HasColumnName("FEC_MOD");
            entity.Property(e => e.FechaExpiracion)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_EXPIRACION");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.TipoRecovery)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TIPO_RECOVERY");
            entity.Property(e => e.TokenHash)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TOKEN_HASH");
            entity.Property(e => e.Usado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'N' ")
                .IsFixedLength()
                .HasColumnName("USADO");
            entity.Property(e => e.UsrBaja)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_BAJA");
            entity.Property(e => e.UsrIng)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_ING");
            entity.Property(e => e.UsrMod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_MOD");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TRecoveryTokens)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("T_RECOVERY_TOKENS_USUARIO_FK");
        });

        modelBuilder.Entity<TRole>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("T_ROLES_PK");

            entity.ToTable("T_ROLES");

            entity.HasIndex(e => e.NombreRol, "T_ROLES_NOMBRE_UQ").IsUnique();

            entity.Property(e => e.IdRol)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_ROL");
            entity.Property(e => e.Activo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'S' ")
                .IsFixedLength()
                .HasColumnName("ACTIVO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.FecBaja)
                .HasColumnType("DATE")
                .HasColumnName("FEC_BAJA");
            entity.Property(e => e.FecIng)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("FEC_ING");
            entity.Property(e => e.FecMod)
                .HasColumnType("DATE")
                .HasColumnName("FEC_MOD");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_ROL");
            entity.Property(e => e.UsrBaja)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_BAJA");
            entity.Property(e => e.UsrIng)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_ING");
            entity.Property(e => e.UsrMod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_MOD");
        });

        modelBuilder.Entity<TRolesPermiso>(entity =>
        {
            entity.HasKey(e => e.IdRolPermiso).HasName("T_ROLES_PERMISOS_PK");

            entity.ToTable("T_ROLES_PERMISOS");

            entity.HasIndex(e => new { e.IdRol, e.IdPermiso }, "T_ROLES_PERMISOS_UQ").IsUnique();

            entity.Property(e => e.IdRolPermiso)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_ROL_PERMISO");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.FecBaja)
                .HasColumnType("DATE")
                .HasColumnName("FEC_BAJA");
            entity.Property(e => e.FecIng)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("FEC_ING");
            entity.Property(e => e.FecMod)
                .HasColumnType("DATE")
                .HasColumnName("FEC_MOD");
            entity.Property(e => e.IdPermiso)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_PERMISO");
            entity.Property(e => e.IdRol)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_ROL");
            entity.Property(e => e.UsrBaja)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_BAJA");
            entity.Property(e => e.UsrIng)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_ING");
            entity.Property(e => e.UsrMod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_MOD");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.TRolesPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("T_ROLES_PERMISOS_PERMISO_FK");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TRolesPermisos)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("T_ROLES_PERMISOS_ROL_FK");
        });

        modelBuilder.Entity<TTipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("T_TIPO_DOCUMENTO_PK");

            entity.ToTable("T_TIPO_DOCUMENTO");

            entity.Property(e => e.IdTipoDocumento)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_TIPO_DOCUMENTO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.FecBaja)
                .HasColumnType("DATE")
                .HasColumnName("FEC_BAJA");
            entity.Property(e => e.FecIng)
                .HasColumnType("DATE")
                .HasColumnName("FEC_ING");
            entity.Property(e => e.FecMod)
                .HasColumnType("DATE")
                .HasColumnName("FEC_MOD");
            entity.Property(e => e.UsrBaja)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_BAJA");
            entity.Property(e => e.UsrIng)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_ING");
            entity.Property(e => e.UsrMod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_MOD");
        });

        modelBuilder.Entity<TUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("T_USUARIOS_PK");

            entity.ToTable("T_USUARIOS");

            entity.HasIndex(e => e.Username, "UQ_T_USUARIOS_USERNAME").IsUnique();

            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.DebeCambiarPassword)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'N' ")
                .IsFixedLength()
                .HasColumnName("DEBE_CAMBIAR_PASSWORD");
            entity.Property(e => e.EmailVerificado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'N' ")
                .IsFixedLength()
                .HasColumnName("EMAIL_VERIFICADO");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("'ACTIVO'")
                .HasColumnName("ESTADO");
            entity.Property(e => e.FecBaja)
                .HasColumnType("DATE")
                .HasColumnName("FEC_BAJA");
            entity.Property(e => e.FecIng)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("FEC_ING");
            entity.Property(e => e.FecMod)
                .HasColumnType("DATE")
                .HasColumnName("FEC_MOD");
            entity.Property(e => e.FechaBloqueo)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_BLOQUEO");
            entity.Property(e => e.FechaUltimaConexion)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_ULTIMA_CONEXION");
            entity.Property(e => e.FechaUltimoCambioPass)
                .HasColumnType("DATE")
                .HasColumnName("FECHA_ULTIMO_CAMBIO_PASS");
            entity.Property(e => e.IdPersona)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_PERSONA");
            entity.Property(e => e.IntentosFallidos)
                .HasPrecision(3)
                .HasDefaultValueSql("0")
                .HasColumnName("INTENTOS_FALLIDOS");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PASSWORD_HASH");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
            entity.Property(e => e.UsrBaja)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_BAJA");
            entity.Property(e => e.UsrIng)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_ING");
            entity.Property(e => e.UsrMod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_MOD");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TUsuarios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("T_USUARIOS_PERSONAS_FK");
        });

        modelBuilder.Entity<TUsuariosRole>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioRoles).HasName("T_USUARIOS_ROLES_PK");

            entity.ToTable("T_USUARIOS_ROLES");

            entity.Property(e => e.IdUsuarioRoles)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_USUARIO_ROLES");
            entity.Property(e => e.Activo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'S' ")
                .IsFixedLength()
                .HasColumnName("ACTIVO");
            entity.Property(e => e.AsignadoPor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ASIGNADO_POR");
            entity.Property(e => e.FecBaja)
                .HasColumnType("DATE")
                .HasColumnName("FEC_BAJA");
            entity.Property(e => e.FecIng)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("FEC_ING");
            entity.Property(e => e.FecMod)
                .HasColumnType("DATE")
                .HasColumnName("FEC_MOD");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("FECHA_ASIGNACION");
            entity.Property(e => e.IdRol)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_ROL");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.UsrBaja)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_BAJA");
            entity.Property(e => e.UsrIng)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_ING");
            entity.Property(e => e.UsrMod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USR_MOD");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TUsuariosRoles)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("T_USUARIOS_ROLES_ROL_FK");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TUsuariosRoles)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("T_USUARIOS_ROLES_USUARIO_FK");
        });
        modelBuilder.HasSequence("SEQ_ID_TIPO_DOCUMENTO");
        modelBuilder.HasSequence("SEQ_T_AUDIT_LOG");
        modelBuilder.HasSequence("SEQ_T_MODULO");
        modelBuilder.HasSequence("SEQ_T_PERMISOS");
        modelBuilder.HasSequence("SEQ_T_PERSONAS");
        modelBuilder.HasSequence("SEQ_T_RECOVERY_TOKENS");
        modelBuilder.HasSequence("SEQ_T_ROLES");
        modelBuilder.HasSequence("SEQ_T_ROLES_PERMISOS");
        modelBuilder.HasSequence("SEQ_T_USUARIOS");
        modelBuilder.HasSequence("SEQ_T_USUARIOS_ROLES");

        OnModelCreatingPartial(modelBuilder);
    }

    private static LambdaExpression CreateFilterExpression(Type type)
    {
        var parameter = Expression.Parameter(type, "e");
        var body = Expression.Equal(Expression.PropertyOrField(parameter, "FecBaja"), Expression.Constant(null, typeof(DateTime?)));
        return Expression.Lambda(body, parameter);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
