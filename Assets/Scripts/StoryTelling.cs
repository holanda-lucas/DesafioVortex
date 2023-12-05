using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StoryTelling : MonoBehaviour
{
    [SerializeField] private GameObject musicObj, title, story;
    [SerializeField] private Animator fadeAnim;

    void Start()
    {
        Invoke("StartMusic", 8);
        Invoke("Fade", 58);
    }

    void StartMusic()
    {
        musicObj.SetActive(true);
        Invoke("StartTitle", 1);
    }

    void StartTitle()
    {
        title.SetActive(true);
        //Invoke("StartStory", 1);
        story.SetActive(true);
    }

    void StartStory()
    {
        story.SetActive(true);
    }

    void Fade()
    {
        fadeAnim.SetTrigger("swap");
        Invoke("ChangeScene", 1);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
