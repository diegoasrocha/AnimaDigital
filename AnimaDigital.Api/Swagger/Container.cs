using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models; 
using System; 
using System.IO; 
 

namespace AnimaDigital.Api.Swagger
{
    public static class Container
    {
        public static void AddSwagger(this IServiceCollection services, string xmlFile)
        {
            OpenApiInfo informacoesApi = GetInfoApiSwagger();
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddSwaggerGen(s =>
            { 
                s.SwaggerDoc(informacoesApi.Version, informacoesApi); 

                if (!string.IsNullOrEmpty(xmlFile))
                { 
                    s.IncludeXmlComments(xmlPath);
                }
            }); 
        }

        public static void UseSwaggerApp(this IApplicationBuilder app)
        {
            OpenApiInfo informacoesApi = GetInfoApiSwagger();

            var endpointSwagger = $"/swagger/{informacoesApi.Version}/swagger.json";

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                var nome = $"{informacoesApi.Version}";
                s.SwaggerEndpoint(endpointSwagger, nome);
                s.RoutePrefix = "swagger";
            });
        }

        private static OpenApiInfo GetInfoApiSwagger()
        { 
            var info = new OpenApiInfo()
            {
                Title = "Anima Digital (School)",
                Version = "v1.0.0",
                Description = "API Anima Digital para cadastro, consultas e matriculas",
                TermsOfService = new Uri("https://animaeducacao.com.br/"),
                Contact = new OpenApiContact
                {
                    Email = "contato@animaeducacao.com.br",
                    Name = "Setor de TI."
                }
            };

            return info;
        } 
    }
}
