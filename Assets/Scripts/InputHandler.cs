using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] string filename;

    List<InputEntry> entries = new List<InputEntry>();

    private void Start()
    {
        entries = FileHandler.ReadFromJSON<InputEntry>(filename);

        SortList(entries);

        //Debug for the list
        foreach (var entry in entries)
        {
            if (entry is InputEntry) // Ensure it's an InputEntry object
            {
                InputEntry inputEntry = (InputEntry)entry;
                Debug.Log($"Player: {inputEntry.playerName}, Points: {inputEntry.points}");
            }
            else
            {
                Debug.LogWarning($"Entry type not handled: {entry.GetType().Name}");
            }
        }
    }

    public void AddNameToList(string name, int number)
    {
        entries.Add(new InputEntry(name, number));

        FileHandler.SaveToJSON<InputEntry>(entries,filename);
    }

    public void SortList(List<InputEntry> listName)
    {
        listName.Sort((a, b) => ((a.points).CompareTo(b.points)));
    }

    public void LimitToTen()
    {
        if(entries.Count > 10) 
        {
            entries.RemoveAt(entries.Count -1);
        }
    }
}
