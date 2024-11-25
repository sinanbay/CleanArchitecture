using Application.Common.Constants.Messages;
using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitecture.Application.TodoLists.Commands.DeleteTodoList;
using CleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Presentation.Contracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Presentation.Controllers
{
    public class TodosController : BaseController
    {
        readonly IMediator mediator;

        public TodosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<TodosVm>>> Get([FromQuery] GetTodosQuery request)
        {
            TodosVm response = await mediator.Send(request);
            if (response == null)
                return NotFound(ApiMessages.NotAvaible);

            return Ok(new ApiResult<TodosVm> { Data = response });
        }

        //[AuthorizeRoles(Roles.SystemAdmin)]
        [HttpPost]
        public async Task<ActionResult<ApiResult>> Post([FromBody] CreateTodoListCommand command)
        {
            var result = await mediator.Send(command);
            if (result == 0)
                return BadRequest(ApiMessages.SaveOpIsNotSuccess);

            return Ok(new ApiResult<int> { Message = ApiMessages.SaveOpIsSuccess, Data = result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult>> Put(int id, [FromBody] UpdateTodoListCommand command)
        {
            if (id != command.Id)
                return BadRequest(ApiMessages.InvalidData);
            await mediator.Send(command);
            return Ok(new ApiResult { Message = ApiMessages.UpdateOpIsSuccess });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            var command = new DeleteTodoListCommand(id);
            await mediator.Send(command);
            return Ok(new ApiResult { Message = ApiMessages.DeleteOpIsSuccess });
        }
    }
}

