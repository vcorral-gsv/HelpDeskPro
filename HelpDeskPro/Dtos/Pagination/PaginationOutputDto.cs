namespace HelpDeskPro.Dtos.Pagination
{
    public class PaginationOutputDto
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageItems { get; set; }
    }
}
