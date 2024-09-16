using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public PlayerController playerHealth;
    private Color flashColor = Color.white;
    public float flashDuration = 0.3f;
    private string ability;
    private bool isFlashing;
    
    private Image fillImage;
    // Update is called once per frame

     void Awake()
    {
        isFlashing = false;
        fillImage = slider.fillRect.GetComponent<Image>();
    }

    void Update()
    {
        slider.value = playerHealth.health;
    }

    public void setHealthBar(float maxHealth)
    {
        this.ability = ability;
        slider.maxValue = maxHealth;
        RectTransform backgroundRT = slider.transform.Find("Background").GetComponent<RectTransform>();
        RectTransform fillAreaRT = slider.transform.Find("Fill Area").GetComponent<RectTransform>();
        backgroundRT.offsetMax = new Vector2(25 * maxHealth, backgroundRT.offsetMax.y); // -50 is the new "right" value, keep the original "top" value
        fillAreaRT.offsetMax = new Vector2(25 * maxHealth, fillAreaRT.offsetMax.y);

    }
    
    public void FlashBar()
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashHealthBar());
        }
    }

    private IEnumerator FlashHealthBar()
    {
        isFlashing = true;
        Color originalColor = fillImage.color;
        fillImage.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        fillImage.color = originalColor;
        
        yield return new WaitForSeconds(flashDuration);
        
        fillImage.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        fillImage.color = originalColor;
        isFlashing = false;
    }
}






