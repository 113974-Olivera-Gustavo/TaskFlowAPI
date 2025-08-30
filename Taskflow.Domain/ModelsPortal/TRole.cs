namespace Taskflow.Domain.ModelsPortal;

public partial class TRole
{
    public decimal IdRol { get; set; }

    public string NombreRol { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Activo { get; set; }

    public DateTime? FecIng { get; set; }

    public DateTime? FecMod { get; set; }

    public DateTime? FecBaja { get; set; }

    public string? UsrIng { get; set; }

    public string? UsrMod { get; set; }

    public string? UsrBaja { get; set; }

    public virtual ICollection<TRolesPermiso> TRolesPermisos { get; set; } = new List<TRolesPermiso>();

    public virtual ICollection<TUsuariosRole> TUsuariosRoles { get; set; } = new List<TUsuariosRole>();
}
