using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordSetter : MonoBehaviour
{
    private TMP_Text nameTxt, scoreTxt;
    void Start()
    {
        nameTxt = transform.GetChild(1).GetComponent<TMP_Text>();
        scoreTxt = transform.GetChild(2).GetComponent<TMP_Text>();
    }

    public void SetInfo(string name, int score)
    {
        nameTxt.text = name;
        scoreTxt.text = score.ToString();
    }
}
