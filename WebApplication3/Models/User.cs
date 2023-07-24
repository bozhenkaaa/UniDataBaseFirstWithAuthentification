using Microsoft.AspNetCore.Identity;
namespace WebApplication3.Models
{
    public class User: IdentityUser
    {
        public int Year { get; set; }
    }
}
