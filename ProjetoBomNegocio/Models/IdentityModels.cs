using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjetoBomNegocio.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string Nome { get; set; }
	 

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    
    public class DB_BomSuporteContext : IdentityDbContext<ApplicationUser>
    {
        public DB_BomSuporteContext()
            : base("DB_BomSuporteContext", throwIfV1Schema: false)
        {
        }

        public DbSet<Tab_Suporte> Suportes { get; set; }
        public DbSet<Tab_Contato> Contatos { get; set; }

        public static DB_BomSuporteContext Create()
        {
            return new DB_BomSuporteContext();
        }
    }
     
}