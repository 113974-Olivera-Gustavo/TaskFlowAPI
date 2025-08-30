namespace Taskflow.Domain.ModelsPortal;

public partial class TPersona
{
    public decimal IdPersona { get; set; }

    public decimal IdTipoDocumento { get; set; }

    public string NroDocumento { get; set; } = null!;

    public string? Cuil { get; set; }

    public string Apellido { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public DateTime? FecIng { get; set; }

    public DateTime? FecMod { get; set; }

    public DateTime? FecBaja { get; set; }

    public string? UsrIng { get; set; }

    public string? UsrMod { get; set; }

    public string? UsrBaja { get; set; }

    public virtual TTipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;

    public virtual ICollection<TUsuario> TUsuarios { get; set; } = new List<TUsuario>();
}
