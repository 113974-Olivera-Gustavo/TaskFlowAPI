namespace Taskflow.Domain.ModelsPortal;

public partial class TModulo
{
    public decimal IdModulo { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime? FecIng { get; set; }

    public DateTime? FecMod { get; set; }

    public DateTime? FecBaja { get; set; }

    public string? UsrIng { get; set; }

    public string? UsrMod { get; set; }

    public string? UsrBaja { get; set; }

    public virtual ICollection<TPermiso> TPermisos { get; set; } = new List<TPermiso>();
}
