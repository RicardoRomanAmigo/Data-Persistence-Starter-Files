using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;


public class MenuManager : MonoBehaviour
{
    [SerializeField] Text playerName;
    [SerializeField] Text topScoreTxt;
    [SerializeField] Text alertText;
    [SerializeField] Animator panelTopPlayer;
    [SerializeField] GameObject audioSourcePrefab;

    // Start is called before the first frame update
    void Start()
    {
        alertText.text = "";

        MainUIManager.Instance.TopPlayers = FileHandler.ReadFromJSON<InputEntry>("topScore.json");

        ProcessGameData();

        TopPanelDown();

        MainUIManager.Instance.AudioSourcePrefab = audioSourcePrefab;
    }
    
    public void ProcessGameData()
    {
        topScoreTxt.text = "Name: " + MainUIManager.Instance.TopPlayerName + " - Score: " + MainUIManager.Instance.TopScore.ToString();
    }

    public void StartGame()
    {
        if(playerName != null && playerName.text.Length >= 3) {

            NewNameSelected(playerName);
            SceneManager.LoadScene(2);
        }
        else
        {
            playerName.text = "";
            alertText.text = "* minimum name length are 3 characters.";
        }
    }

    public void HighScore()
    {
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void NewNameSelected(Text text)
    {        
            MainUIManager.Instance.PlayerName = text.text;
    }

    public void DeleteAlert()
    {
        if(alertText != null)
        {
            alertText.text = "";
        }
        
    }

    void TopPanelDown()
    {
        panelTopPlayer.Play("PanelTopPlayer");
    }
}
