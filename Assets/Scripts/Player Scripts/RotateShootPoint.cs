using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShootPoint : MonoBehaviour
{
    private Transform cameraObj;
    // Start is called before the first frame update
    void Start()
    {
        cameraObj = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = cameraObj.rotation;
    }
}
