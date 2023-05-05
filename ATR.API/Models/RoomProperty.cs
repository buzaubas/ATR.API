using System;
using System.Collections.Generic;

namespace ATR.API.Models;

public partial class RoomProperty
{
    public int RoomPropertyId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Room> RoomsRooms { get; set; } = new List<Room>();
}
