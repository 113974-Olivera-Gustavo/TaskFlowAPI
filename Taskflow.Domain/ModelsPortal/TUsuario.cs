namespace Taskflow.Domain.ModelsPortal;

public partial class TUsuario
{
    public decimal IdUsuario { get; set; }

    public decimal IdPersona { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Estado { get; set; }

    public string? DebeCambiarPassword { get; set; }

    public DateTime? FechaUltimoCambioPass { get; set; }

    public DateTime? FechaUltimaConexion { get; set; }

    public byte? IntentosFallidos { get; set; }

    public DateTime? FechaBloqueo { get; set; }

    public string? EmailVerificado { get; set; }

    public DateTime? FecIng { get; set; }

    public DateTime? FecMod { get; set; }

    public DateTime? FecBaja { get; set; }

    public string? UsrIng { get; set; }

    public string? UsrMod { get; set; }

    public string? UsrBaja { get; set; }

    public virtual TPersona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<TAuditLog> TAuditLogs { get; set; } = new List<TAuditLog>();

    public virtual ICollection<TRecoveryToken> TRecoveryTokens { get; set; } = new List<TRecoveryToken>();

    public virtual ICollection<TUsuariosRole> TUsuariosRoles { get; set; } = new List<TUsuariosRole>();
}
