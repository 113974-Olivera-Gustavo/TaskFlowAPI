namespace Taskflow.Application.Sequences
{
    public abstract class Sequence
    {
        private static IConfiguration _configuration;

        private static string PortalConnection => _configuration.GetConnectionString("PORTAL");

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static int ID_TIPO_DOCUMENTO()
        {
            return NextSequenceValue(AppSettings.Sequences.SeqIdTipoDocumento, PortalConnection);
        }

        public static int GetSequenceForType<TEntity>()
        {
            return typeof(TEntity) switch
            {
                _ when typeof(TEntity) == typeof(TTipoDocumento) => ID_TIPO_DOCUMENTO(),
                _ => throw new ArgumentException($"No sequence defined for type {typeof(TEntity).Name}")
            };
        }

        private static int NextSequenceValue(string sequenceName, string connectionString)
        {
            var querySeq = $"SELECT {sequenceName}.NEXTVAL FROM DUAL";
            var conn = new OracleConnection(connectionString);
            conn.Open();
            var command = new OracleCommand(querySeq, conn);
            var resultado = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();
            return resultado;
        }
    }
}
