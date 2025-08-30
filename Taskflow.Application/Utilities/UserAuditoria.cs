namespace Taskflow.Application.Utilities
{
    public abstract class UserAuditoria
    {
        private static PortalContext _dbContext;

        public static void Initialize(PortalContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Obtiene el nombre completo del usuario (Apellido, Nombre) a partir de su identificador.
        /// Si el parámetro no es un ID numérico válido, retorna el valor original.
        /// Si el usuario no existe en la base de datos, retorna una cadena vacía.
        /// </summary>
        /// <param name="user">Identificador del usuario o nombre de usuario.</param>
        /// <returns>Nombre completo del usuario, el valor original si no es un ID, o cadena vacía si no existe.</returns>
        public static string GetUserAuditoria(string user)
        {
            if (!int.TryParse(user, out var idUsuario))
            {
                return user;
            }

            var usuario = _dbContext.TUsuarios
                .Include(x => x.IdPersonaNavigation)
                .FirstOrDefault(x => x.IdUsuario == idUsuario);

            if (usuario is null)
            {
                return string.Empty;
            }

            return usuario.IdPersonaNavigation.Apellido + ", " + usuario.IdPersonaNavigation.Nombre;
        }
    }
}
