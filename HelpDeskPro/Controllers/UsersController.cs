using AutoMapper;
using HelpDeskPro.Criterias.Users;
using HelpDeskPro.Dtos.Pagination;
using HelpDeskPro.Dtos.User;
using HelpDeskPro.Services.UserService;
using HelpDeskPro.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskPro.Controllers
{
    [Authorize (Roles = "Admin")]
    public class UsersController(IMapper _mapper, ILogger<UsersController> _logger, IUserService _userService) : HelpDeskProBaseController<UsersController>(_logger, _mapper)
    {
        [HttpPost("get-all")]
        public async Task<ActionResult<GenericPaginationOutputDto<ListUserDto>>> GetUsers([FromBody] GetUsersFiltersInputDto request)
        {
            var result = await _userService.GetUsersAsync(request);

            ResponseHeadersUtils.SetHeader_XPagination(Response, result.PaginationMetadata);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailUserDto>> GetUserById(int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            return userDto == null ? NotFound() : Ok(userDto);
        }

        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<DetailUserDto>> GetUserByEmail(string email)
        {
            var userDto = await _userService.GetUserByEmailAsync(email);
            return userDto == null ? NotFound() : Ok(userDto);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateUser([FromBody] AddUserDto request)
        {
            var userDto = await _userService.CreateUserAsync(request);
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
        }

        [HttpPost("get-grouped-by-role")]
        public async Task<ActionResult<GenericPaginationOutputDto<ListRolesWithUsersDto>>> GetUsersGroupedByRole([FromBody] PaginationInputDto request)
        {
            var result = await _userService.GetUsersGroupedByRoleAsync(request);
            ResponseHeadersUtils.SetHeader_XPagination(Response, result.PaginationMetadata);
            return Ok(result);
        }
    }
}
