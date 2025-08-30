namespace Taskflow.Domain.ModelsPortal;

public partial class TRecoveryToken
{
    public decimal IdToken { get; set; }

    public decimal IdUsuario { get; set; }

    public string TokenHash { get; set; } = null!;

    public string? CodigoVerificacion { get; set; }

    public string TipoRecovery { get; set; } = null!;

    public DateTime FechaExpiracion { get; set; }

    public string? Usado { get; set; }

    public DateTime? FecIng { get; set; }

    public DateTime? FecMod { get; set; }

    public DateTime? FecBaja { get; set; }

    public string? UsrIng { get; set; }

    public string? UsrMod { get; set; }

    public string? UsrBaja { get; set; }

    public virtual TUsuario IdUsuarioNavigation { get; set; } = null!;
}
