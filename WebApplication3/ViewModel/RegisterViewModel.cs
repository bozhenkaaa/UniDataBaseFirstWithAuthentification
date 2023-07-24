using System.ComponentModel.DataAnnotations;
namespace WebApplication3.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Name")]
        
        public string Name { get; set; }

        [Required]
        [Display(Name = "Passport")]
        public string Passport { get; set; }
        [Required]
        [Display(Name = "Рік народження")]
        [Range(1920, 2023, ErrorMessage = "Неправильний рік народження") ]
        public int Year { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Поле {0} має бути мінімум {2} і максимум {1} символів.", MinimumLength = 5)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="Паролі не співпадають")]
        [Display(Name = "Підтвердження пароля")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
