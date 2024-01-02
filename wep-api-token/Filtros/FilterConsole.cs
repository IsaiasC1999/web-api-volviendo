using Microsoft.AspNetCore.Mvc.Filters;

namespace wep_api_token.Filtros
{
    public class FilterConsole : IAsyncActionFilter
    {
        //Estos son filtros personalizados que se hablan desde el controlador. No son filtros globales 

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("Antes de llegar al end-point");
            await next();
            Console.WriteLine("Despues de la ejecucion del end-point");
        }
    }
}