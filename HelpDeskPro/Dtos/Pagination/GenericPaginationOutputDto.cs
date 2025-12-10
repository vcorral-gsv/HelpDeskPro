namespace HelpDeskPro.Dtos.Pagination
{
    /// <summary>
    /// Resultado paginado genérico.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad del listado</typeparam>
    public class GenericPaginationOutputDto<TEntity>(List<TEntity> items, PaginationOutputDto paginationMetadata) where TEntity : class
    {
        /// <summary>Elementos de la página actual.</summary>
        public List<TEntity> Items { get; set; } = items;
        /// <summary>Metadatos de paginación.</summary>
        public PaginationOutputDto PaginationMetadata { get; set; } = paginationMetadata;
    }
}
