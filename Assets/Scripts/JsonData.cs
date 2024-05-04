using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using System.IO;

public class JsonData : MonoBehaviour
{
    GameObject mainManagerObject;
    private bool isLoading;
    private int points;
    private string playerName;
    


    //Continuidad de datos entre sesiones

    [System.Serializable]
    class SaveData
    {
        public string TopPlayerName;
        public int TopScore;
        //Lista records
        public List<ScoreEntryS> HighScores;
     
    }

    

    public void SaveName(int points, string name)
    {
        SaveData data = new SaveData();
        data.TopPlayerName = name;
        data.TopScore = points;
        

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            //Inicion carga 
            isLoading = true;
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (MainUIManager.Instance != null)
            {
                MainUIManager.Instance.TopPlayerName = data.TopPlayerName;
                MainUIManager.Instance.TopScore = data.TopScore;
            }
            else
            {
                Debug.LogError("MainUIManager instance not found!");
            }
            isLoading = false;
        }
    }

    public bool IsLoading
    {
        get { return isLoading; }
    }


    
}
