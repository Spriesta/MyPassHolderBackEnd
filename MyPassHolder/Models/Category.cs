using System;
using System.Collections.Generic;

namespace MyPassHolder.Models;

public partial class Category
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long UserId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<MyPassword> MyPasswords { get; set; } = new List<MyPassword>();

    public virtual User User { get; set; } = null!;
}
