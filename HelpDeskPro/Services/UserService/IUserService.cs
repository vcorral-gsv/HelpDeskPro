using HelpDeskPro.Criterias.Users;
using HelpDeskPro.Dtos.Pagination;
using HelpDeskPro.Dtos.User;

namespace HelpDeskPro.Services.UserService
{
    public interface IUserService
    {
        Task<GenericPaginationOutputDto<ListUserDto>> GetUsersAsync(
            GetUsersFiltersInputDto request
            );
        Task<DetailUserDto?> GetUserByIdAsync(int id);
        Task<DetailUserDto?> GetUserByEmailAsync(string email);
        Task<DetailUserDto> CreateUserAsync(AddUserDto request);
        Task<GenericPaginationOutputDto<ListRolesWithUsersDto>> GetUsersGroupedByRoleAsync(PaginationInputDto request);
        Task<GenericPaginationOutputDto<ListUsersGroupedByTeamDto>> GetUsersGroupedByTeamAsync(PaginationInputDto request);


    }
}
