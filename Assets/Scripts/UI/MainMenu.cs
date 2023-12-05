using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainOptions, credits;

    public void StartGame()
    {
        SceneManager.LoadScene("Story");
    }

    public void Credits()
    {
        mainOptions.SetActive(false);
        credits.SetActive(true);
    }

    public void Back()
    {
        mainOptions.SetActive(true);
        credits.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
