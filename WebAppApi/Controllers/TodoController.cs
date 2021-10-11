using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppApi.Application.Commands.Todo;
using WebAppApi.Application.Models;
using WebAppApi.DataAccess;

namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public IMediator Mediator { get; }
        public DatabaseContext DatabaseContext { get; }

        public TodoController(IMediator mediator, DatabaseContext databaseContext)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            DatabaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        [HttpGet]
        public async Task<IEnumerable<Todo>> Get()
        {
            return await DatabaseContext.Todo.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> Get(int id)
        {
            var result = await DatabaseContext.Todo.FindAsync(id);
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
            bool result = await Mediator.Send(new TodoDeleteCommand { Id = id });
            return result 
                ? Ok(new { status = "removed", code = 200 }) 
                : NotFound(new { status = "not found" });
        }
    }
}
