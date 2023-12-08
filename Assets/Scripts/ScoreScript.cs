using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private TMP_Text visor;
    public float timePassed;
    public bool passTime = true;

    void Awake()
    {
        visor = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (passTime)
        {
            timePassed += Time.deltaTime;

            visor.text = GetTime();
        }
    }

    string GetTime()
    {
        int minutes = (int)timePassed / 60;
        int seconds = (int)timePassed % 60;

        return $"{minutes:D2}:{seconds:D2}";
    }
}
