using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject NotImage;
    
    void Start()
    {
        SetNotImage();
    }

    void SetNotImage() 
    {
        if (PersistentSettings.timer)
        {
            NotImage.SetActive(false);
        }
        else
        {
            NotImage.SetActive(true);
        }
    }

    public void ToggleTimer()
    {
        PersistentSettings.timer = !PersistentSettings.timer;
        SetNotImage();
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
