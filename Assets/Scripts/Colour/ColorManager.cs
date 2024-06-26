using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorManager : MonoBehaviour
{
    public UnityAction onColorChanged;

    public bool layer0Red = true;
    public bool layer0Green = true;
    public bool layer0Blue = false;

    void Start()
    {
        layer0Green = Random.Range(0, 2) == 0;
        if(Random.Range(0, 2) == 0)
        {
            layer0Blue = true;
            layer0Red = false;
        }

        onColorChanged.Invoke();
    }

    /// <summary>
    /// Moves the light source from a scene to the other
    /// </summary>
    /// <param name="lightId">0: Red, 1: Green, 2: Blue</param>
    public void ChangeLightPosition(int lightId)
    {
        switch(lightId)
        {
            case 0:
                layer0Red = !layer0Red; break;
            case 1:
                layer0Green = !layer0Green; break;
            case 2:
                layer0Blue = !layer0Blue; break;
        }

        onColorChanged.Invoke();
    }

    // Singleton Object

    public static ColorManager instance;
    public ColorManager() { instance = this; }

}
