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

    

    // Start is called before the first frame update
    void Start()
    {
        GameObject mainManagerObject = GameObject.Find("MainUIManager");
        if (mainManagerObject != null)
        {
            MainManager mainManager = mainManagerObject.GetComponent<MainManager>();

            mainManager.LoadPlayerName();

        }

        if(MainUIManager.Instance != null )
        {
            
            topScoreTxt.text = "Best Score: " + MainUIManager.Instance.TopPlayerName + " " + MainUIManager.Instance.TopScore.ToString();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);

        if(playerName != null) {

            NewNameSelected(playerName);
        }
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
