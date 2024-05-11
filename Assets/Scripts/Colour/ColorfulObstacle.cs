using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorfulObstacle : ColorfulObject
{
    Collider2D collider;

    public override void Start()
    {
        base.Start();

        collider = GetComponent<Collider2D>();
    }

    public override void ColorChanged()
    {
        base.ColorChanged();

        collider.enabled = objectVisible;
    }
}
