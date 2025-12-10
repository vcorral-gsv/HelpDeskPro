using AutoMapper;
using HelpDeskPro.Criterias;
using HelpDeskPro.Criterias.Users;
using HelpDeskPro.Data.Repositories.UserRepository;
using HelpDeskPro.Dtos.Pagination;
using HelpDeskPro.Dtos.User;
using HelpDeskPro.Entities;

namespace HelpDeskPro.Services.UserService
{
    public class UserService(
        IUserRepository _userRepository,
        ILogger<UserService> _logger,
        IMapper _mapper,
        IPasswordService _passwordService
    ) : IUserService
    {
        public async Task<GenericPaginationOutputDto<ListUserDto>> GetUsersAsync(GetUsersFiltersInputDto request)
        {
            var critList = new List<CriteriaBase<User>>()
            {
                new GetUsersCriteria(request)
            };

            var page = await _userRepository.GetUsersAsync(
                request.Page,
                request.PageSize,
                critList
            );

            var userDtos = _mapper.Map<List<ListUserDto>>(page.Items);

            var paginationMetadata = new PaginationOutputDto
            {
                CurrentPage = request.Page,
                PageSize = request.PageSize,
                PageItems = userDtos.Count,
                TotalItems = page.TotalCount,
                TotalPages = (int)Math.Ceiling((double)page.TotalCount / request.PageSize)
            };
            return new GenericPaginationOutputDto<ListUserDto>(
                userDtos,
                paginationMetadata
            );
        }

        public async Task<DetailUserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            var userDto = _mapper.Map<DetailUserDto>(user);
            return userDto;
        }

        public async Task<DetailUserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            var userDto = _mapper.Map<DetailUserDto>(user);
            return userDto;
        }

        public async Task<DetailUserDto> CreateUserAsync(AddUserDto request)
        {
            var user = _mapper.Map<User>(request);
            user.PasswordHash = _passwordService.HashPassword(request.Password);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            var userDto = _mapper.Map<DetailUserDto>(user);
            return userDto;
        }

        public async Task<GenericPaginationOutputDto<ListRolesWithUsersDto>> GetUsersGroupedByRoleAsync(PaginationInputDto request)
        {
            var page = await _userRepository.GetUsersGroupedByRoleAsync(request.Page, request.PageSize);

            var dtoGroups = page.Items
                .Select(g => new ListRolesWithUsersDto
                {
                    RoleName = g.RoleName,
                    Users = _mapper.Map<List<ListUserDto>>(g.Users)
                })
                .ToList();

            var paginationMetadata = new PaginationOutputDto
            {
                CurrentPage = request.Page,
                PageSize = request.PageSize,
                PageItems = dtoGroups.Count,
                TotalItems = page.TotalCount,
                TotalPages = (int)Math.Ceiling((double)page.TotalCount / request.PageSize)
            };
            return new GenericPaginationOutputDto<ListRolesWithUsersDto>(
                dtoGroups,
                paginationMetadata
            );
        }
    }
}
