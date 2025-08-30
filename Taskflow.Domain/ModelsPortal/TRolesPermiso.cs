namespace Taskflow.Domain.ModelsPortal;

public partial class TRolesPermiso
{
    public decimal IdRolPermiso { get; set; }

    public decimal IdRol { get; set; }

    public decimal IdPermiso { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? FecIng { get; set; }

    public DateTime? FecMod { get; set; }

    public DateTime? FecBaja { get; set; }

    public string? UsrIng { get; set; }

    public string? UsrMod { get; set; }

    public string? UsrBaja { get; set; }

    public virtual TPermiso IdPermisoNavigation { get; set; } = null!;

    public virtual TRole IdRolNavigation { get; set; } = null!;
}
