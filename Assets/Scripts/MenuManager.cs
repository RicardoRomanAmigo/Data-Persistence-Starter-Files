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
    
    // Start is called before the first frame update
    void Start()
    {
        MainUIManager.Instance.IsPaused = false;    

        alertText.text = "";

        MainUIManager.Instance.TopPlayers = FileHandler.ReadFromJSON<InputEntry>("topScore.json");

        ProcessGameData();

        TopPanelDown();

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
        //#if UNITY_EDITOR
        //EditorApplication.ExitPlaymode();
        //#else
        //Application.Quit();
        //#endif

#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
    Application.Quit();
#elif (UNITY_WEBGL)
    Application.OpenURL("https://play.unity.com/mg/other/webgl-s7r");
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
