using Application.Common.Constants.Messages;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitecture.Application.TodoLists.Commands.DeleteTodoList;
using CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts;

namespace Presentation.Controllers
{
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet("{userId}/is-in-role/{role}")]
        public async Task<IActionResult> IsInRole(string userId, string role)
        {
            var isInRole = await _identityService.IsInRoleAsync(userId, role);

            return Ok(new ApiResult<bool> { Data = isInRole });
        }

        [HttpPost("{userId}/authorize")]
        public async Task<IActionResult> AuthorizeUser(string userId, [FromBody] AuthorizeRequest request)
        {
            var isAuthorized = await _identityService.AuthorizeAsync(userId, request.PolicyName);

            return Ok(new ApiResult<bool> { Data = isAuthorized });
        }

        [HttpGet("{userId}/username")]
        public async Task<ActionResult<ApiResult<string>>> Get(string userId)
        {
            var userName = await _identityService.GetUserNameAsync(userId);
            if (userName == null)
                return NotFound(ApiMessages.NotAvaible);

            return Ok(new ApiResult<string> { Data = userName });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> CreateUser([FromBody] CreateUserRequest request)
        {
            var result = await _identityService.CreateUserAsync(request.UserName, request.Password);
            if (!result.Result.Succeeded)
                return BadRequest(ApiMessages.SaveOpIsNotSuccess);

            return Ok(new ApiResult<string> { Message = ApiMessages.SaveOpIsSuccess, Data = result.UserId });
        }

    }
}

public class CreateUserRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AuthorizeRequest
{
    public string PolicyName { get; set; } = string.Empty;
}