using MediatR;

namespace WebAppApi.Application.Commands.TodoCommand
{
    public class TodoDeleteCommand: IRequest<bool>
    {
        public TodoDeleteCommand(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }
}
