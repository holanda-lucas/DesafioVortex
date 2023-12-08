using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class SuckerScript : MonoBehaviour
{
    public int limitPoints, currentPoints;
    public UIVisualizer visualizer;

    [SerializeField] private ParticleSystem particles;


    [SerializeField] private GameObject[] trashesPrefabs;
    public List<GameObject> trashesList;

    public float attractionForce;
    [SerializeField] private Transform outPoint;
    [SerializeField] GameObject suckArea, inArea;
    private bool shooting = false;

    [SerializeField] private Transform rangeArea;


    public InputActionProperty inputRight, inputLeft;
    private int handState = 0;
    private float goingOut;

    [SerializeField] private AudioSource suckingSound, outSound;

    [SerializeField] private Tutorial tutorial;

    private void Start()
    {
        trashesList = new List<GameObject>();

        // Criando o Interactable
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.activated.AddListener(x => StartSucking());
        grabInteractable.deactivated.AddListener(x => StopSucking());
    }
    private void Update()
    {
        // Setando capacidade da bag no visualizador
        visualizer.SetText(GetUsage());


        // Ativando o input para a mão correspondente
        switch (handState)
        {
            case 0:
                goingOut = 0;
                break;
            case 1:
                goingOut = inputRight.action.ReadValue<float>();
                break;
            case 2:
                goingOut = inputLeft.action.ReadValue<float>();
                break;
        }

        if (goingOut > 0 && !shooting)
        {
            StartCoroutine(Shoot());
            shooting = true;
        }
        else if (goingOut == 0 && shooting)
        {
            StopAllCoroutines();
            shooting = false;
        }
    }

    public void EnterTrash(Collider other)
    {
        if (other.tag == "trash" && currentPoints < limitPoints)
        {
            int points = other.GetComponent<TrashScript>().points;

            AddTrashToStorage(other.GetComponent<TrashScript>().points);

            Destroy(other.gameObject);

            if ((currentPoints + points) >= limitPoints)
            {
                currentPoints = limitPoints;
            }
            else
            {
                currentPoints += points;
            }
        }
    }

    public string GetUsage()
    {
        return currentPoints.ToString() + "/" + limitPoints.ToString();
    }

    private void AddTrashToStorage(int pointsToEarn)
    { 
        switch (pointsToEarn) 
        {
            case 5:
                trashesList.Add(trashesPrefabs[0]);
                break;
            case 10:
                trashesList.Add(trashesPrefabs[1]);
                break;
            case 20:
                trashesList.Add((trashesPrefabs[2]));
                break;
            case 40:
                trashesList.Add(trashesPrefabs[3]);
                break;
        }
    }

    public void StartSucking()
    {
        particles.Play();
        suckArea.SetActive(true);
        inArea.SetActive(true);
        suckingSound.Play();
    }

    public void StopSucking()
    {
        particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        suckArea.SetActive(false);
        inArea.SetActive(false);
        suckingSound.Stop();
    }

    IEnumerator Shoot()
    {
        while (trashesList.Count > 0)
        {
            GameObject trash = trashesList[trashesList.Count - 1];
            int trashPoints = trash.GetComponent<TrashScript>().points;
            trashesList.RemoveAt(trashesList.Count - 1);

            // Evitando que o valor da arma fique negativo
            if (currentPoints - trashPoints >= 0)
            {
                currentPoints -= trashPoints;
            }
            else
            {
                currentPoints = 0;
            }

            trash = Instantiate(trash, outPoint.position, outPoint.rotation);
            trash.GetComponent<Rigidbody>().AddForce(outPoint.forward * 10, ForceMode.Impulse);

            outSound.Play();

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void UpgradeRange()
    {
        var mainModule = particles.main;
        mainModule.startLifetimeMultiplier *= 1.2f;

        rangeArea.localScale = new Vector3(rangeArea.localScale.x, rangeArea.localScale.y, rangeArea.localScale.z * 1.2f);
    }

    public void UpgradeForce()
    {
        attractionForce += 1.5f;
    }

    // Controlar qual mão está segurando o objeto
    enum Hands
    {
        None,
        RightInteractor,
        LeftInteractor
    }
    public void EnterEvent(SelectEnterEventArgs ev)
    {
        string name = ev.interactorObject.transform.name;

        Hands enumSearch = (Hands)Enum.Parse(typeof(Hands), name);
        handState = (int)enumSearch;

        tutorial.StartTrashTutorial();
    }

    public void ExitEvent(SelectExitEventArgs ev)
    {
        handState = 0;
    }
}
