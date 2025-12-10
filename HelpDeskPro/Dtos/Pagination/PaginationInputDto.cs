namespace HelpDeskPro.Dtos.Pagination
{
    /// <summary>
    /// Parámetros de entrada para paginación.
    /// </summary>
    public class PaginationInputDto
    {
        /// <summary>Página solicitada (1-based).</summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>Tamaño de página.</summary>
        public int PageSize { get; set; } = 20;
    }
}
