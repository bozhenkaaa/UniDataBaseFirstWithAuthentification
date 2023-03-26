using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public partial class Train
{
    public int TrainId { get; set; }
    [Display(Name = "Місто відправлення")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public string TrainDeparture { get; set; }
    [Display(Name = "Місто прибуття")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public string TrainDestination { get; set; }
    [Display(Name = "Час відправлення")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public TimeSpan TrainTimeOfDep { get; set; }
    [Display(Name = "Час прибуття")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public TimeSpan TrainTimeOfStop { get; set; }
    [Display(Name = "Тип потяга")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public string TrainType { get; set; }
    [Display(Name = "Розклад №")]
    [Required(ErrorMessage = "Не може бути порожнє!")]
    public int ScheduleId { get; set; }

    public virtual ICollection<Carriage> Carriages { get; } = new List<Carriage>();

    public virtual Schedule Schedule { get; set; }

    public virtual ICollection<TrainSchedule> TrainSchedules { get; } = new List<TrainSchedule>();
}
