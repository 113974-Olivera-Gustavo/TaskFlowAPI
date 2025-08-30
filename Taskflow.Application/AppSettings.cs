namespace Taskflow.Application
{
    public abstract class AppSettings
    {
        public class Sequences
        {
            public static string SeqIdTipoDocumento { get; set; }
            public static string SeqTAuditLog { get; set; }
            public static string SeqTModulo { get; set; }
            public static string SeqTPermisos { get; set; }
            public static string SeqTPersonas { get; set; }
            public static string SeqTRoles { get; set; }
            public static string TRecoveryTokens { get; set; }
            public static string TRolesPermisos { get; set; }
            public static string TUsuarios { get; set; }
            public static string TUsuariosRoles { get; set; }
        }

        public class DataBase
        {
            public static string? Portal { get; set; }
        }

        public class Jwt
        {
            public static string SecretKey { get; set; } = " ";
            public static string? Issuer { get; set; }
            public static string? Audience { get; set; }
            public static int Minutes { get; set; }
        }

        public class Smtp
        {
            public static string Host { get; set; }

            public static string Name { get; set; }

            public static string Password { get; set; }

            public static int Port { get; set; }

            public static string UserName { get; set; }

            public static string Address { get; set; }

            public static int Authentication { get; set; }
        }

        public class ReportesEmail
        {
            public static List<Reporting> LstReporting { get; set; } = new List<Reporting>();
        }

        public class Reporting
        {
            public string Url { get; set; }

            public string Title { get; set; }
        }

        public class SendMail
        {
            public static string ToAddress { get; set; }

            public static string CcAddress { get; set; }
        }

    }
}
