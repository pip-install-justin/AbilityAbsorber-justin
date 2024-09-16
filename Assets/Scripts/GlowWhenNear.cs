using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GlowWhenNear : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform transform;
    private Vector3 originalScale;
    public float glowAmount = 0.1f; // how much to scale the object for the glow
    public float glowSpeed = 2.0f; // speed of the glow effect
    public bool glow;
    private Color glowColor;
    private Color originalColor;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        originalScale = transform.localScale;
        glowColor = Color.yellow;
        originalColor = spriteRenderer.color;
    }

     void Update()
    {
        if (glow)
        {
            float glowNum = Mathf.Sin(Time.time * glowSpeed) * glowAmount;
            
            transform.localScale = originalScale * (1.0f + glowNum);
            
            spriteRenderer.color = Color.Lerp(Color.white, glowColor, (glowNum + 5.0f) / 2.0f);
        }

    }

     public void reset()
     {
         print("reset called");
         transform.localScale = originalScale;
         spriteRenderer.color = originalColor;
     }
    
}

