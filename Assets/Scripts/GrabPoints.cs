using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrabPoints : MonoBehaviour
{
    public TMP_Text grabpoints;

    private void Start()
    {
        if (!grabpoints) grabpoints = GetComponent<TMP_Text>();

        grabpoints.SetText("Points: " + PlayerPrefs.GetInt("HighScore", 0).ToString());
    }
}
