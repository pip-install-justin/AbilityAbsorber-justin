using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTree : MonoBehaviour
{
    private GameObject fire;
    public Renderer renderer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        fire = GameObject.FindGameObjectWithTag("FireAbility");
        if (other.gameObject == fire)
        {
            print("tree/crate burnt to black");
            Color color = HexToColor("372E2E");
            renderer.material.color = color;
        }
    }

    // Method to convert Hex color code to Color
    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r,g,b, 255);
    }
}
