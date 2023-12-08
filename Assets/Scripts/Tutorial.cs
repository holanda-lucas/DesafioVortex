using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject suckerTut, trashTut, recycleTut, screensTut;
    private bool startedTrashTut = false, startedRecycleTut = false, startedScreensTut = false;

    public void StartTrashTutorial()
    {
        if (!startedTrashTut)
        {
            suckerTut.SetActive(false);
            trashTut.SetActive(true);

            startedTrashTut = true;
        }
    }

    public void StartRecycleTutorial()
    {
        if (!startedRecycleTut)
        {
            trashTut.SetActive(false);
            recycleTut.SetActive(true);

            startedRecycleTut= true;
        }
    }

    public void StartScreensTutorial()
    {
        if (!startedScreensTut)
        {
            recycleTut.SetActive(false);
            screensTut.SetActive(true);

            startedScreensTut = true;

            Invoke("StopScreensTutorial", 20);
        }
    }

    void StopScreensTutorial()
    {
        screensTut.SetActive(false);
    }
}