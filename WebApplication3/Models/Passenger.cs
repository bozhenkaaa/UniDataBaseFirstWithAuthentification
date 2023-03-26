using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public partial class Passenger
{
    
    public int PsId { get; set; }
    [Display(Name = "Прізвище")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public string PsSurname { get; set; }
    [Display(Name = "Ім'я")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public string PsName { get; set; }
    [Display(Name = "Номер телефону")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public string PsPhone { get; set; }
    [Display(Name = "Номер паспорта")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public string PsPassport { get; set; }
    [Display(Name = "Пошта")]
    
    public string PsEmail { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
