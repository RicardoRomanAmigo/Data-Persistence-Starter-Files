using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class SaveLoadSystem : MonoBehaviour
{
    //Constante con el nombre del archivo que se guardara y leerá
    private const string FileName = "savefile.json";

    //Referncia a los datos guardados en SaveData.cs
    [SerializeField] 
    private SaveData saveData;

    

    //Metodo para poder acceder a saveData publicamente 
    public SaveData SaveData => saveData;

    //Atributo que sirve para ejecutar el metodo desde los tres puntitos del objeto en el inspector
    [ContextMenu("Save")]
    public void Save()
    {
        //-------------------test listas--------------------
        //Convertimos la lista a string json 
        string topScoresJson = JsonUtility.ToJson(saveData.TopScores);

        //Conversion a Json nuestra saveData almacenada en SaveData.cs
        //string gameDataInfoJson = JsonUtility.ToJson(saveData); ---------------------  (test listas)

        //-------------------test listas--------------------
        //--------------------Combinamos los datos existentes con la lista
        string gameDataInfoJson = JsonUtility.ToJson(new
        {
            topScore = saveData.TopScore,
            topPlayerName = saveData.TopPlayerName,
            topScores = saveData.TopScores
        });

        //Ruta "path" donde guardar el archivo. Por defecto Application.persistentDataPath (para que funcione en la mayoria de las plataformas)
        //string path = Application.persistentDataPath + "/gamedata.json"; - Forma normal
        //Usando el metodo Path.Combine que evita usar las barras y elimina limitaciones segun dispositivos usados
        //string path = Path.Combine(Application.persistentDataPath, FileName); 
        string path = Application.persistentDataPath + FileName;

        //Guardar en el disco metodo(ruta, archivo a guardar)
        File.WriteAllText(path, gameDataInfoJson);
    }

    public void Load()
    {
        //try
        //{
            //Ruta para lectura
            string path = Path.Combine(Application.persistentDataPath, FileName);

            //Lectura del Json
            string gameDataInfoJson = File.ReadAllText(path);

            //Almacenamos en un tipo SaveData la conversion de gameDataInfoJson al tipo SaveData con el metodo fromJson de JsonUtility
            //Ponemos otro nombre al nuevo archivo temporal saveDataLoaded tipo SaveData para luego no tener cruces con la primera instancia de este tipo
            SaveData saveDataLoaded = JsonUtility.FromJson<SaveData>(gameDataInfoJson);

            //--------------------test listas--------------------------------------
            //----------------desserializamos los datos del objeto gameDataInfoJson
            //var loadedData = JsonUtility.FromJson<object>(gameDataInfoJson);

            //--------------- accedemos a los datos del objeto anonimo
            //saveData.TopScore = JsonUtility.FromJson<int>((string)loadedData.GetType().GetProperty("topScore").GetValue(loadedData, null));
            //saveData.TopPlayerName = JsonUtility.FromJson<string>((string)loadedData.GetType().GetProperty("topPlayerName").GetValue(loadedData, null));

            //----------------desserializamos la lista
            //saveData.TopScores = JsonUtility.FromJson<List<ScoreEntry>>((string)loadedData.GetType().GetProperty("topScores").GetValue(loadedData, null));

            //Ahora podemos almacenar la informacion en la antigua instancia para no duplicar
            //saveData.TopScore = saveDataLoaded.TopScore;
            //saveData.TopPlayerName = saveDataLoaded.TopPlayerName;


            //-------------------Test 2------------------------


            var loadedData = JsonUtility.FromJson<Dictionary<string, object>>(gameDataInfoJson);

            
        if (loadedData.ContainsKey("topScore"))
        {
            Debug.Log("leoTopscore");
            saveData.TopScore = JsonUtility.FromJson<int>((string)loadedData["topScore"]);
        }
        else
        {
            Debug.LogWarning("SaveLoadSystem: 'topScore' key not found in save data. Using default value.");
            saveData.TopScore = 0;
        }

        if (loadedData.ContainsKey("topPlayerName"))
        {
            saveData.TopPlayerName = JsonUtility.FromJson<string>((string)loadedData["topPlayerName"]);
        }
        else
        {
            Debug.LogWarning("SaveLoadSystem: 'topPlayerName' key not found in save data. Using default value.");
            saveData.TopPlayerName = "NoOne";
        }

        if (loadedData.ContainsKey("topScores"))
        {
            string topScoresJson = (string)loadedData["topScores"];
            saveData.TopScores = JsonUtility.FromJson<List<ScoreEntry>>(topScoresJson);
        }
        else
        {
            Debug.LogWarning("SaveLoadSystem: 'topScores' key not found in save data. Using default value.");
             saveData.TopScores = new List<ScoreEntry>();
        }

        

            MainUIManager.Instance.TopScore = saveData.TopScore;
            MainUIManager.Instance.TopPlayerName = saveData.TopPlayerName;
        //}
        //catch(Exception e)
        //{
            //Debug.LogError("SaveLoadSystem: Error loading data: " + e.Message);
        //}
    }

}
