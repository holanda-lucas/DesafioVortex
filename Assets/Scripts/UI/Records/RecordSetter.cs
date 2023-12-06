using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecordSetter : MonoBehaviour
{
    [SerializeField] private TMP_Text nameTxt, scoreTxt;

    public void SetInfo(string name, int score)
    {
        nameTxt.text = name;
        scoreTxt.text = score.ToString();
    }
}
