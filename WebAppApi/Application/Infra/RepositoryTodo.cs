using WebAppApi.Application.Infra.Base;
using WebAppApi.Application.Models;
using WebAppApi.DataAccess;

namespace WebAppApi.Application.Infra
{
    public class RepositoryTodo : Repository<Todo>, IRepositoryTodo
    {
        public RepositoryTodo(DatabaseContext context) : base(context)
        {
        }
    }
}
