namespace HelpDeskPro.Dtos
{
    public class ApiErrorResponse
    {
        public string? Descripcion { get; set; }
        public string? Path { get; set; }
        public string? Method { get; set; }
        public string? ExceptionSerialized { get; set; }
        public int? StatusCode { get; set; }
        public string? TraceId { get; set; }
        public string[]? ErrorCodes { get; set; }
    }
}
