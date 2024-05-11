using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGem : Gem
{
    public override void Collected(PlayerController player)
    {
        player.Heal();
    }
}
