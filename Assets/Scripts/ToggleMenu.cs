using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    public GameObject menu;

    void Start()
    {

        menu.SetActive(false);
    }

    public void ToggleMenuVisibility()
    {

        menu.SetActive(!menu.activeSelf);
    }

}