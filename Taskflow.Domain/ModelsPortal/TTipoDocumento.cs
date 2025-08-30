namespace Taskflow.Domain.ModelsPortal;

public partial class TTipoDocumento
{
    public decimal IdTipoDocumento { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FecIng { get; set; }

    public DateTime? FecBaja { get; set; }

    public DateTime? FecMod { get; set; }

    public string? UsrIng { get; set; }

    public string? UsrBaja { get; set; }

    public string? UsrMod { get; set; }

    public virtual ICollection<TPersona> TPersonas { get; set; } = new List<TPersona>();
}
