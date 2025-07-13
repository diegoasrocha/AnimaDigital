using AnimaDigital.Infrastructure.DBConfiguration;
using AnimaDigital.Infrastructure.Repositories;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using System; 

namespace AnimaDigital.Application
{
    public static class Container
    {
        private const string CorsApp = "CorsApp";

        public static void UseCorsApp(this IApplicationBuilder app)
            => app.UseCors(CorsApp);

        public static void AddAnimaApplication(this IServiceCollection services)
        {
            AddContext(services);
            AddRepositories(services);
            AddMediator(services);
            AddCorsApp(services);
        }

        private static void AddContext(IServiceCollection services)
            => services.AddDbContext<AnimaContext>(options => {
            options.UseInMemoryDatabase();
                //options.UseSqlServer(DatabaseConnection.ConnectionConfiguration.GetConnectionString("DefaultConnection"));
            });

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
        }

        private static void AddMediator(IServiceCollection services)
            => services.AddMediatR(AppDomain.CurrentDomain.Load("AnimaDigital.Application"));

        private static void AddCorsApp(IServiceCollection services)
        =>  services.AddCors(options => {
                options.AddPolicy(
                    CorsApp, 
                    builder => builder.AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowAnyOrigin()
                                      .AllowCredentials()
                                      .WithExposedHeaders("Content-Disposition")
                ); 
            }); 
    }
}
