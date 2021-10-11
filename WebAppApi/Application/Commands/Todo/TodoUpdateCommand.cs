using MediatR;

namespace WebAppApi.Application.Commands.Todo
{
    public class TodoUpdateCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}
