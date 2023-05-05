using System;
using System.Collections.Generic;

namespace ATR.API.Models;

public partial class JobPosition
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public virtual ICollection<TeamWork> TeamWorks { get; set; } = new List<TeamWork>();
}
