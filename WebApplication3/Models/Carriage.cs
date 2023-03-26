using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public partial class Carriage
{
    public int CarriageId { get; set; }
    [Display(Name ="Вагон №")]
    [Required(ErrorMessage ="Не може бути порожнє!")]
    public int TrainId { get; set; }
    [Display(Name = "Тип вагону №")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public int CarriageType { get; set; }
    [Display(Name = "назва типу вагона")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public string CarriageName { get; set; }
    [Display(Name = "Кількість місць")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public int PlaceCount { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();

    public virtual Train Train { get; set; }
}
