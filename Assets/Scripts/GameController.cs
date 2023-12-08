using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GameController : MonoBehaviour
{
    public static float attractionRadius = 10;
    public static bool haveAntenna = false, haveSucker = true, haveBag = false;

    [SerializeField] private Volume dirty;

    public static float playerSpeed = 2;
    private static DynamicMoveProvider playerMovement;


    // Identifica se o player ja possui a skill basedo no seu id
    public static bool haveFocus = false;


    public static float pollutionResistence = 40, dirtness = 0;

    [SerializeField] private Transform finalSpawnPosition;
    private bool gameOver = false;

    [SerializeField] private Records records;
    [SerializeField] private GameObject ray, rayAttatch, bag, sucker;

    public RandomizeUpgrades randomizeUpgrades;

    public GameObject[] people;
    public ScoreScript scoreScript;

    public SuckerScript suckerScript;
    public BagScript bagScript;

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
            records.scoreGot = (int)scoreScript.timePassed;
            records.scoreGotTxt.text = "Pontuação: " + records.scoreGot;
            scoreScript.passTime = false;
            RemoveAllTrash();
            
            // Retirando todos os inimigos
            foreach (GameObject person in people)
            {
                Destroy(person);
            }

            // Ativando o raio de interação com o UI
            ray.SetActive(true);
            rayAttatch.SetActive(true);

            bag.SetActive(false);
            sucker.SetActive(false);

            // Resetando variáveis
            pollutionResistence = 40;
            dirtness = 0;
            haveAntenna = false;
            haveFocus = false;
            haveBag = false;
            attractionRadius = 10;
            playerSpeed = 2;
        }
    }

    public void RemoveAllTrash()
    {
        GameObject[] trashes = GameObject.FindGameObjectsWithTag("trash");

        foreach (GameObject trash in trashes)
        {
            Destroy(trash);
        }

        suckerScript.trashesList.Clear();

        bagScript.currentPoints = 0;
        suckerScript.currentPoints = 0;

        dirtness = 0;
    }
}
