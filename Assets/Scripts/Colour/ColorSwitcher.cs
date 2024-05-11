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
    [SerializeField] Image cooldownIcon;

    ColorManager colorManager;
    const float cooldown = 2f;

    float switchCooldown = 0f;

    int p1LightPointer = -1;    // -1: nothing, 0 red, 1 green, 2 blue
    int p2LightPointer = -1;    // -1: nothing, 0 red, 1 green, 2 blue

    float p1PointDelay = 0f;
    float p2PointDelay = 0f;

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
        cooldownIcon.fillAmount = switchCooldown / cooldown;
    }


    // This code is awful for maintability but i am too lazy to change
    void ColorSwitchingUpdate()
    {
        int colorsOnLayer0 = (colorManager.layer0Red ? 1 : 0) + (colorManager.layer0Green ? 1 : 0) + (colorManager.layer0Blue ? 1 : 0);

        // Player 1 Color Stealing
        #region PLAYER1
        if (colorsOnLayer0 == 1 && Input.GetButton("P1Steal") && p1LightPointer == -1 && switchCooldown < 0)
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

            p1LightPointer = -1;
            switchCooldown = cooldown;
        }
        else if (p1LightPointer != -1)
        {
            // Change the pointed color while holding the button

            if(p1PointDelay > 0.5f)
            {
                p1PointDelay = 0;

                if (!colorManager.layer0Red && p1LightPointer != 0)
                    p1LightPointer = 0;
                else if (!colorManager.layer0Green && p1LightPointer != 1)
                    p1LightPointer = 1;
                else
                    p1LightPointer = 2;
            }
            p1PointDelay += Time.deltaTime;

        }
        #endregion

        // Player 2 Color Stealing(same as the upper one)
        #region PLAYER2
        if (colorsOnLayer0 == 2 && Input.GetButton("P2Steal") && p2LightPointer == -1 && switchCooldown < 0)
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

            p2LightPointer = -1;
            switchCooldown = cooldown;

        }
        else if (p2LightPointer != -1)
        {
            // Change the pointed color while holding the button

            if (p1PointDelay > 0.5f)
            {
                p1PointDelay = 0;

                if (colorManager.layer0Red && p2LightPointer != 0)
                    p2LightPointer = 0;
                else if (colorManager.layer0Green && p2LightPointer != 1)
                    p2LightPointer = 1;
                else
                    p2LightPointer = 2;
            }
            p1PointDelay += Time.deltaTime;

        }
        #endregion

        switchCooldown -= Time.deltaTime;
    }
}
