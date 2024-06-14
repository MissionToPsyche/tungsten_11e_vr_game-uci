using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float time = 180f;
    private TMP_Text time_remaining;

    private void Start()
    {
        if (!PersistentSettings.timer) Destroy(this.gameObject);
        if (!time_remaining) time_remaining = GetComponent<TMP_Text>();
    }

    public void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f) NextScene();

        int min = (int)(time / 60);
        int sec = (int)(time % 60);

        time_remaining.SetText($"Time Left: {min}:{sec.ToString("00")}");
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Game Over");
    }
}