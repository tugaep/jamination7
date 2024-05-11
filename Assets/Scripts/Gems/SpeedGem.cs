using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGem : Gem
{
    public override void Collected(PlayerController player)
    {
        StartCoroutine(player.IncreaseSpeed());
    }
}
