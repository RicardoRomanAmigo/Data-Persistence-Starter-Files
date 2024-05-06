using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData 
{
    public int TopScore;
    public string TopPlayerName;

    //Guardar un vector
    public float PosX;
    public float PosY;
    public List<SaveData> dataScores;

    //Constructor para podeer acceder a sus valores
    public SaveData()
    {
        TopScore = 0;
        TopPlayerName = " ";

        //Guardado del vector
        PosX = 1;
        PosY = 1;
    }

}


