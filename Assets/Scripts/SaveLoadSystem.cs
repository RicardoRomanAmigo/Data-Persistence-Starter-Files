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

        
        //Conversion a Json nuestra saveData almacenada en SaveData.cs
        string gameDataInfoJson = JsonUtility.ToJson(saveData);
        Debug.Log(gameDataInfoJson);
        

        //Ruta "path" donde guardar el archivo. Por defecto Application.persistentDataPath (para que funcione en la mayoria de las plataformas)
        //string path = Application.persistentDataPath + "/gamedata.json";
        //Usando el metodo Path.Combine que evita usar las barras y elimina limitaciones segun dispositivos usados
        string path = Path.Combine(Application.persistentDataPath, FileName); 

        //Guardar en el disco metodo(ruta, archivo a guardar)
        File.WriteAllText(path, gameDataInfoJson);
    }

    public void Load()
    {
        
            //Ruta para lectura
            string path = Path.Combine(Application.persistentDataPath, FileName);

            //Lectura del Json
            string gameDataInfoJson = File.ReadAllText(path);

            //Almacenamos en un tipo SaveData la conversion de gameDataInfoJson al tipo SaveData con el metodo fromJson de JsonUtility
            //Ponemos otro nombre al nuevo archivo temporal saveDataLoaded tipo SaveData para luego no tener cruces con la primera instancia de este tipo
            SaveData saveDataLoaded = JsonUtility.FromJson<SaveData>(gameDataInfoJson);

            //Ahora podemos almacenar la informacion en la antigua instancia para no duplicar
            saveData.TopScore = saveDataLoaded.TopScore;
            saveData.TopPlayerName = saveDataLoaded.TopPlayerName;

            MainUIManager.Instance.TopPlayerName= saveDataLoaded.TopPlayerName;
            MainUIManager.Instance.TopScore= saveDataLoaded.TopScore;
    }

}
