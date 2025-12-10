namespace HelpDeskPro.Dtos.Pagination
{
    /// <summary>
    /// Metadatos de salida de la paginación.
    /// </summary>
    public class PaginationOutputDto
    {
        /// <summary>Total de elementos.</summary>
        public int TotalItems { get; set; }
        /// <summary>Total de páginas.</summary>
        public int TotalPages { get; set; }
        /// <summary>Página actual.</summary>
        public int CurrentPage { get; set; }
        /// <summary>Tamaño de página.</summary>
        public int PageSize { get; set; }
        /// <summary>Elementos en la página actual.</summary>
        public int PageItems { get; set; }
    }
}
