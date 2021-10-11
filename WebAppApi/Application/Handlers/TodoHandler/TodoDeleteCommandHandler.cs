using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAppApi.Application.Commands.TodoCommand;
using WebAppApi.Application.Infra;
using WebAppApi.Application.Models;

namespace WebAppApi.Application.Handlers.TodoHandler
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
                Todo model = await RepositoryTodo.GetAsync(request.Id);
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