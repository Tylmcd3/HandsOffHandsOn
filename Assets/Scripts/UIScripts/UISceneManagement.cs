using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManagement : MonoBehaviour
{
    // Loads scene given a name
    // just make sure the scene is added to build settings
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    // Toggles a gameobject on / off
    // Use this when you need to switch between UI elements within a scene
    public void ToggleUIComponent(GameObject toToggle)
    {
        toToggle.SetActive(!toToggle.activeSelf);
    }
}
