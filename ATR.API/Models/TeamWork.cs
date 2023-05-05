using System;
using System.Collections.Generic;

namespace ATR.API.Models;

public partial class TeamWork
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string AboutWork { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateUser { get; set; } = null!;

    public string LinkedIn { get; set; } = null!;

    public string Instagram { get; set; } = null!;

    public string FaceBook { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string JobPositionId { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public int JobPositionNameId { get; set; }

    public virtual JobPosition JobPositionName { get; set; } = null!;
}
