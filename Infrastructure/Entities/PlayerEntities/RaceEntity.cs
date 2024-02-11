using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class RaceEntity
{
    public int Id { get; set; }

    public string RaceName { get; set; } = null!;

    public int Hp { get; set; }

    public virtual ICollection<PlayerEntity> Players { get; set; } = new List<PlayerEntity>();
}
