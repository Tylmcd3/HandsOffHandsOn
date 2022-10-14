using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnDamageText : MonoBehaviour
{
    public GameObject textPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnText(int damage)
    {
        Vector3 pos = transform.position + Vector3.up * 0.6f + (Random.insideUnitSphere * 0.4f);
        GameObject text = Instantiate(textPrefab, pos, transform.rotation);
        // set text
        text.GetComponent<FadeOutText>().damage = damage;
 
        // destroy after some time
        Destroy(text, 3.0f);
    }
}
