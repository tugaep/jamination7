using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorfulObject : MonoBehaviour
{
    public bool objectOnLayer1 = true;
    public bool colorRed = true;
    public bool colorGreen = true;
    public bool colorBlue = true;

    ColorManager colorManager;
    [HideInInspector] public SpriteRenderer renderer;

    Color desiredColor = Color.white;
    public bool objectVisible = false;

    public virtual void Start()
    {
        colorManager = ColorManager.instance;
        renderer = GetComponentInChildren<SpriteRenderer>();

        ColorManager.instance.onColorChanged += ColorChanged;
    }

    public virtual void Update()
    {
        renderer.color = (renderer.color * (1 - 5 * Time.deltaTime) + desiredColor * 5 * Time.deltaTime);
    }

    public virtual void ColorChanged()
    {
        desiredColor = new Color((colorManager.layer0Red != objectOnLayer1) && colorRed ? 1 : 0.5f,
                                    (colorManager.layer0Green != objectOnLayer1) && colorGreen ? 1 : 0.5f,
                                    (colorManager.layer0Blue != objectOnLayer1) && colorBlue ? 1 : 0.5f);

        objectVisible = ((colorManager.layer0Red != objectOnLayer1) && !colorRed) ||
                        ((colorManager.layer0Green != objectOnLayer1) && !colorGreen) ||
                        ((colorManager.layer0Blue != objectOnLayer1) && !colorBlue);
    }
}
