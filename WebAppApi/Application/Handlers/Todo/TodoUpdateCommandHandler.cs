using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAppApi.Application.Commands.Todo;
using WebAppApi.Application.Infra;

namespace WebAppApi.Application.Handlers.Todo
{
    public class TodoUpdateCommandHandler : IRequestHandler<TodoUpdateCommand, bool>
    {
        public IRepositoryTodo RepositoryTodo { get; }

        public TodoUpdateCommandHandler(IRepositoryTodo repositoryTodo)
        {
            RepositoryTodo = repositoryTodo ?? throw new ArgumentNullException(nameof(repositoryTodo));
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
                await RepositoryTodo.UpdateAsync(todo);
                return await RepositoryTodo.CommitAsync(cancellationToken) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
