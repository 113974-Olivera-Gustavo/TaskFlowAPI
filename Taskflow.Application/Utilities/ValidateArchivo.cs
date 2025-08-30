namespace Taskflow.Application.Utilities
{
    public abstract class ValidateArchivo
    {
        public static bool ValidateFormatAttachment(string? adjunto)
        {
            if (string.IsNullOrEmpty(adjunto))
            {
                return true;
            }

            var extension = Path.GetExtension(adjunto);
            return extension == ".pdf" || extension == ".jpg" || extension == ".jpeg" || extension == ".png";
        }

        public static bool ValidateFileSize(string? base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                return true;
            }

            const int maxContentLength = 5242880;
            var data = Convert.FromBase64String(base64String);
            return data.Length <= maxContentLength;
        }
    }

}
