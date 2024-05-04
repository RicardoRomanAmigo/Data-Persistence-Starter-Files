using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListHandler : MonoBehaviour
{
    private string filename;

    List<InputEntry> entries = new List<InputEntry>();

    public void AddNameToList(string name, int score)
    {
        entries.Add(new InputEntry(name, score));

        FileHandler.SaveToJSON<InputEntry>(entries,filename);
    }
}
// https://www.youtube.com/watch?v=KZft1p8t2lQ