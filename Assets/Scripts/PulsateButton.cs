using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is for the Unity UI system

public class PulsateButton : MonoBehaviour
{
    private Image buttonImage; // Image component instead of SpriteRenderer
    private Transform buttonTransform; 
    private Vector3 originalScale;
    public float glowAmount = 0.1f; 
    public float glowSpeed = 2.0f; 
    public bool glow;
    private Color glowColor;
    private Color originalColor;

    private void Awake()
    {
        buttonImage = GetComponent<Image>(); // Get the Image component instead
        buttonTransform = GetComponent<Transform>();
        originalScale = buttonTransform.localScale;
        glowColor = Color.yellow;
        if (buttonImage != null)
            originalColor = buttonImage.color; // Use buttonImage.color instead
    }

    void Update()
{
    if (glow)
    {
        float glowNum = Mathf.Sin(Time.time * glowSpeed) * glowAmount;

        buttonTransform.localScale = originalScale * (1.0f + glowNum);

        buttonImage.color = Color.Lerp(Color.white, glowColor, (glowNum + 5.0f) / 2.0f); 
    }
    else
    {
        ResetButton();
    }
}


    public void ResetButton()
    {
        buttonTransform.localScale = originalScale;
        if (buttonImage != null)
            buttonImage.color = originalColor;
    }
}
