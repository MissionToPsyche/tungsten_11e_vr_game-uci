using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float time = 180f;
    private float time2 = 59;
    private float time3 = 59;
    private float time4 = 59;
    public TMP_Text time_remaining;

    private void Start()
    {
        if (!time_remaining) time_remaining = GetComponent<TMP_Text>();
    }

    public void Update()
    {
        if (time == 180f)
        {
            time_remaining.SetText("3:00");
        }
        else if (time >= 121f)
        {
            if (time2 < 10)
            {
                time_remaining.SetText("2:0" + time2.ToString("F0"));
            }
            else
            {
                time_remaining.SetText("2:" + time2.ToString("F0"));
            }
            time2 -= Time.deltaTime;
        }
        else if (time >= 61f)
        {
            if (time3 < 10)
            {
                time_remaining.SetText("1:0" + time3.ToString("F0"));
            }
            else
            {
                time_remaining.SetText("1:" + time3.ToString("F0"));
            }
            time3 -= Time.deltaTime;
        }
        else
        {
            if (time4 < 10)
            {
                time_remaining.SetText("0" + time4.ToString("F0"));
            }
            else
            {
                time_remaining.SetText(time4.ToString("F0"));
            }
            time4 -= Time.deltaTime;
        }

        if (time <= 0.0f)
        {
            TimeUp();
        }

        time -= Time.deltaTime;
    }

    public void TimeUp()
    {
        SceneManager.LoadScene("Game Over");
    }
}