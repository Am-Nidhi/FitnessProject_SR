using System;
using System.Collections.Generic;

namespace FitnessFinal.Models;

public partial class Membershiptype
{
    public int MembershipId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Fee { get; set; }

    public virtual ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
}
