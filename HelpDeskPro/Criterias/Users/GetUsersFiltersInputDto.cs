using HelpDeskPro.Dtos;

namespace HelpDeskPro.Criterias.Users
{
    /// <summary>
    /// Filtros disponibles para búsqueda de usuarios con paginación.
    /// </summary>
    public class GetUsersFiltersInputDto : FiltersInput
    {
        /// <summary>Identificador del usuario.</summary>
        public int? Id { get; set; }
        /// <summary>Email del usuario.</summary>
        public string? Email { get; set; }
        /// <summary>Nombre del usuario.</summary>
        public string? FirstName { get; set; }
        /// <summary>Apellidos del usuario.</summary>
        public string? LastName { get; set; }

        // Pertenece a alguno o todos de estos roles
        /// <summary>Roles a incluir en el filtro.</summary>
        public List<string>? Roles { get; set; }
        /// <summary>Estado de actividad del usuario.</summary>
        public bool? IsActive { get; set; }
        /// <summary>Fecha de creación posterior a.</summary>
        public DateTime? CreatedAfterDate { get; set; }
        /// <summary>Fecha de creación anterior a.</summary>
        public DateTime? CreatedBeforeDate { get; set; }
        /// <summary>Último login posterior a.</summary>
        public DateTime? LastLoginAfterDate { get; set; }
        /// <summary>Último login anterior a.</summary>
        public DateTime? LastLoginBeforeDate { get; set; }

        /// <summary>Ids de equipos a filtrar.</summary>
        public List<int>? TeamIds { get; set; }
        /// <summary>Nombres de equipos a filtrar.</summary>
        public List<string>? TeamNames { get; set; }
    }
}
