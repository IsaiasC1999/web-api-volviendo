using Microsoft.EntityFrameworkCore;
using wep_api_token.ContextModel;
using wep_api_token.models.DTOs;

namespace wep_api_token.repository
{
    public class UserRepository
    {
        private readonly UsuariosContext db;

        public UserRepository(UsuariosContext db)
        {
            this.db = db;
        }



        public async Task<bool> ExistUser(UserDto user)
        {
           
            var resu = await db.Usuarios.AnyAsync(usu => usu.NombreUsuario == user.UserName && usu.ContrasenaHash == user.Password);
            return resu;   
            
        }
    }
}
