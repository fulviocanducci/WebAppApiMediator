using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppApi.Application.Commands.TodoCommand;
using WebAppApi.Application.Infra;
using WebAppApi.Application.Models;

namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TodoController : ControllerBase
    {
        public IMediator Mediator { get; }
        public IRepositoryTodo RepositoryTodo { get; }

        public TodoController(IMediator mediator, IRepositoryTodo repositoryTodo)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            RepositoryTodo = repositoryTodo ?? throw new ArgumentNullException(nameof(repositoryTodo));
        }

        [HttpGet]
        public IAsyncEnumerable<Todo> Get()
        {
            return RepositoryTodo.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> Get(int id)
        {
            var result = await RepositoryTodo.GetAsync(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> Post([FromBody] TodoAddCommand value)
        {
            return CreatedAtAction(nameof(Get), await Mediator.Send(value));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] TodoUpdateCommand value)
        {
            bool result = id == value.Id && await Mediator.Send(value);
            return result
                ? Ok(new { status = "updated" })
                : NotFound(new { status = "not found" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                bool result = await Mediator.Send(new TodoDeleteCommand(id));
                return result
                    ? Ok(new { status = "removed", code = 200 })
                    : NotFound(new { status = "not found" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
