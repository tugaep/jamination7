using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedGem : Gem
{
    public override void Collected(PlayerController player)
    {
        StartCoroutine(player.attackSpeedModifiy());
    }

}
