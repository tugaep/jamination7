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
    SpriteRenderer renderer;

    Color desiredColor = Color.white;
    bool objectVisible = false;

    private void Start()
    {
        colorManager = ColorManager.instance;
        renderer = GetComponent<SpriteRenderer>();

        ColorManager.instance.onColorChanged += ColorChanged;
    }

    private void Update()
    {
        renderer.color = (renderer.color * (1 - 5 * Time.deltaTime) + desiredColor * 5 * Time.deltaTime);
    }

    private void ColorChanged()
    {
        desiredColor = new Color((colorManager.layer0Red && colorRed) != objectOnLayer1 ? 1 : 0,
                                    (colorManager.layer0Green && colorGreen) != objectOnLayer1 ? 1 : 0,
                                    (colorManager.layer0Blue && colorBlue) != objectOnLayer1 ? 1 : 0);

        objectVisible = (colorManager.layer0Red == colorRed) && (colorManager.layer0Green == colorGreen) && (colorManager.layer0Blue == colorBlue);
    }
}
