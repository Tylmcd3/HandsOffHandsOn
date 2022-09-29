using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerDamageFlash : MonoBehaviour
{
    [SerializeField] private float intensityCap = 0.25f;
    public PostProcessVolume volume;
    private Vignette red;

    [SerializeField] float time = 3;
    // Start is called before the first frame update
    void Start()
    {
        // find volume and assign red
        red = volume.profile.GetSetting<Vignette>();
        red.intensity.value = 0;
        red.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        // change intensity to go -> 0 over some time
        if (red.active)
        {
            // decrease over time, every (time) go down 0.2
            red.intensity.value -= (Time.deltaTime / time) * 0.2f;
            // reset
            if (red.intensity <= 0)
            {
                red.active = false;
            }
        }
    }

    public void FlashScreen()
    {
        // add 0.15, cap at cap
        red.intensity.value += (red.intensity.value < 0.15f)? 0.15f : intensityCap - red.intensity.value;
        red.active = true;
    }
}
