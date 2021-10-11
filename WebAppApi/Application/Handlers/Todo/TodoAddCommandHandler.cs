using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAppApi.Application.Commands.Todo;
using WebAppApi.DataAccess;

namespace WebAppApi.Application.Handlers.Todo
{
    public class TodoAddCommandHandler : IRequestHandler<TodoAddCommand, Models.Todo>
    {
        private readonly DatabaseContext _databaseContext;

        public TodoAddCommandHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public async Task<Models.Todo> Handle(TodoAddCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var todo = new Models.Todo()
                {
                    Description = request.Description,
                    Done = request.Done
                };
                _databaseContext.Todo.Add(todo);
                await _databaseContext.SaveChangesAsync(cancellationToken);
                return todo;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
