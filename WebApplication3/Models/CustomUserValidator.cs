using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication3.Models;

namespace WebApplication3.Models
{
    public class CustomUserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (user.Email.ToLower().EndsWith("@spam.com"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Даний домен в спам-базі. Введіть іншу пошту"
                });
            }
            
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}