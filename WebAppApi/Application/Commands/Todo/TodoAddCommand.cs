using MediatR;

namespace WebAppApi.Application.Commands.Todo
{
    public class TodoAddCommand : IRequest<Models.Todo>
    {
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}
