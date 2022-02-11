using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthWidget : MonoBehaviour
{
    [SerializeField]private Text healthText;
    [SerializeField]private Slider healtSlider;

    private int startHealth;
    private int currentHealth;


    public void Init(int health)
    {
        startHealth = health;
        ChangeHealth(startHealth);
        Hide();
    }

    public void ChangeHealth(int value)
    {
        currentHealth = value;
        float h = (float)currentHealth / startHealth;
        Debug.Log(h);
        healthText.text = currentHealth.ToString();
        healtSlider.value = h;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
