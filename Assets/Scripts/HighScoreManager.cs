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

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toMenu.GoToMenu();
        }
        
    }

    
    
}
