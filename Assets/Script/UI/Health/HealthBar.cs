using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    public void SetHealth(float healthNormalized)
    {
        healthSlider.value = healthNormalized;
    }
}