using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class BackgroundFade : MonoBehaviour
{
    private Image background;

    private Color newColor;
    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        newColor = background.color;
    }

    // Update is called once per frame
    void Update()
    {
        //newColor.a = 0;
        newColor.a = (Mathf.Sin(Mathf.PI + 0.5f * Time.time) + 1) / 2;
        background.color = newColor;
    }
}
