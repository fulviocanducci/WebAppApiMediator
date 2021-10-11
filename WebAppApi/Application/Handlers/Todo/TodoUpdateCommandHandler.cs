using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAppApi.Application.Commands.Todo;
using WebAppApi.DataAccess;

namespace WebAppApi.Application.Handlers.Todo
{
    public class TodoUpdateCommandHandler : IRequestHandler<TodoUpdateCommand, bool>
    {
        private readonly DatabaseContext _databaseContext;

        public TodoUpdateCommandHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public async Task<bool> Handle(TodoUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var todo = new Models.Todo()
                {
                    Id = request.Id,
                    Description = request.Description,
                    Done = request.Done
                };
                _databaseContext.Todo.Update(todo);
                await _databaseContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
