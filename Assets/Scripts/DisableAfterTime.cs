using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    public float TimeToDisable = 0.1f;
    float TimeTillDisable;
    // Start is called before the first frame update
    void OnEnable()
    {
        TimeTillDisable = TimeToDisable;
    }

    // Update is called once per frame
    void Update()
    {
        TimeTillDisable -= Time.deltaTime;
        if(TimeTillDisable <= 0) this.gameObject.SetActive(false);
    }
}
