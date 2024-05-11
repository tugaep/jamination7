using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : ColorfulObject
{
    
    GameObject gem;
    

    public void Init(bool onLayer1)
    {
        if (onLayer1)
        {
            gameObject.layer = 7;
            GetComponent<Rigidbody2D>().excludeLayers = 0b1000000;
        }
        else
        {
            gameObject.layer = 6;
            GetComponent<Rigidbody2D>().excludeLayers = 0b0100000;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    { 
        if(col.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            Collected(player);
            Destroy(gameObject);
        }
    }

    public virtual void Collected(PlayerController player)
    {

    }

}
