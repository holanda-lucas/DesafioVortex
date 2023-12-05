using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    public int limitPoints, currentPoints;
    public UIVisualizer visualizer;

    public int dirtnessPoints = 0;

    private AudioSource bagSound;

    private void Update()
    {
        // Setando capacidade da bag no visualizador
        visualizer.SetText(GetUsage());

        bagSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "trash" && currentPoints < limitPoints)
        {
            bagSound.Play();

            int points = other.GetComponent<TrashScript>().points;

            Destroy(other.gameObject);

            if ((currentPoints + points) >= limitPoints)
            {
                currentPoints = limitPoints;
            } 
            else
            {
                currentPoints += points;
            }

            dirtnessPoints++;
        }
    }

    public string GetUsage()
    {
        return currentPoints.ToString() + "/" + limitPoints.ToString();
    }
}
