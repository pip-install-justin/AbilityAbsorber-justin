using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostwall : MonoBehaviour
{
    public GameObject player;
    private GhostAbility ghostAbility;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        ghostAbility = player.GetComponent<GhostAbility>();
        collider = GetComponent<Collider2D>();
    }

    void Update() {
        if (ghostAbility.getUsingStealth()) {
            collider.enabled = false;
        } else {
            collider.enabled = true;
        }
    }
    
}
