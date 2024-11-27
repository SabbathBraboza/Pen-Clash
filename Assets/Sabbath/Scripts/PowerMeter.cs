using UnityEngine;
using UnityEngine.UI;

public class PowerMeter : MonoBehaviour
{
    [Header("Parameters:")]
    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 1f;
    [SerializeField] private float speed = 1f;

    [Header("GameObject:")]
    [SerializeField] Slider m_Slider;

    private void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        float oscillatingValue = Mathf.Lerp(minValue, maxValue, t);
        m_Slider.minValue = minValue; m_Slider.maxValue = maxValue;
        m_Slider.value = oscillatingValue;
    }
}
