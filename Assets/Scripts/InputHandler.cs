using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] string filename;

    List<InputEntry> entries = new List<InputEntry>();

    private void Start()
    {
        entries = FileHandler.ReadFromJSON<InputEntry>(filename);

        MainUIManager.Instance.TopPlayers = entries;

        SortList(entries);

        TopPlayer();

        //Debug for the list
        foreach (var entry in MainUIManager.Instance.TopPlayers)
        {
            if (entry is InputEntry) // Ensure it's an InputEntry object
            {
                InputEntry inputEntry = (InputEntry)entry;
                
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

        SortList(entries);

        LimitToTen();

        TopPlayer() ;

        MainUIManager.Instance.TopPlayers = entries;

        FileHandler.SaveToJSON<InputEntry>(entries,filename);
    }

    public void SortList(List<InputEntry> listName)
    {
        listName.Sort((a, b) => ((b.points).CompareTo(a.points)));
    }

    public void LimitToTen()
    {
        if(entries.Count >= 10) 
        {
            entries.RemoveAt(entries.Count -1);
        }
    }

    public void TopPlayer()
    {
        MainUIManager.Instance.TopPlayerName = entries.FirstOrDefault().playerName;
        MainUIManager.Instance.TopScore = entries.FirstOrDefault().points;
    }
}
