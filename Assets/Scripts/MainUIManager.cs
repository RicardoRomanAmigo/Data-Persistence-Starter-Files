using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    //Guardado de datos entre escenas

    public static MainUIManager Instance;

    public string PlayerName;

    public string TopPlayerName;

    public int TopScore;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
