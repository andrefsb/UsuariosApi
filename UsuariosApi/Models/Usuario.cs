using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Models
{
    public class Usuario:IdentityUser
    {
        public DateTime Birthday { get; set; }
        public Usuario() : base() { }
    }
}
