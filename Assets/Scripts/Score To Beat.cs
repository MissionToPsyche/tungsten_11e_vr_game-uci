using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreToBeat : MonoBehaviour
{

    public TMP_Text score_to_beat;

    // Start is called before the first frame update
    void Start()
    {
        if (!score_to_beat) score_to_beat = GetComponent<TMP_Text>();
        score_to_beat.SetText("Score To Beat: " + PlayerPrefs.GetInt("HighScore", 0).ToString());
    }

}
