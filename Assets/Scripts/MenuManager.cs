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

    [SerializeField] SaveLoadSystem saveLoadSystem;

    // Start is called before the first frame update
    void Start()
    {
       
        //LoadGameData();  
        
        MainUIManager.Instance.TopPlayers = FileHandler.ReadFromJSON<InputEntry>("topScore.json");

        ProcessGameData();

        //Debug.Log(MainUIManager.Instance.TopPlayers);
    }
    
    public void ProcessGameData()
    {
        topScoreTxt.text = "Best Score: " + MainUIManager.Instance.TopPlayerName + " " + MainUIManager.Instance.TopScore.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);

        if(playerName != null) {

            NewNameSelected(playerName);
        }
    }

    public void HighScore()
    {
        SceneManager.LoadScene(2);
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
 
}
