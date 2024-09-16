using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEnemy : MonoBehaviour
{

    public float attractionForce; // Strength of the attraction force
    public float radius;
    public float durationOfAttract;
    public float gapBetweenAttracts;
    public GameObject magnetRadiusPrefab;
    public GameObject player;
    
    private bool isAlive = true;
    private bool usingAbility = false;
    private AbilityManager abilityManager;

    private GameObject newradius;

    private float health = 5f;
    private ShowDamage showDamage;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AttractRoutine());
        abilityManager = player.GetComponent<AbilityManager>();
        showDamage = GetComponent<ShowDamage>();
    }

    private IEnumerator AttractRoutine()
    {
        while (isAlive)
        {
            //Debug.Log("using magnet radius ability");
 
            // create circle magnet radius
            Vector2 spawnPosition = transform.position;
            newradius = Instantiate(magnetRadiusPrefab, spawnPosition, Quaternion.identity);
            newradius.transform.parent = transform;
            Destroy(newradius, durationOfAttract);
            
            // Wait for the next
            yield return new WaitForSeconds(durationOfAttract + gapBetweenAttracts);
        }
    }

    // for the attraction only
    void FixedUpdate()
    {
        if (abilityManager.getSelectedAbility() == "electric" || abilityManager.getSelectedAbility() == "magnet") {
            if (newradius != null && !ReferenceEquals(newradius, null)) {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius); // Adjust the radius as needed

                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject != gameObject)
                    {
                        Vector2 direction = transform.position - collider.transform.position;
                        if (collider.attachedRigidbody != null)
                            collider.attachedRigidbody.AddForce(direction.normalized * attractionForce);
                    }
                }
            }
        }
        
    }

    // touching the actual magnet
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("ElectricAbility"))
        {
            TakeDamage(2f);
        }
    }


    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            showDamage.TurnRed();
            print(health);
        }
        if (health <= 0)
        {
            // make into corpse
            Debug.Log("killed magnet");
            isAlive = false;
            Color color = HexToColor("372E2E");
            GetComponent<Renderer>().material.color = color;
        }
    }
    
    private Color HexToColor(string hex)
    {
        Color color = Color.black;
        ColorUtility.TryParseHtmlString("#" + hex, out color);
        return color;
    }

    public bool getIsAlive() {
        return isAlive;
    }
}
