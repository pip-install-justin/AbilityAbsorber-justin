using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InvisibleAbility : MonoBehaviour
{
    public ParticleSystem identifierEffect;
    private Renderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        // Get all Renderer components on this game object and its children
        renderers = GetComponentsInChildren<Renderer>();

        // Start invisible
        SetInvisible(true);
    }

    public void SetInvisible(bool invisible)
    {
        // Toggle visibility of all Renderer components
        foreach (var renderer in renderers)
        {
            // Get the materials of the renderer
            Material[] materials = renderer.materials;

            // For each material, adjust the alpha value of its color
            for (int i = 0; i < materials.Length; i++)
            {
                Color color = materials[i].color;
                color.a = invisible ? 0 : 1;
                materials[i].color = color;
            }

            // Apply the changed materials
            renderer.materials = materials;
        }

        // Toggle identifier effect
        if (invisible)
        {
            identifierEffect.Play();
        }
        else
        {
            identifierEffect.Stop();
        }
    }
}
