using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Slider damageIndicatorBar;
    Slider slider;  
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)player.currentHealth / player.maxHealth;
        
        if(damageIndicatorBar.value > slider.value)
            damageIndicatorBar.value -= Time.deltaTime * 0.1f;

        if(damageIndicatorBar.value < slider.value)
            damageIndicatorBar.value = slider.value;
    }
}
