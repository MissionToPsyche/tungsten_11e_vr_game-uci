using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private TMP_Text text;

    private void Start()
    {
        if (!text) text = GetComponent<TMP_Text>();
        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        int score = PlayerPrefs.GetInt("Score", 0);
        string res = "";
        if (score > highscore) {
            res += "New Highscore!\n";
            PlayerPrefs.SetInt("Highscore", score);
        }
        res += $"Score: {score}";
        res += $"Highscore: {highscore}";
        text.SetText(res);
    }
}
