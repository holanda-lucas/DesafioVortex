using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIVisualizer : MonoBehaviour
{
    private Transform cameraTR;

    [SerializeField] private TMP_Text text;
    [SerializeField] private Transform textTR;

    public bool show = true;

    void Start()
    {
        cameraTR = GameObject.Find("Main Camera").transform;

        if (textTR == null)
        {
            textTR = text.transform;
        }
    }

    void Update()
    {
        textTR.LookAt(2 * textTR.position - cameraTR.position);
    }

    public void SetText(string content)
    {
        if (show)
        {
            text.text = content;
        }
        else
        {
            text.text = "";
        }
    }
}
