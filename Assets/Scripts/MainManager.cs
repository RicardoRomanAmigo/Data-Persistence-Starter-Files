using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;


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

    SaveLoadSystem saveLoadSystem;

    InputHandler inputHandler;
    

    

    // Start is called before the first frame update
    void Start()
    {
        //saveLoadSystem = GameObject.FindObjectOfType<SaveLoadSystem>();
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
            }
        }

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
        BetterScore();

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
            ScoreTextTop.text = "Best Score: " + playerName + ": " + m_Points;
            
        }
    }

}
