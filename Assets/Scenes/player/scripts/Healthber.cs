
using UnityEngine;
using UnityEngine.UI;

public class Healthber : MonoBehaviour
{
    public Image healthBarImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float fillAmount = (float)currentHealth / maxHealth;
        healthBarImage.fillAmount = fillAmount;
    }

}
