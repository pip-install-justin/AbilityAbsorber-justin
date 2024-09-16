using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBat : MonoBehaviour
{
    public GameObject Torch1;
    public GameObject Torch2;
    public GameObject puzzleBanner;

    private Bat bossBat;

    private bool isActionPerformed = false;

    // Start is called before the first frame update
    void Start()
    {
        bossBat = GetComponent<Bat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossBat.getIsCorpse() && !isActionPerformed) {

            Torch1.SetActive(true);
            Torch2.SetActive(true);
            puzzleBanner.SetActive(true);
            isActionPerformed = true;
            Time.timeScale = 0;
        }
    }
}
