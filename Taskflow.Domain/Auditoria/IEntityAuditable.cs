namespace Taskflow.Domain.Auditoria
{
    public interface IEntityAuditable
    {
        public int Id { get; set; }
        public string? UserIng { get; set; }

        public DateTime? FecIng { get; set; }

        public string? UserMod { get; set; }

        public DateTime? FecMod { get; set; }

        public string? UserBaja { get; set; }

        public DateTime? FecBaja { get; set; }
    }
}
