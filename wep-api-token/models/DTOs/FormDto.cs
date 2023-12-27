using System.ComponentModel.DataAnnotations;

namespace wep_api_token.models.DTOs
{
    public class FormDto
    {
        
        public string Name { get; set; }

        public IFormFile Archivo { get; set; }


    }
}
