using MediatR;
using System.ComponentModel.DataAnnotations;

namespace WebAppApi.Application.Commands.TodoCommand
{
    public class TodoUpdateCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        [MinLength(2, ErrorMessage = "Minimo 2 caracateres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public bool Done { get; set; }
    }
}
