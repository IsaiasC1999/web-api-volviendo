using System.ComponentModel.DataAnnotations.Schema;
using wep_api_token.ContextModel;

namespace wep_api_token.models.DTOs
{
    public class BlogCreateDto
    {       
        
        public string Titulo { get; set; }

        public string Autor { get; set; }

        public string SubTitulo { get; set; }

    }
}
