using Microsoft.AspNetCore.Mvc.Filters;

namespace wep_api_token.Filtros
{
    public class FiltroResultado : IResultFilter
    {

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Despues filtro de resultado");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Antes filtro de resultado");
        }
    }
}
