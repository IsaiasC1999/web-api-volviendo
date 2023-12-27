using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wep_api_token.models;
using wep_api_token.models.DTOs;
using wep_api_token.repository;

namespace wep_api_token.services
{
    public class UserServices
    {
        private readonly UserRepository repoUser;

         

        public UserServices(UserRepository repoUser)
        {
            this.repoUser = repoUser;
        }


        public async Task<Response> Login(UserDto user)
        {
            
            //aqui podria pasar la contraseña a un hash
            if ( await repoUser.ExistUser(user))
            {
                return new Response
                {
                    Success= true,
                    Message= "autentificacion exitosa",
                    
                };

            }
            return new Response
            {
                Success = false,
                Message = "usuario o contraseña incorrecta",
                data = null
            };
        }


    }
}
