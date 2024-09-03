using System;
using System.Collections.Generic;

namespace FitnessFinal.Models;

public partial class Enquiry
{
    public int Enqid { get; set; }

    public int UserId { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Email { get; set; }

    public string? Fee { get; set; }

    public string? Gympackage { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<TrainerEnquiry> TrainerEnquiries { get; set; } = new List<TrainerEnquiry>();

    public virtual Member? User { get; set; } = null!;
}
