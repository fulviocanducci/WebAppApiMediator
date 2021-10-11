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
    public class TodoAddCommandHandler : IRequestHandler<TodoAddCommand, Models.Todo>
    {
        public IRepositoryTodo RepositoryTodo { get; }
        public IMapper Mapper { get; }

        public TodoAddCommandHandler(IRepositoryTodo repositoryTodo, IMapper mapper)
        {
            RepositoryTodo = repositoryTodo ?? throw new ArgumentNullException(nameof(repositoryTodo));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Models.Todo> Handle(TodoAddCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Todo todo = Mapper.Map<Todo>(request);
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
