namespace HelpDeskPro.Dtos.Pagination
{
    public class GenericPaginationOutputDto<TEntity>(List<TEntity> items, PaginationOutputDto paginationMetadata) where TEntity : class
    {
        public List<TEntity> Items { get; set; } = items;
        public PaginationOutputDto PaginationMetadata { get; set; } = paginationMetadata;
    }
}
