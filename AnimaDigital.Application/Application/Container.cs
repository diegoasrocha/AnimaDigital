using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection; 

namespace AnimaDigital.Application.Application
{
    public static class Container
    {
        public static void AddApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var result = new Response();

                    foreach (var propriedade in context.ModelState.Keys)
                    {
                        var errosPropriedade = context.ModelState[propriedade];

                        foreach (var erroPropriedadeItem in errosPropriedade.Errors)
                        {
                            result.AddErro(erroPropriedadeItem.ErrorMessage);
                        }
                    }

                    return new UnprocessableEntityObjectResult(result);
                };
            });
        }
    }
}
