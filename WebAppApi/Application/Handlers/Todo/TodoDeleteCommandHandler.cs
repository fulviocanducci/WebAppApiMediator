using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAppApi.Application.Commands.Todo;
using WebAppApi.DataAccess;

namespace WebAppApi.Application.Handlers.Todo
{
    public class TodoDeleteCommandHandler : IRequestHandler<TodoDeleteCommand, bool>
    {
        private readonly DatabaseContext _databaseContext;

        public TodoDeleteCommandHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public async Task<bool> Handle(TodoDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = _databaseContext.Todo.Find(request.Id);
                if (model is not null)
                {
                    _databaseContext.Todo.Remove(model);
                    return await _databaseContext.SaveChangesAsync(cancellationToken) > 0;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
