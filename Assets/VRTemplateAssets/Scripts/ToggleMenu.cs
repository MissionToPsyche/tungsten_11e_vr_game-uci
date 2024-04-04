using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    public GameObject menu; // Reference to the menu GameObject

    private bool menuVisible = false; // Current visibility state of the menu

    // Function to toggle the visibility of the menu
    public void ToggleMenuVisibility()
    {
        menuVisible = !menuVisible;
        menu.SetActive(menuVisible);
    }
}

