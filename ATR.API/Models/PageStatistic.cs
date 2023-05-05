using System;
using System.Collections.Generic;

namespace ATR.API.Models;

public partial class PageStatistic
{
    public int PageStatisticsId { get; set; }

    public string? Path { get; set; }

    public string? PathBase { get; set; }

    public DateTime? CreateDate { get; set; }
}
