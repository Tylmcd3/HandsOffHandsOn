using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFlash : MonoBehaviour
{
    private Material bombMaterial;

    private Color originalColour;
    
    [SerializeField] private Color flashColour = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        bombMaterial = GetComponent<MeshRenderer>().material;
        originalColour = bombMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Flash()
    {
        bombMaterial.color = flashColour;
        yield return new WaitForSeconds(0.05f);
        bombMaterial.color = originalColour;
    }
}
