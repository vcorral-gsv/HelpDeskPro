namespace HelpDeskPro.Utils
{
    public class ResponseHeadersUtils
    {
        private static readonly System.Text.Json.JsonSerializerOptions _cachedJsonOptions = new()
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
        };

        public static void SetHeader_XPagination<T>(HttpResponse response, T paginationMetadata)
        {
            string metadataJson = System.Text.Json.JsonSerializer.Serialize(paginationMetadata, _cachedJsonOptions);
            response.Headers["X-Pagination"] = metadataJson;
        }
    }
}
