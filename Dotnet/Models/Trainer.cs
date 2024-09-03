using System;
using System.Collections.Generic;

namespace FitnessFinal.Models;

public partial class Trainer
{
    public int TrainerId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Specialization { get; set; }

    public string? City { get; set; }

    public virtual ICollection<TrainerEnquiry> TrainerEnquiries { get; set; } = new List<TrainerEnquiry>();
}
