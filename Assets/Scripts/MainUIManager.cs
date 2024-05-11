using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainUIManager : MonoBehaviour
{
    //Guardado de datos entre escenas

    public static MainUIManager Instance;

    public string PlayerName;

    public string TopPlayerName;

    public int TopScore;

    public List<InputEntry> TopPlayers = new List<InputEntry>();

    public bool MusicOn;

    public float MusicPauseTime;

    public GameObject AudioSourcePrefab;

    public bool IsPaused;

    public int LevelPoints;


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
