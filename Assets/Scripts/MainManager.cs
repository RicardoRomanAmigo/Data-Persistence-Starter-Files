using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;


public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    [SerializeField] Text ScoreText;
    [SerializeField] Text ScoreTextTop;
    public GameObject GameOverText;
    public GameObject GameOverPanel;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private string playerName;
    private int topScore;
    private string topPlayerName;


    InputHandler inputHandler;

    AudioSource musicPlayer;

    float mainVolume = 0.3f;

    //Once the player brakes the record
    bool betterScore = false;
    [SerializeField] AudioClip scoreBrake;
    [SerializeField] AudioSource audioManager;

    //AudioManager audioManagerScript;
    [SerializeField] AudioClip overBtn;
    [SerializeField] AudioClip pressBtn;

    //Pause
    [SerializeField] GameObject pausePanel;

    int totalLevelPoints = 0;

    //Victory
    [SerializeField] GameObject victoryPanel;
    [SerializeField] AudioClip victorySound;
    


    // Start is called before the first frame update
    void Start()
    {
        MainUIManager.Instance.LevelPoints = totalLevelPoints;
        m_GameOver = false;
        m_Started = false;

       // audioManagerScript = new AudioManager();

       Debug.Log(MainUIManager.Instance.IsPaused);

        MusicVolume(mainVolume);

        inputHandler = GameObject.FindObjectOfType<InputHandler>();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                PointsCalculator(brick.PointValue);
            }
        }
        Debug.Log(totalLevelPoints);

        //Recuperacion datos para escena
        if(MainUIManager.Instance != null)
        {
            playerName = MainUIManager.Instance.PlayerName;
            topPlayerName = MainUIManager.Instance.TopPlayerName;
            topScore = MainUIManager.Instance.TopScore;

            
            ScoreTextTop.text = "Best Score: " + topPlayerName +   ": " + topScore;
            ScoreText.text = "Score " + playerName + " : 0" ; 
        }
    }

    private void Update()
    {
        PauseGame();

        BetterScore();

        StartCoroutine(VictoryPanel());

        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                /*JsonData jsonDataFile = jsonDataObject.GetComponent<JsonData>();
                jsonDataFile.SaveName(0, "None");*/
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                RestardGame();
            }
        }
       
        

    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score {playerName} : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        GameOverPanel.SetActive(true);

        

            if (inputHandler != null)
            {               
                //saveLoadSystem.Save();
                inputHandler.AddNameToList(playerName, m_Points);              
            }
            else
            {
                Debug.LogError("JsonDataFile is null");
            } 
    }

    void RestardGame()
    {
        if (MainUIManager.Instance != null)
        {
            playerName = MainUIManager.Instance.PlayerName;
            topPlayerName = MainUIManager.Instance.TopPlayerName;
            topScore = MainUIManager.Instance.TopScore;


            ScoreTextTop.text = "Best Score: " + topPlayerName + ": " + topScore;
            ScoreText.text = "Score " + playerName + " : 0";


        }
    }

    void BetterScore()
    {
        if (m_Points > MainUIManager.Instance.TopScore)
        {
            if(betterScore == false)
            {
                BetterScoreSound();
                Debug.Log("SonidoMejor!");
            }
            
            betterScore = true;

            ScoreTextTop.text = "Best Score: " + playerName + ": " + m_Points;
            
        }
    }

    void MusicVolume(float level)
    {
        musicPlayer = GameObject.FindGameObjectWithTag("MainContainer").transform.GetComponent<AudioSource>();
        musicPlayer.volume = level;
    }

    

    void BetterScoreSound()
    {
        if(audioManager != null) 
        {
            audioManager.volume = 0.5f;
            audioManager.PlayOneShot(scoreBrake);
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

    public void PlaySoundOverBtn()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(overBtn);
        }
    }

    public void PlaySoundPressBtn()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(pressBtn);
        }
    }

    public void PauseGame()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(!MainUIManager.Instance.IsPaused)
            {
                if(pausePanel != null)
                {
                    Time.timeScale = 0f;
                    pausePanel.SetActive(true);
                    MainUIManager.Instance.IsPaused = true;
                }
            }
            
        }
    }

    public void ResumeGameEsc()
    {
        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("ResumEsc");
            Debug.Log(MainUIManager.Instance.IsPaused);
            if (MainUIManager.Instance.IsPaused == true)
            {
                if (pausePanel != null)
                {
                    Time.timeScale = 1f;
                    pausePanel.SetActive(false);
                    MainUIManager.Instance.IsPaused = false;
                }
            }

        }
            
    }

    public void ResumeGame()
    {
        if (MainUIManager.Instance.IsPaused)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            MainUIManager.Instance.IsPaused = false;
        }
    }

    void PointsCalculator(int points)
    {
        MainUIManager.Instance.LevelPoints = totalLevelPoints + points;
    }

    IEnumerator VictoryPanel()
    {
        if (m_Points >= MainUIManager.Instance.LevelPoints)
        {
            m_Points = 0;
            Destroy(Ball);
            WinSound();
            victoryPanel.SetActive(true);


            yield return new WaitForSeconds(2);

            //Time.timeScale = 0f;
            //MainUIManager.Instance.IsPaused = true;
            
        }

            
    }

    void WinSound()
    {
        Debug.Log("entro");
        if (audioManager != null)
        {
            audioManager.volume = 0.8f;
            audioManager.PlayOneShot(victorySound);
        }
    }
}
