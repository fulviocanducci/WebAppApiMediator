using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAppApi.Application.Commands.Todo;
using WebAppApi.Application.Infra;

namespace WebAppApi.Application.Handlers.Todo
{
    public class TodoDeleteCommandHandler : IRequestHandler<TodoDeleteCommand, bool>
    {
        public IRepositoryTodo RepositoryTodo { get; }

        public TodoDeleteCommandHandler(IRepositoryTodo repositoryTodo)
        {
            RepositoryTodo = repositoryTodo ?? throw new ArgumentNullException(nameof(repositoryTodo));
        }

        public async Task<bool> Handle(TodoDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = RepositoryTodo.GetAsync(request.Id);
                if (model is not null)
                {
                    await RepositoryTodo.RemoveAsync(model);
                    return await RepositoryTodo.CommitAsync(cancellationToken) > 0;
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
