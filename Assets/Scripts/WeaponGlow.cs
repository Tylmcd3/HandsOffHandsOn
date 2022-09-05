using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGlow : MonoBehaviour
{
    // Default values
    public Color defaultColor = Color.white;
    public Color selectedColor = Color.yellow;
    Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
    }

    public void UnselectWeapon()
    {
        outline.OutlineColor = defaultColor;
    }

    public void SelectWeapon()
    {
        outline.OutlineColor = selectedColor;
    }
}
