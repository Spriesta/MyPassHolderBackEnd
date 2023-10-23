using System;
using System.Collections.Generic;

namespace MyPassHolder.Models;

public partial class MyPassword
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long CategoryId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
