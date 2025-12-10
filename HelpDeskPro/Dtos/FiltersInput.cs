using HelpDeskPro.Dtos.Pagination;

namespace HelpDeskPro.Dtos
{
    public class FiltersInput : PaginationInputDto
    {
        public bool ExactMatch { get; set; }
    }
}
