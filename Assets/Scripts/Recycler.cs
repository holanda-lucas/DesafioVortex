using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;


public class Recycler : MonoBehaviour
{
    private int currentPoints;
    public int pointsToReward;
    [SerializeField] private TMP_Text visor, pollutionLevel;


    private BagScript bag;

    [SerializeField] private XRLever lever;

    [SerializeField] private RandomizeUpgrades randomizeUpgrades;

    public double cooldown = 2;
    private float timePassed = 0;
    private bool countingTime = false;

    [SerializeField] private RectTransform recycleBar;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Animator doorAnim;

    private AudioSource doorSound;

    private void Start()
    {
        bag = GameObject.Find("Bag").transform.GetChild(0).GetComponent<BagScript>();
        doorAnim.SetBool("IsOpen", true);
        doorSound = transform.parent.GetComponent<AudioSource>();
    }
    private void Update()
    {
        visor.text = GetUsage();
        pollutionLevel.text = GetPollutionLevel();


        if (lever.value)
        {
            if (currentPoints == pointsToReward)
            {
                countingTime = true;
            }
            else
            {
                lever.value = false;
            }
        }
        else
        {
            countingTime = false;
            timePassed = 0;
        }

        if (countingTime)
        {
            if (timePassed < cooldown)
            {
                timePassed += Time.deltaTime;
            }
            else
            {
                lever.value = false;
                timePassed = 0;
                countingTime = false;
                currentPoints = 0;

                GiveReward();
                pollutionLevel.transform.parent.gameObject.SetActive(false);
            }
        }

        // Fechar a porta quando o recycler atingir o limite
        if (doorAnim.GetBool("IsOpen") && pointsToReward == currentPoints)
        {
            doorAnim.SetBool("IsOpen", false);
            doorSound.Play();
        }

        // Ajustando o tamanho da barra para ficar condizente com o tempo de reciclagem
        recycleBar.localScale = new Vector3(0.06f, (float)((timePassed/cooldown)*0.25f), 1);
    }


    public void StartRecycling()
    {
        countingTime = true;
    }

    private string GetUsage()
    {
        return currentPoints.ToString() + "/\n" + pointsToReward.ToString();
    }

    private string GetPollutionLevel()
    {
        return GameController.dirtness + "/" + GameController.pollutionResistence;
    }

    private void GiveReward()
    {
        randomizeUpgrades.transform.parent.gameObject.SetActive(true);
        randomizeUpgrades.RandomizeUpgradeValues();

        pointsToReward += 2;
    }


    // Recebe os lixos e acumula pontos
    private void OnTriggerEnter(Collider other)
    {
        if (currentPoints < pointsToReward)
        {
            if (other.transform.tag.Equals("trash"))
            {
                int trashPoints = other.GetComponent<TrashScript>().points;

                if (currentPoints + trashPoints < pointsToReward)
                {
                    currentPoints += trashPoints;
                }
                else
                {
                    currentPoints = pointsToReward;

                }

                Destroy(other.gameObject);
                GameController.dirtness -= 1;
            }
            else if (other.transform.tag.Equals("bag"))
            {
                int bagPoints = bag.currentPoints;

                if ((currentPoints + bagPoints) > pointsToReward)
                {
                    int pointsRemaining = currentPoints + bagPoints - pointsToReward;
                    bag.currentPoints = pointsRemaining;
                    currentPoints = pointsToReward;
                }
                else
                {
                    currentPoints += bagPoints;
                    bag.currentPoints = 0;
                    doorAnim.SetBool("IsOpen", false);
                }

                bag.transform.parent.transform.position = spawnPoint.position;
                GameController.dirtness -= bag.dirtnessPoints;
                bag.dirtnessPoints = 0;
            }
            else if (other.transform.tag.Equals("sucker"))
            {
                other.transform.position = spawnPoint.position;
            }
        }
    }
}
