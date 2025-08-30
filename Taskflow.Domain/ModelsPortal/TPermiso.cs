namespace Taskflow.Domain.ModelsPortal;
public partial class TPermiso
{
    public decimal IdPermiso { get; set; }

    public string NombrePermiso { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal IdModulo { get; set; }

    public string? Activo { get; set; }

    public DateTime? FecIng { get; set; }

    public DateTime? FecMod { get; set; }

    public DateTime? FecBaja { get; set; }

    public string? UsrIng { get; set; }

    public string? UsrMod { get; set; }

    public string? UsrBaja { get; set; }

    public virtual TModulo IdModuloNavigation { get; set; } = null!;

    public virtual ICollection<TRolesPermiso> TRolesPermisos { get; set; } = new List<TRolesPermiso>();
}
