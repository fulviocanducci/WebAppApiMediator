using MediatR;

namespace WebAppApi.Application.Commands.Todo
{
    public class TodoDeleteCommand: IRequest<bool>
    {
        public int Id { get; set; }
    }
}
