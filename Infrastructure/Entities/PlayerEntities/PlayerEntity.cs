using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class PlayerEntity
{
    public int Id { get; set; }

    public string CharacterName { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public int Kind { get; set; }

    public int Race { get; set; }

    public virtual KindEntity KindNavigation { get; set; } = null!;

    public virtual ICollection<PlayerWeaponEntity> PlayerWeapons { get; set; } = new List<PlayerWeaponEntity>();

    public virtual RaceEntity RaceNavigation { get; set; } = null!;
}
