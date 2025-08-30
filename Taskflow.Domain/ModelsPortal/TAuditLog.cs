namespace Taskflow.Domain.ModelsPortal;

public partial class TAuditLog
{
    public decimal IdLog { get; set; }

    public decimal IdUsuario { get; set; }

    public string Accion { get; set; } = null!;

    public string TablaAfectada { get; set; } = null!;

    public decimal RegistroAfectado { get; set; }

    public DateTime? FecIng { get; set; }

    public DateTime? FecMod { get; set; }

    public DateTime? FecBaja { get; set; }

    public string? UsrIng { get; set; }

    public string? UsrMod { get; set; }

    public string? UsrBaja { get; set; }

    public virtual TUsuario IdUsuarioNavigation { get; set; } = null!;
}
