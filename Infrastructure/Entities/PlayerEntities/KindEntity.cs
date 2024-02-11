using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class KindEntity
{
    public int Id { get; set; }

    public string KindName { get; set; } = null!;

    public virtual ICollection<PlayerEntity> Players { get; set; } = new List<PlayerEntity>();
}
