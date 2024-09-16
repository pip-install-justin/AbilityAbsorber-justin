using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public float speed = 5f; // Public variable for speed, you can set it in the Inspector.
    public Vector2 pointA = new Vector2(34.6f, 3.58f);
    public Vector2 pointB = new Vector2(53.42f, 3.58f);
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool movingRight = true;
     
    private bool hasCollided = false;
     private static int count=0; 
    private bool isCollidingWithGlue = false;
private bool isCollidingWithPressurePlate = false;
    void Start()
    {
        targetPosition = pointB; // Start by moving towards pointB.
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move the sprite horizontally towards the target position.
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the sprite reached the target position.
        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            // If the sprite reached pointA or pointB, change the target position to move in the opposite direction.
            if (targetPosition == pointA)
                targetPosition = pointB;
            else if (targetPosition == pointB)
                targetPosition = pointA;
        }
    }
/*
     private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Glue") && other.gameObject.name != "GlueBottle" )
        {
            Debug.Log("Moving sprite stuck in glue");
            isCollidingWithGlue = true;
            speed = 0f;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            if (other.gameObject.CompareTag("PressurePlate"))
            {  Debug.Log("Moving sprite on pressure plate");
              count++; }
        }
        if(count>=2)
        {
            GameObject closed_door = GameObject.Find("Closed door");
            Destroy(closed_door);
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D other) 
{
    // Check for Glue
    if (other.gameObject.CompareTag("Glue") && other.gameObject.name != "GlueBottle") 
    {
        isCollidingWithGlue = true;
        speed = 0f;
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Check for PressurePlate
    if (other.gameObject.CompareTag("PressurePlate"))
    {
        isCollidingWithPressurePlate = true;
         
            SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.green; // You can change this to another default color if needed
            }
    }

    CheckCollisionState();
}

private void OnTriggerExit2D(Collider2D other)
{
    // Check for Glue
    if (other.gameObject.CompareTag("Glue"))
    {
        isCollidingWithGlue = false;
        
    }

    // Check for PressurePlate
    if (other.gameObject.CompareTag("PressurePlate"))
    {
        isCollidingWithPressurePlate = false;
        SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.red; // You can change this to another default color if needed
            }
    }
}

private void CheckCollisionState()
{
    if (isCollidingWithGlue && isCollidingWithPressurePlate)
    {
        Debug.Log("Moving sprite stuck in glue and on pressure plate");
        // ... Your logic here ...
        count++;
        
        if(count == 2) 
        {
            GameObject closed_door = GameObject.Find("Closed door");
            Destroy(closed_door);
        }
    }
}
}
