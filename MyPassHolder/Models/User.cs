using System;
using System.Collections.Generic;

namespace MyPassHolder.Models;

public partial class User
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
