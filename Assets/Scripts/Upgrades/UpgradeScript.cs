using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class UpgradeScript : MonoBehaviour
{
    public int selection;

    [SerializeField] private Recycler recycler;
    [SerializeField] private BagScript bag;
    [SerializeField] private SuckerScript sucker;
    [SerializeField] private GameObject antenna;
    [SerializeField] private GameObject bombPack;

    [SerializeField] private Transform spawnPosition;

    [SerializeField] private Animator doorAnim;
    [SerializeField] private GameObject pollutionLevel;

    public void UpgradePlayer()
    {
        switch (selection)
        {
            // Upgrades
            case 0:
                PollutionResistanceUpgrade();
                break;
            case 1:
                RecycleSpeedUpgrade();
                break;
            case 2:
                MoveSpeedUpgrade();
                break;
            case 3:
                CapacityUpgrade();
                break;
            case 4:
                LuckUpgrade();
                break;

            // Itens
            case 5:
                Bag();
                break;
            case 6:
                Sucker();
                break;
            case 7:
                SuckerForceUpgrade();
                break;
            case 8:
                SuckerRangeUpgrade();
                break;
            case 9:
                Antenna();
                break;
            case 10:
                AntennaRadiusUpgrade();
                break;
            case 11:
                BoogieBomb();
                break;

            // Skills
            case 12:
                Focus();
                break;
            case 13:
                FastClean();
                break;
        }

        doorAnim.SetBool("IsOpen", true);
        transform.parent.gameObject.SetActive(false);
        pollutionLevel.SetActive(true);
    }

    // Upgrades

    void PollutionResistanceUpgrade()
    {
        GameController.pollutionResistence += 10;
    }

    void RecycleSpeedUpgrade()
    {
        recycler.cooldown *= 0.75;
    }

    void MoveSpeedUpgrade()
    {
        GameController.playerSpeed += 0.25f;
    }

    void CapacityUpgrade()
    {
        bag.limitPoints += 10;
        sucker.limitPoints += 10;
    }

    void LuckUpgrade()
    {
        PersonScript.luck += 8;
    }



    // Itens e Upgrade de Itens

    void Bag()
    {
        bag.transform.parent.position = spawnPosition.position;
    }



    void Sucker()
    {
        sucker.transform.position = spawnPosition.position;
        GameController.haveSucker = true;
    }

    void SuckerRangeUpgrade()
    {
        sucker.UpgradeRange();
    }

    void SuckerForceUpgrade()
    {
        sucker.UpgradeForce();
    }



    void Antenna()
    {
        antenna.SetActive(true);
        GameController.haveAntenna = true;
    }

    void AntennaRadiusUpgrade()
    {
        GameController.attractionRadius -= 1;
    }

    void BoogieBomb()
    {
        Instantiate(bombPack, spawnPosition.position, Quaternion.identity);
    }

    // Skills

    void Focus()
    {
        GameController.haveFocus = true;
    }

    void FastClean()
    {
        GameController.RemoveAllTrash();
    }
}
