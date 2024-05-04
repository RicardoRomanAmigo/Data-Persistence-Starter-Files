using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Mas adelante comprobar nombres con el metodo Contains y asi no duplicar

public class ScoreEntryS : MonoBehaviour
{
    private List<ScoreEntry> scoreEntries = new List<ScoreEntry>();

    private int position;

    // Start is called before the first frame update
    void Start()
    {
        position = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class ScoreEntry
    {
        public string Name { get; set; }
        public int Score { get; set; }

        
    }




    

    //Comparamos el score para saber si es mejor que alguno de la lista 
    private bool CheckScores(int score)
    {

        for(int i = 0;i<scoreEntries.Count;i++)
        {
            if (scoreEntries[i].Score < score)
            {
                return true;
            }
        }
        return false;
    }

    //Ordenamnos la lista
    private void ListOrder()
    {
        scoreEntries = (List<ScoreEntry>)scoreEntries.OrderBy(x => x.Score);
    }

    //Borramos el ultimo elemento de la lista
    private void DeleteLastScore()
    {
        scoreEntries.RemoveAt(scoreEntries.Count - 1);
    }

    
}
