using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Records : MonoBehaviour
{
    private string url = "https://apizeus.holanda-lucas.repl.co/";

    private KeyValueList rank;

    [SerializeField] private RecordSetter[] records;
    [SerializeField] private TMP_InputField input;
    public TMP_Text scoreGotTxt;

    [SerializeField] GameObject gameOverScreen, rankScreen;

    public int scoreGot = 0;

    // Chamar api com o top 5
    private IEnumerator ObterDadosDaAPI()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + "records"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Erro na requisi��o: " + www.error);
            }
            else
            {
                // A resposta est� em www.downloadHandler.text, agora vamos desserializar
                rank = JsonUtility.FromJson<KeyValueList>(www.downloadHandler.text);

                ShowRecord();
            }
        }
    }

    private void ShowRecord()
    {
        for (int i = 0; i < rank.rank.Count; i++)
        {
            string key = rank.rank[i].Key.Replace('_', ' ');
            int value = rank.rank[i].Value;

            records[i].SetInfo(key, value);
        }
    }

    // Fun��o para chamar a API
    IEnumerator PostValue()
    {
        string name = input.text;
        name.Replace(' ', '_');

        string authKey = "5e7d7e6c-8fdc-11ee-b9d1-0242ac120002";

        // Cria uma requisi��o web
        UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url + name + "/" + scoreGot + "/" + authKey, "");

        // Envia a requisi��o e espera pela resposta
        yield return webRequest.SendWebRequest();

        // Verifica se ocorreu algum erro
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro na requisi��o: " + webRequest.error);
        }
        else
        {
            // A resposta da API est� dispon�vel em webRequest.downloadHandler.text
            Debug.Log("Resposta da API: " + webRequest.downloadHandler.text);
        }

        // Mostrar o record na tela
        StartCoroutine(ObterDadosDaAPI());
    }

    // Voids dos botões
    public void PostRecord()
    {
        StartCoroutine(PostValue());

        // Coloca a tela certa no canvas
        gameOverScreen.SetActive(false);
        rankScreen.SetActive(true);
    }

    public void Skip()
    {
        StartCoroutine(ObterDadosDaAPI());

        // Coloca a tela certa no canvas
        gameOverScreen.SetActive(false);
        rankScreen.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
