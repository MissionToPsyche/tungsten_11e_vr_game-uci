using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointWindow : MonoBehaviour
{
    public TMP_Text Point;
    string text = "Points: ";
    int point_value = 50;

    // Start is called before the first frame update
    void Start()
    {
        //Point.SetText(text + point_value);
    }

    // Update is called once per frame
    void Update()
    {
        if (1 == 1)
        {
            point_value++;
            Point.SetText(text + point_value);
        }
    }
}
