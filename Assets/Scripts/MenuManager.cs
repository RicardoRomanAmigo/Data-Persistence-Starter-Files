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

    [SerializeField] GameObject jsonDataObject;

    

    // Start is called before the first frame update
    void Start()
    {
        
       LoadGameData();

        
    }

    //Metodo para retardar la carga hasta que se carguen los datos del JSON

    public void LoadGameData()
    {
        StartCoroutine(LoadGameDataAsync());
    }

    private IEnumerator LoadGameDataAsync()
    {
        JsonData jsonDataFile = jsonDataObject.GetComponent<JsonData>();

       jsonDataFile.LoadPlayerName();

        while (jsonDataFile.IsLoading)
        {
            yield return null;
        }
        
        ProcessGameData();
    }

    

    public void ProcessGameData()
    {
        topScoreTxt.text = "Best Score: " + MainUIManager.Instance.TopPlayerName + " " + MainUIManager.Instance.TopScore.ToString();
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
