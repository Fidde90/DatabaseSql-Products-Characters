using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class WeaponEntity
{
    public int Id { get; set; }

    public string WeaponName { get; set; } = null!;

    public int Power { get; set; }

    public virtual ICollection<PlayerWeaponEntity> PlayerWeapons { get; set; } = new List<PlayerWeaponEntity>();
}
