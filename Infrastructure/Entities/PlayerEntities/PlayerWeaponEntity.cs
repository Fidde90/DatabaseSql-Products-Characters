using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class PlayerWeaponEntity
{
    public int Player { get; set; }

    public int Weapon { get; set; }

    public int Quantity { get; set; }

    public virtual PlayerEntity PlayerNavigation { get; set; } = null!;

    public virtual WeaponEntity WeaponNavigation { get; set; } = null!;
}
