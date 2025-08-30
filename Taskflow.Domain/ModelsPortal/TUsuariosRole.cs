namespace Taskflow.Domain.ModelsPortal;

public partial class TUsuariosRole
{
    public decimal IdUsuarioRoles { get; set; }

    public decimal IdUsuario { get; set; }

    public decimal IdRol { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public string? Activo { get; set; }

    public string? AsignadoPor { get; set; }

    public DateTime? FecIng { get; set; }

    public DateTime? FecMod { get; set; }

    public DateTime? FecBaja { get; set; }

    public string? UsrIng { get; set; }

    public string? UsrMod { get; set; }

    public string? UsrBaja { get; set; }

    public virtual TRole IdRolNavigation { get; set; } = null!;

    public virtual TUsuario IdUsuarioNavigation { get; set; } = null!;
}
