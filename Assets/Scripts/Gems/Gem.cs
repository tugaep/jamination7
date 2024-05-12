using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : ColorfulObject
{
    [SerializeField] GameObject eatParticles;
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

        // Random color
        int randomColor = Random.Range(1, 8);
        colorRed = (randomColor & 0b100) == 0b100;
        colorGreen = (randomColor & 0b010) == 0b010;
        colorBlue = (randomColor & 0b001) == 0b001;
    }
    public override void Start()
    {
        base.Start();

        ColorChanged();
    }

    private void OnTriggerEnter2D(Collider2D col)
    { 
        if(col.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            Collected(player);
            Destroy(gameObject);

            // Particles
            GameObject obj = Instantiate(eatParticles, transform.position, Quaternion.identity);
            obj.layer = gameObject.layer;
            obj.GetComponent<ParticleSystem>().startColor = renderer.color;
        }
    }

    public virtual void Collected(PlayerController player)
    {

    }

}
