using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeDuration = 0.1f;
    public float shakeAmount = 0.2f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPosition;
    private float currentShakeDuration = 0f;
    private bool isShaking = false;

    void Awake()
    {
        cameraTransform = GetComponent<Transform>(); 
        originalPosition = cameraTransform.localPosition; // Store original position
    }

    void Update()
    {
        if (isShaking)
        {
            if (currentShakeDuration > 0)
            {
                cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
                currentShakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                currentShakeDuration = 0f;
                cameraTransform.localPosition = originalPosition;
                isShaking = false;
            }
        }
    }

    public void Shake()
    {
        if (!isShaking)
        {
            currentShakeDuration = shakeDuration;
            isShaking = true;
        }
    }
}
