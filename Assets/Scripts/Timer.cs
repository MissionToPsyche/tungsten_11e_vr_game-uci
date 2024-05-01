using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time = 180f;
    public TMP_Text time_remaining;

    public void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0.0f)
        {
            TimeUp();
        }
    }

    public void TimeUp()
    {
        //change to the next scene
    }
}