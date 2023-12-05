using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GameController : MonoBehaviour
{
    public static float attractionRadius = 10;
    public static bool haveAntenna = false, haveSucker = true
;

    [SerializeField] private Volume dirty;

    public static float playerSpeed = 2;
    private static DynamicMoveProvider playerMovement;


    // Identifica se o player ja possui a skill basedo no seu id
    public static bool haveFocus;


    public static float pollutionResistence = 40, dirtness = 0;

    [SerializeField] private Transform finalSpawnPosition;
    private bool gameOver = false;

    [SerializeField] private Records records;
    [SerializeField] private GameObject ray, rayAttatch;

    private void Start()
    {
        playerMovement = GameObject.Find("XR Origin (XR Rig)").GetComponent<DynamicMoveProvider>();
    }

    private void Update()
    {
        dirty.weight = (dirtness / pollutionResistence);

        // Habilidade de dobrar a velocidade em momentos críticos
        if (haveFocus && dirtness >= pollutionResistence / 2)
        {
            playerMovement.moveSpeed = playerSpeed * 2;
        }
        else
        {
            playerMovement.moveSpeed = playerSpeed;
        }

        // Evento de Game Over
        if (dirtness >= pollutionResistence && !gameOver)
        {
            gameOver = true;

            playerMovement.transform.position = finalSpawnPosition.position;
            records.scoreGot = (int)ScoreScript.timePassed;
            records.scoreGotTxt.text = "Pontuação: " + records.scoreGot;
            ScoreScript.passTime = false;
            RemoveAllTrash();
            
            // Retirando todos os inimigos
            GameObject[] people = GameObject.FindGameObjectsWithTag("person");
            foreach (GameObject person in people)
            {
                Destroy(person);
            }

            // Ativando o raio de interação com o UI
            ray.SetActive(true);
            rayAttatch.SetActive(true);
        }
    }

    public static void RemoveAllTrash()
    {
        GameObject[] trashes = GameObject.FindGameObjectsWithTag("trash");

        foreach (GameObject trash in trashes)
        {
            dirtness--;
            Destroy(trash);
        }
    }
}
