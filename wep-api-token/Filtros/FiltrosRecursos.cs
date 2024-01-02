using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace wep_api_token.Filtros
{
    public class FiltrosRecursos : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Filtros de curso [before]");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Filtros de curso [after]");
        }
    }
}
