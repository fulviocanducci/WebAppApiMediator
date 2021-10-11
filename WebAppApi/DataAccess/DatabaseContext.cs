using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using WebAppApi.Application.Models;
using WebAppApi.Application.Models.Mapping;

namespace WebAppApi.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext([NotNull] DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
