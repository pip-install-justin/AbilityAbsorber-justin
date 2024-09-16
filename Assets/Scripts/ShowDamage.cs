using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDamage : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TurnRed()
    {
        StartCoroutine(ChangeColorTemporarily());
    }

    IEnumerator ChangeColorTemporarily()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1.0f); 
        resetColor();
    }

    public void resetColor()
    {
        spriteRenderer.color = originalColor;
    }
}

