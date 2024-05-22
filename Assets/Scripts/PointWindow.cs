using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointWindow : MonoBehaviour
{
    public TMP_Text point;
    private int points = 0;

    private void Start()
    {
        if (!point) point = GetComponent<TMP_Text>();
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void AddPoints(int points) {
        this.points += points;
        point.SetText("Points: " + this.points);
    }
}
