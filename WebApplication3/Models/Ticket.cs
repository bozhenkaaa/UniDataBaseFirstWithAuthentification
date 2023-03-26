using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public partial class Ticket
{
    public int TicketId { get; set; }
    [Display(Name = "Ціна квитка")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public decimal TicketPrice { get; set; }
    [Display(Name = "Пасажир №")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public int PsId { get; set; }
    [Display(Name = "Розклад потяга")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public int PId { get; set; }
    [Display(Name = "Вагон №")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public int CarriageId { get; set; }
    [Display(Name = "Дата та час купівлі")]
    
    public DateTime? DateOfBuying { get; set; }
    [Display(Name = "Місце №")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public int PlaceNumber { get; set; }
    [Display(Name = "Вагон")]
    public virtual Carriage Carriage { get; set; }
    [Display(Name = "Розклад Потяга")]
    public virtual TrainSchedule PIdNavigation { get; set; }
    [Display(Name = "Пасажир")]
    public virtual Passenger Ps { get; set; }
}
