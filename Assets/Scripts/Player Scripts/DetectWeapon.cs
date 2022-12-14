using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWeapon : MonoBehaviour
{
    // Keep a list of all weapons in range
    List<GameObject> weapons;
    public GameObject selected; // this object is only for outside reference, not used in detection

    void Start()
    {
        weapons = new List<GameObject>();
        selected = new GameObject();
    }

    void Update()
    {
        //Debug.Log(weapons.Count);
        if (weapons.Count > 0)
        {
            UpdateWeapons(ClosestObject());
        }
        else
        {
            selected = null; // be careful with this
        }
    }

    GameObject ClosestObject()
    {
        // Closest is at index 0 by default
        GameObject returnObject = weapons[0];
        float dist = Vector3.Distance(weapons[0].transform.position, transform.position);

        foreach (GameObject g in weapons)
        {
            // Set return object to closest one
            if (Vector3.Distance(g.transform.position, transform.position) < dist)
            {
                dist = Vector3.Distance(g.transform.position, transform.position);
                //index = weapons.FindIndex(x => x == g); // lambda scary
                returnObject = g;
            }
        }

        return returnObject;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "DroppedWeapon")
        {
            // LAZY BANDAID FIX, THE WEAPONS GET ADDED TWICE BY DEFAULT CUS UNITY SUCK
            // Add the weapon to list
            if(!weapons.Contains(collider.gameObject)) 
                weapons.Add(collider.gameObject);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "DroppedWeapon")
        {
            // Remove weapon from list, update weapons
            weapons.Remove(collider.gameObject);
            // Unselect weapon
            collider.gameObject.GetComponent<WeaponGlow>().UnselectWeapon();
        }
    }

    void UpdateWeapons(GameObject weapon)
    {
        selected = weapon;
        // Select selected one, update all others to unselected
        weapons.Find(x => x == weapon).GetComponent<WeaponGlow>().SelectWeapon();

        foreach (GameObject g in weapons)
        {
            if (weapons.Find(x => x == g) != weapon)
            {
                g.GetComponent<WeaponGlow>().UnselectWeapon();
            }
        }
    }

    public void ClearList()
    {
        weapons.Clear();
    }

    public void RemoveSelected()
    {
        // TODO fix this
        // selected.SetActive(false);
        weapons.Remove(selected);
        Debug.Log("removed " + weapons.Count);
        Destroy(selected);
        selected = null;
    }
}
