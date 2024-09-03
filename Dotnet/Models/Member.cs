using System;
using System.Collections.Generic;

namespace FitnessFinal.Models;

public partial class Member
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string? Name { get; set; }

    public string? City { get; set; }

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }

    public bool IsAdmin { get; set; }

    public string? SecurityQues { get; set; }

    public string? SecurityAns { get; set; }

    public virtual ICollection<Enquiry> Enquiries { get; set; } = new List<Enquiry>();

    public virtual ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
}
