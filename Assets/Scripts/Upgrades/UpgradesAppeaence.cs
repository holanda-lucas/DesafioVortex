using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesAppeaence : MonoBehaviour
{
    public Sprite[] sprites;
    public string[] names;


    public void SetIdentifier(int selection, UpgradeScript up)
    {
        Image image = up.transform.GetChild(0).gameObject.GetComponent<Image>();
        TMP_Text text = up.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();

        // Ajustar nome e �cone do b�nus
        image.sprite = sprites[selection];
        text.text = names[selection];


        // Ajustar cor do texto dependendo da raridade do b�uns
        if (selection <= 4)
        {
            text.color = Color.blue;
        }
        else if (selection <= 11)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.yellow;
        }
    }
}
