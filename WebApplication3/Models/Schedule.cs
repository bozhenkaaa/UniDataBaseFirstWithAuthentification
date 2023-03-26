using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }
    [Display(Name = "Станція №")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public int StationNumber { get; set; }
    [Display(Name = "Назва станції")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public string StationName { get; set; }

    public virtual ICollection<Train> Trains { get; } = new List<Train>();
}
