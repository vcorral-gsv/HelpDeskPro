namespace HelpDeskPro.Dtos
{
    /// <summary>
    /// Estructura estándar para errores de API.
    /// </summary>
    public class ApiErrorResponse
    {
        /// <summary>Descripción legible del error.</summary>
        public string? Descripcion { get; set; }
        /// <summary>Ruta del endpoint.</summary>
        public string? Path { get; set; }
        /// <summary>Método HTTP.</summary>
        public string? Method { get; set; }
        /// <summary>Excepción serializada si aplica.</summary>
        public string? ExceptionSerialized { get; set; }
        /// <summary>Código de estado HTTP.</summary>
        public int? StatusCode { get; set; }
        /// <summary>Identificador de traza.</summary>
        public string? TraceId { get; set; }
        /// <summary>Listado de códigos de error.</summary>
        public string[]? ErrorCodes { get; set; }
    }
}
