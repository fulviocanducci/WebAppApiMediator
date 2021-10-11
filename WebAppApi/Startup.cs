using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebAppApi.Application.Commands.TodoCommand;
using WebAppApi.Application.Infra;
using WebAppApi.Application.Models;
using WebAppApi.DataAccess;

namespace WebAppApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.CreateMap<Todo, TodoAddCommand>().ReverseMap();
                config.CreateMap<Todo, TodoUpdateCommand>().ReverseMap();
            });

            services.Configure<RouteOptions>(c =>
            {
                c.LowercaseQueryStrings = true;
                c.LowercaseUrls = true;
            });

            services.AddMediatR(typeof(Startup));

            services.AddDbContext<DatabaseContext>(config =>
            {
                config.UseSqlite("Data Source = ./db.sqlite");
            });

            services.AddScoped<IRepositoryTodo, RepositoryTodo>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAppApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAppApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
