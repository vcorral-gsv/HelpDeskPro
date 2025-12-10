using HelpDeskPro.Dtos;

namespace HelpDeskPro.Criterias.Users
{
    public class GetUsersFiltersInputDto : FiltersInput
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        // Pertenece a alguno o todos de estos roles
        public List<string>? Roles { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAfterDate { get; set; }
        public DateTime? CreatedBeforeDate { get; set; }
        public DateTime? LastLoginAfterDate { get; set; }
        public DateTime? LastLoginBeforeDate { get; set; }

        public List<int>? TeamIds { get; set; }
        public List<string>? TeamNames { get; set; }
    }
}
