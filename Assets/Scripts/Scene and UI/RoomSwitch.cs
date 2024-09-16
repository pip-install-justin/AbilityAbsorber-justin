using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSwitch : MonoBehaviour {

    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;
    public GameObject Banner;
    
    


	// Use this for initialization
	void Start () {
        cam = Camera.main.GetComponent<CameraMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (Banner != null)
            {
	            Banner.SetActive(true);
	            Time.timeScale = 0;
            }
        }
    }


}
