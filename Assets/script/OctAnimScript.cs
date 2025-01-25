using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OctAnimScript : MonoBehaviour
{
    
    public Material material; // Assign your Shader Graph material here

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (material == null)
        {
            Debug.LogError("Material with Shader Graph not assigned!");
        }
    }

    void Update()
    {
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            // Get the current sprite's texture
            Texture2D currentTexture = spriteRenderer.sprite.texture;

            // Update the material's texture property
            material.SetTexture("_MainTex", currentTexture);
        }
    }

}
