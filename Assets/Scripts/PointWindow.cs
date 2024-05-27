using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointWindow : MonoBehaviour
{
    private TMP_Text point;
    private int points = 0;

    private void Start()
    {
        if (!point) point = GetComponent<TMP_Text>();
    }

    public void AddPoints(Delivery delivery) {
        this.points += delivery.points;
        point.SetText("Points: " + this.points);

        if (this.points > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", this.points);
        }
    }
}
