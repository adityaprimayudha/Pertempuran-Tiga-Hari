using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public void SetHealth(float healthNormalized)
    {
        healthSlider.value = healthNormalized;
    }
}