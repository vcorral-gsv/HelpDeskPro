using HelpDeskPro.Dtos.Pagination;

namespace HelpDeskPro.Dtos
{
    /// <summary>
    /// Entrada base para filtros con paginación.
    /// </summary>
    public class FiltersInput : PaginationInputDto
    {
        /// <summary>Si es true, los filtros se aplican en coincidencia exacta.</summary>
        public bool ExactMatch { get; set; }
    }
}
