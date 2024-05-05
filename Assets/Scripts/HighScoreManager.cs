using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    SaveLoadSystem saveLoadSystem;

    [SerializeField] GameObject linePos;
    [SerializeField] GameObject linePrefab;
    Text lineText;

    [SerializeField] ToMenu toMenu;


    private void Start()
    {
        saveLoadSystem = GameObject.FindObjectOfType<SaveLoadSystem>();

        StartCoroutine(LinesInstance());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toMenu.GoToMenu();
        }
        
    }

    IEnumerator LinesInstance()
    {
        ScoreEntry newEntry = new ScoreEntry();
        foreach (ScoreEntry entry in saveLoadSystem.SaveData.TopScores)
        {
            lineText.text = entry.Name + ": " + entry.Score;
            Instantiate(linePrefab, linePos.transform.position, Quaternion.identity);
            linePrefab.gameObject.GetComponent<Text>().text = lineText.text;
        }

        yield return new WaitForSeconds(2);
    }
    
}
