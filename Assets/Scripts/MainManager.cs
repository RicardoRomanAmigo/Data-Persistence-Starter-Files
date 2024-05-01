using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    [SerializeField] Text ScoreText;
    [SerializeField] Text ScoreTextTop;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private string playerName;
    private int topScore;
    private string topPlayerName;

    private void Awake()
    {
        LoadPlayerName();
    }

    // Start is called before the first frame update
    void Start()
    {
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

        //Continuacion de datos entre escenas
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

        if(m_Points > MainUIManager.Instance.TopScore)
        {
            SaveName();
        }
    }

    //Continuidad de datos entre sesiones

    [System.Serializable]
    class SaveData
    {
        public string TopPlayerName;
        public int TopScore;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.TopPlayerName = playerName;
        data.TopScore = m_Points;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        
        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (MainUIManager.Instance !=null)
            {
                MainUIManager.Instance.TopPlayerName = data.TopPlayerName;
                MainUIManager.Instance.TopScore = data.TopScore;
            }
            else
            {
                Debug.LogError("MainUIManager instance not found!");
            }

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

}
