using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public partial class TrainSchedule
{
    public int PId { get; set; }
    [Display(Name = "Потяг №")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public int TrainId { get; set; }
    [Display(Name = "Дата потяга")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public DateTime TrainDate { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
    [Display(Name = "Потяг")]
    public virtual Train Train { get; set; }
}
