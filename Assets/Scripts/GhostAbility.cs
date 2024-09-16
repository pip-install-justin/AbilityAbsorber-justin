using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAbility : MonoBehaviour
{
    public GameObject ghostForm;
    private SpriteRenderer renderer;

    private AbilityManager abilityManager;
    private bool usingStealth = false;
    private Coroutine myCoroutine;

    public bool getUsingStealth()
    {
        return usingStealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        abilityManager = GetComponent<AbilityManager>();
        renderer = ghostForm.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Start coroutine when spacebar is held down
        if (Input.GetKeyDown(KeyCode.Space) && abilityManager.getSelectedAbility() == "stealth")
        {
            usingStealth = true;
            myCoroutine = StartCoroutine(DecreaseHealthGradually());

        }

        // Stop coroutine when spacebar is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            usingStealth = false;
            if (myCoroutine != null)
                StopCoroutine(myCoroutine);
            // reset ghost form to full opaque
            Color c = renderer.color;
            c.a = 1f;
            renderer.color = c;
            // reset player default form to opaque
            c = GetComponent<SpriteRenderer>().color;
            c.a = 1f;
            GetComponent<SpriteRenderer>().color = c;

        }
    }

    IEnumerator DecreaseHealthGradually()
    {
        while (usingStealth)
        {
            GetComponent<PlayerController>().TakeDamage(0.01f, "self-stealth");
            // make ghost transparent
            Color c = renderer.color;
            c.a = 0.5f;
            renderer.color = c;

            // hide player default form (it's underneath all other forms)
            c = GetComponent<SpriteRenderer>().color;
            c.a = 0f;
            GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(0.01f);
        }

    }
}
