using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TMP_Text highscore;

    private void Start()
    {
        if (!highscore) highscore = GetComponent<TMP_Text>();
        highscore.SetText("High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString());
    }
}
