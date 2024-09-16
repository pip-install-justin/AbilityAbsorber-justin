using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Animator animator;
    public GameObject water;
    private SpriteRenderer waterSR;
    public GameObject fireEnemy;
    public float growTime = 2f;  // Total time it takes to grow
    public float dryTime = 2f;
    private Vector3 initialScale;
    private Vector3 targetScale;
    public GameObject exitDoor;
    public GameObject exitDoorOpen;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        initialScale = water.transform.localScale;
        targetScale = new Vector2(1.6f,3.2f);  // Replace with your desired scale
        GameObject waterChild = water.transform.GetChild(0).gameObject; // assuming parentTransform has at least one child
        waterSR = waterChild.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openGate()
    {
        animator.SetBool("genTurnedOn", true);
        StartCoroutine(ReleaseWater());
        StartCoroutine(DeactivateAfterDelay(fireEnemy, 3f));
        exitDoor.SetActive(false); exitDoorOpen.SetActive(true);
    }

    IEnumerator DeactivateAfterDelay(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        target.SetActive(false);
    }
    
    IEnumerator ReleaseWater()
    {
        yield return new WaitForSeconds(1f);
        float elapsedTime = 0f;

        while (elapsedTime < growTime)
        {
            water.transform.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / growTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        // Ensure it gets exactly to the target
        water.transform.localScale = targetScale;
        
        yield return new WaitForSeconds(1f);
        Color startColor = waterSR.color;
        elapsedTime = 0f;

        while (elapsedTime < dryTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / dryTime);
            waterSR.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }
        Destroy(water);
    }
}
