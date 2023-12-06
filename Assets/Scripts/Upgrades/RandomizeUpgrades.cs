using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class RandomizeUpgrades : MonoBehaviour
{
    [SerializeField] private UpgradeScript up1, up2;
    private int selection1, selection2;

    private UpgradesAppeaence identifier;

    // Quantidade de cada tipo de melhora
    private int upgrades = 5, itens = 4, skills = 2;

    private int currentRandom = 0;


    private void Awake()
    {
        identifier = GetComponent<UpgradesAppeaence>();
    }
    public void RandomizeUpgradeValues()
    {
        RandomOrHelp();

        // Colocando a escolha feita em cada opção
        up1.selection = selection1;
        up2.selection = selection2;

        // Decorando corretamente cada botão (imagem, texto e cor do texto)
        identifier.SetIdentifier(selection1, up1);
        identifier.SetIdentifier(selection2, up2);

        Debug.Log(selection1 + " " + selection2);
    }
    int CalculateValue()
    {
        // Escolhe qual tipo de bônus será fornecido (upgrade, item ou skill)
        int bonusType = Random.Range(0, 100);

        if (bonusType <= 65)
        {
            return ChooseUpgrade();
        }
        else if (bonusType <= 85)
        {
            return ChooseItem();
        }
        else
        {
            return ChooseSkill();
        }
    }

    int ChooseUpgrade()
    {
        // Sortear uma habilidade indefinidamente (habilidades não possuem impedimentos)
        int choose = Random.Range(0, upgrades);

        return choose;
    }

    int ChooseItem()
    {
        int choose = Random.Range(0, itens);

        // Escolhendo entre o item em si ou uma upgrade para ele (itens podem ser melhorados caso ja tenham sido escolhidos)
        switch (choose)
        {
            case 1:
                if (GameController.haveSucker)
                {
                    return upgrades + choose + (1 + Random.Range(0, 2)) + 0;
                }
                return upgrades + choose + 0;

            case 2:
                if (GameController.haveAntenna)
                {
                    return upgrades + choose + (1) + 2;
                }
                return upgrades + choose + 2;
            case 3:
                return upgrades + choose + 3;
        }

        return choose + upgrades;
    }

    int ChooseSkill()
    {
        // Escolhe número inicial indefinidamente
        int choose = Random.Range(0, skills);

        if (choose == 0 && GameController.haveFocus)
        {
            choose = 1;
        }

        return upgrades + itens + choose + 3;
    }

    void RandomOrHelp()
    {
        // Verificar se está em algum estágio de ajuda obrigatória
        switch (currentRandom)
        {
            case 3:
                ChooseItemForHelp();
                break;
            case 6:
                ChooseItemForHelp();
                break;
            default:
                NormalRandom();
                break;
        }

        currentRandom++;
    }

    void NormalRandom()
    {
        // Escolhendo bônus aleatório para cada opção
        selection1 = CalculateValue();
        selection2 = CalculateValue();

        // Repetindo o processo até que sejam bônus diferentes
        while (selection2 == selection1)
        {
            selection2 = CalculateValue();
        }
    }

    void ChooseItemForHelp()
    {
        selection1 = ChooseItem();
        selection2 = ChooseItem();

        while (selection1 == selection2)
        {
            selection2 = ChooseItem();
        }
    }
}