using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickScript : MonoBehaviour
{
    public int brickLives;
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    public RockAbility raScript;
    // Start is called before the first frame update
    void Start()
    {
        brickLives = 2;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (brickLives < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && raScript.isRushing)
        {
            print("works");
            brickLives--;
            if (brickLives >= 0)
            {
                spriteRenderer.sprite = sprites[brickLives]; 
            }
        }
    }
}
