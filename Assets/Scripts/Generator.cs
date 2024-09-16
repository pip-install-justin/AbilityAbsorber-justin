
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour 
{
    private SpriteRenderer renderer;

    // optional gate, set in inspector
    public Gate gate;

    // optional soil, set in inspector
    public GameObject soil;

    void Start() 
    {
        renderer = GetComponent<SpriteRenderer>();
        //gate = GameObject.FindWithTag("Gate").GetComponent<Gate>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ElectricAbility"))
        {
            print("turning on generator");
            Color color = HexToColor("E5C1C1");
            renderer.color = color;  // Change here

            if (gate != null) {
                gate.openGate();
                print("opening gate");
            }
            if (soil != null) {
                print("removing soil");
                Destroy(soil.gameObject);
            }
        }
    }

    private Color HexToColor(string hex)
    {
        Color color = Color.black;
        ColorUtility.TryParseHtmlString("#" + hex, out color);
        return color;
    }
}