using HelpDeskPro.Consts;
using HelpDeskPro.Dtos.Auth;

namespace HelpDeskPro.Dtos.User
{
    /// <summary>
    /// Datos para crear usuario por un administrador.
    /// </summary>
    public class AddUserDto : RegisterUserRequestDto
    {
        /// <summary>Rol que se asignará al usuario.</summary>
        public Roles Role { get; set; }
        /// <summary>Indica si el usuario se crea activo.</summary>
        public bool IsActive { get; set;  } = true;
    }
}
