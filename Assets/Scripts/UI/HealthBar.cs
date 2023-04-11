using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private float lastHealth;
    private float lastMaxHealth;

    public void Start()
    {
        slider.maxValue = 5;
    }

    public void Update()
    {
        SetHealth(Player.Singleton.Health);
        SetMaxHealth(Player.Singleton.MaxHealth);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
