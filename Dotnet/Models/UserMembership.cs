using System;
using System.Collections.Generic;

namespace FitnessFinal.Models;

public partial class UserMembership
{
    public int UserMembershipId { get; set; }

    public int UserId { get; set; }

    public int MembershipId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Membershiptype Membership { get; set; } = null!;

    public virtual Member User { get; set; } = null!;
}
