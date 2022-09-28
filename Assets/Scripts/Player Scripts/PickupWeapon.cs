using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    private DetectWeapon detect;
    // Start is called before the first frame update
    void Start()
    {
        detect = GetComponent<DetectWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        // Default key to pickup weapon is E
        if (Input.GetKeyDown(KeyCode.E))
        {
            // if theres a weapon to pick up...
            if (detect.selected)
            {
                // ...
                Debug.Log("Picking up " + detect.selected.GetComponent<DroppedWeaponValues>().type.ToString());
            }
        }
    }
}
