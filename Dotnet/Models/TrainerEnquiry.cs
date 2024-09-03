using System;
using System.Collections.Generic;

namespace FitnessFinal.Models;

public partial class TrainerEnquiry
{
    public int TrainerEnquiryId { get; set; }

    public int TrainerId { get; set; }

    public int Enqid { get; set; }

    public DateOnly AssignedDate { get; set; }

    public virtual Enquiry Enq { get; set; } = null!;

    public virtual Trainer Trainer { get; set; } = null!;
}
