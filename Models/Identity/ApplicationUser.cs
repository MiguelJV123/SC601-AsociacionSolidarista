using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace proyecto.asociacionsolidarista.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public bool Activo { get; set; } = true;

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var identity =
                await manager.CreateIdentityAsync(
                    this,
                    DefaultAuthenticationTypes.ApplicationCookie);

            identity.AddClaim(new Claim("NombreCompleto", $"{Nombre} {Apellidos}"));

            return identity;
        }
    }
}