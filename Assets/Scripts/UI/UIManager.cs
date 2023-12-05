using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Transform cameraTR;

    public TMP_Text pointsCounter;
    private Transform pointsTR;

    // PLACEHOLDER
    public BagScript bag;

    void Start()
    {
        pointsTR = pointsCounter.transform;
        cameraTR = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        // PLACEHOLDER
        pointsCounter.text = bag.GetUsage();
        pointsTR.LookAt(2 * pointsTR.position - cameraTR.position);
    }
}
