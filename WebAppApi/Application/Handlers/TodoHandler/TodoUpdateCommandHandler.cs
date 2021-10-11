using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAppApi.Application.Commands.TodoCommand;
using WebAppApi.Application.Infra;
using WebAppApi.Application.Models;

namespace WebAppApi.Application.Handlers.TodoHandler
{
    public class TodoUpdateCommandHandler : IRequestHandler<TodoUpdateCommand, bool>
    {
        public IRepositoryTodo RepositoryTodo { get; }
        public IMapper Mapper { get; }

        public TodoUpdateCommandHandler(IRepositoryTodo repositoryTodo, IMapper mapper)
        {
            RepositoryTodo = repositoryTodo ?? throw new ArgumentNullException(nameof(repositoryTodo));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(TodoUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var todo = Mapper.Map<Todo>(request);
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
