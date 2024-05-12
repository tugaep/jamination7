using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSwitcher : MonoBehaviour
{
    [SerializeField] RectTransform lightRed;
    [SerializeField] RectTransform lightGreen;
    [SerializeField] RectTransform lightBlue;
    [SerializeField] RectTransform p1Pointer;
    [SerializeField] RectTransform p2Pointer;
    [SerializeField] Image p1CooldownIcon;
    [SerializeField] Image p2CooldownIcon;

    ColorManager colorManager;
    const float cooldown = 2f;

    float p1Cooldown = 0f;
    float p2Cooldown = 0f;

    int p1LightPointer = -1;    // -1: nothing, 0 red, 1 green, 2 blue
    int p2LightPointer = -1;    // -1: nothing, 0 red, 1 green, 2 blue

    float p1PointDelay = 0f;
    float p2PointDelay = 0f;

    public bool allowColorSwitching = true;

    void Start()
    {
        colorManager = ColorManager.instance;
    }

    void Update()
    {
        ColorSwitchingUpdate();

        // -------- UI -------- //

        // Lights Position Update

        lightRed.anchoredPosition = lightRed.anchoredPosition * (1 - Time.deltaTime * 10) + new Vector2(colorManager.layer0Red ? -260 : 100, 0) * Time.deltaTime * 10;
        lightGreen.anchoredPosition = lightGreen.anchoredPosition * (1 - Time.deltaTime * 10) + new Vector2(colorManager.layer0Green ? -180 : 180, 0) * Time.deltaTime * 10;
        lightBlue.anchoredPosition = lightBlue.anchoredPosition * (1 - Time.deltaTime * 10) + new Vector2(colorManager.layer0Blue ? -100 : 260, 0) * Time.deltaTime * 10;

        // P1 Light Pointer Position Update
        if (p1LightPointer == -1)
        {
            p1Pointer.sizeDelta = Vector2.zero;
        }
        else
        {
            p1Pointer.sizeDelta = Vector2.one * 60;

            Vector2 desiredPos = new Vector2(100 + p1LightPointer * 80, 0);
            p1Pointer.anchoredPosition = p1Pointer.anchoredPosition * (1 - Time.deltaTime * 10) + desiredPos * Time.deltaTime * 10;
        }

        // P2 Light Pointer Position Update
        if (p2LightPointer == -1)
        {
            p2Pointer.sizeDelta = Vector2.zero;
        }
        else
        {
            p2Pointer.sizeDelta = Vector2.one * 60;

            Vector2 desiredPos = new Vector2(-260 + p2LightPointer * 80, 0);
            p2Pointer.anchoredPosition = p2Pointer.anchoredPosition * (1 - Time.deltaTime * 10) + desiredPos * Time.deltaTime * 10;
        }

        // Cooldown Icon Update
        p1CooldownIcon.fillAmount = p1Cooldown / cooldown;
        p2CooldownIcon.fillAmount = p2Cooldown / cooldown;
    }

    // This code is awful for maintability but i am too lazy to change, yeah i regret this
    void ColorSwitchingUpdate()
    {
        int colorsOnLayer0 = (colorManager.layer0Red ? 1 : 0) + (colorManager.layer0Green ? 1 : 0) + (colorManager.layer0Blue ? 1 : 0);

        // Player 1 Color Stealing
        #region PLAYER1
        if (Input.GetButton("P1Steal") && p1LightPointer == -1 && p1Cooldown < 0 && allowColorSwitching)
        {
            // When button is pressed, start pointing a color

            if (!colorManager.layer0Red)
                p1LightPointer = 0;
            else if (!colorManager.layer0Green)
                p1LightPointer = 1;
            else
                p1LightPointer = 2;
        }
        else if (!Input.GetButton("P1Steal") && p1LightPointer != -1)
        {
            // When pressing is over, steal the pointed color

            colorManager.ChangeLightPosition(p1LightPointer);

            // balancing
            if(colorsOnLayer0 == 2)
            {
                int opt1, opt2;
                if(p1LightPointer == 0)
                {
                    opt1 = 1;
                    opt2 = 2;
                }
                else if (p1LightPointer == 1)
                {
                    opt1 = 0;
                    opt2 = 2;
                }
                else
                {
                    opt1 = 0;
                    opt2 = 1;
                }

                colorManager.ChangeLightPosition(Random.Range(0, 2) == 0 ? opt1 : opt2);
            }

            p1LightPointer = -1;
            p1Cooldown = cooldown;
        }
        else if (p1LightPointer != -1 && allowColorSwitching)
        {
            // Change the pointed color while holding the button

            if(p1PointDelay > 0.5f)
            {
                p1PointDelay = 0;

                int newPtr = PointNextLight(p1LightPointer, true);

                if (newPtr != -1)
                {
                    p1LightPointer = newPtr;
                }
            }
            p1PointDelay += Time.deltaTime;

        }
        #endregion

        // Player 2 Color Stealing(same as the upper one)
        #region PLAYER2
        if (Input.GetButton("P2Steal") && p2LightPointer == -1 && p2Cooldown < 0 && allowColorSwitching)
        {
            // When button is pressed, start pointing a color

            if (colorManager.layer0Red)
                p2LightPointer = 0;
            else if (colorManager.layer0Green)
                p2LightPointer = 1;
            else
                p2LightPointer = 2;
        }
        else if (!Input.GetButton("P2Steal") && p2LightPointer != -1)
        {
            // When pressing is over, steal the pointed color

            colorManager.ChangeLightPosition(p2LightPointer);

            // balancing
            if (colorsOnLayer0 == 1)
            {
                int opt1, opt2;
                if (p1LightPointer == 0)
                {
                    opt1 = 1;
                    opt2 = 2;
                }
                else if (p1LightPointer == 1)
                {
                    opt1 = 0;
                    opt2 = 2;
                }
                else
                {
                    opt1 = 0;
                    opt2 = 1;
                }

                colorManager.ChangeLightPosition(Random.Range(0, 2) == 0 ? opt1 : opt2);
            }

            p2LightPointer = -1;
            p2Cooldown = cooldown;

        }
        else if (p2LightPointer != -1 && allowColorSwitching)
        {
            // Change the pointed color while holding the button

            if (p1PointDelay > 0.5f)
            {
                p1PointDelay = 0;

                int newPtr = PointNextLight(p2LightPointer, false);

                if(newPtr != -1)
                {
                    p2LightPointer = newPtr;
                }
            }
            p1PointDelay += Time.deltaTime;

        }
        #endregion

        p1Cooldown -= Time.deltaTime;
        p2Cooldown -= Time.deltaTime;
    }

    int PointNextLight(int currentPtrPos, bool forLayer0)
    {
        int currentPointer = currentPtrPos;

        for (int i = 0; i < 3; i++)
        {
            currentPointer = (currentPointer + 1) % 3;

            if(colorManager.layer0Red != forLayer0 && currentPointer == 0)
            {
                return currentPointer;
            }
            else if (colorManager.layer0Green != forLayer0 && currentPointer == 1)
            {
                return currentPointer;
            }
            else if (colorManager.layer0Blue != forLayer0 && currentPointer == 2)
            {
                return currentPointer;
            }
        }

        return -1;
    }
}
