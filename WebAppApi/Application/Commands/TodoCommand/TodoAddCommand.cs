using MediatR;
using System.ComponentModel.DataAnnotations;
using WebAppApi.Application.Models;

namespace WebAppApi.Application.Commands.TodoCommand
{
    public class TodoAddCommand : IRequest<Todo>
    {
        [Required(ErrorMessage = "Requerido")]
        [MinLength(2, ErrorMessage = "Minimo 2 caracateres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public bool Done { get; set; }
    }
}
