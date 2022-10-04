using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlashOnHit : MonoBehaviour
{
    private Color original;
    private Material originalMaterial;
    private MeshRenderer meshRenderer;
    [SerializeField] private Color flashColour = Color.red; // default red
    [SerializeField] private Material flashMaterial;

    [SerializeField] private float flashTime = 0.5f; // defulat 0.5
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalMaterial = meshRenderer.material;
        original = meshRenderer.material.color;
    }

    public void FlashColour()
    {
        meshRenderer.material = flashMaterial;
        meshRenderer.material.color = flashColour;
        Invoke("ResetColour", flashTime);
    }

    private void ResetColour()
    {
        meshRenderer.material = originalMaterial;
        meshRenderer.material.color = original;
    }
}
