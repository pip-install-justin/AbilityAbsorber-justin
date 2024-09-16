using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screech_ability : MonoBehaviour
{
    private AbilityManager abilityManager;
    private GameObject waveObject;
    private PlayerMovement playerMovement;
    public GameObject shockWave;
    public Transform shockWaveSpawn;

    private bool isMegaScreech = false;
    float regularWidth = 3f;
    float regularHeight = 3f;
    float megaWidth = 8f;
    float megaHeight = 7f;


    // Start is called before the first frame update
    void Start()
    {
        abilityManager = GetComponent<AbilityManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && abilityManager.getSelectedAbility() == "screech")
        {
            if (waveObject == null)
            {
                isMegaScreech = false;
                Vector3 spawnPosition = shockWaveSpawn.position;
                waveObject = Instantiate(shockWave, spawnPosition, Quaternion.identity);
            }
        }

        if (waveObject != null)
        {
            isMegaScreech = false;
            
            // if the shockwave is going through the Megaphone, enlarge it, set MegaScreech=true
            // now it can shatter rock
            GameObject[] megaphones = GameObject.FindGameObjectsWithTag("Megaphone");
            foreach (GameObject megaphone in megaphones)
            {
                float distance = Vector3.Distance(waveObject.transform.position, megaphone.transform.position);
                if (distance <= 3)
                {
                    print("player shockwave is near megaphone");
                    isMegaScreech = true;
                    break;
                }
            }
            float newWidth;
            float newHeight;
            Vector3 offset; 
            if (isMegaScreech) {
                newWidth = megaWidth;
                newHeight = megaHeight;
                offset = new Vector3(3f, 0f, 0f);
            }
            else {
                newWidth = regularWidth;
                newHeight = regularHeight;
                offset = new Vector3(0f, 0f, 0f);
            }


            
            if (!playerMovement.isMovingLeft)
            {
                waveObject.transform.position = shockWaveSpawn.position + offset;

                // Flip the sprite to face right
                waveObject.transform.localScale = new Vector3(newWidth, newHeight, 1f);
            }
            else
            {
                waveObject.transform.position = shockWaveSpawn.position + (-1 * offset);

                // Flip the sprite to face left
                waveObject.transform.localScale = new Vector3(-1f * newWidth, newHeight, 1f);
            }

            Destroy(waveObject, 3f);
        }
    }

    public bool getIsMegaScreech() {
        return isMegaScreech;
    }
}
