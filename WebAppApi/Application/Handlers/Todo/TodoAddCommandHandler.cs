using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAppApi.Application.Commands.Todo;
using WebAppApi.Application.Infra;

namespace WebAppApi.Application.Handlers.Todo
{
    public class TodoAddCommandHandler : IRequestHandler<TodoAddCommand, Models.Todo>
    {
        public IRepositoryTodo RepositoryTodo { get; }

        public TodoAddCommandHandler(IRepositoryTodo repositoryTodo)
        {
            RepositoryTodo = repositoryTodo ?? throw new ArgumentNullException(nameof(repositoryTodo));
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
                await RepositoryTodo.AddAsync(todo);
                await RepositoryTodo.CommitAsync(cancellationToken);
                return todo;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
