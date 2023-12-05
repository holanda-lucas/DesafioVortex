using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public PersonScript[] people;
    private int currentChange = 1;

    private float timeToChangeDifficulty = 30, currentTime;

    void Start()
    {   

    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= timeToChangeDifficulty)
        {
            IncreaseDifficulty();

            currentTime = 0;
        }
    }

    void IncreaseDifficulty()
    {
        if (currentChange <= 3)
        {
            // Diminuir o tempo que demora pra mudar a dificuldade
            if (currentChange == 3)
            {
                timeToChangeDifficulty = 20;
            }

            people[currentChange].gameObject.SetActive(true);
        }
        else
        {
            int personToChangeIndex = currentChange % 4;

            people[personToChangeIndex].cooldown -= 1;
        }

        currentChange++;
    }
}
