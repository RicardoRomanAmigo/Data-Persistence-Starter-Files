using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    

    
    [SerializeField] RectTransform linePos;
    
    [SerializeField] GameObject linePrefab;
    
    [SerializeField] GameObject canvasParent;
    Text lineText;
    float posY = 0;
    
    float topBorder = 450.0f;
    float moveSpeed = 1f;

    [SerializeField] ToMenu toMenu;
    

    private void Start()
    {
        posY = 0;
        lineText = linePrefab.GetComponent<Text>();
        
        ScoresUp();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toMenu.GoToMenu();
        } 
    }

    
    private void ScoresUp()
    {
        //List<InputEntry> reversedList = SortListinvers(MainUIManager.Instance.TopPlayers);
        

        foreach (var entry in MainUIManager.Instance.TopPlayers)
        {
            Vector3 spawnLocation = new Vector3(linePos.localPosition.x, linePos.localPosition.y, linePos.localPosition.z);

            GameObject newLine = Instantiate(linePrefab, spawnLocation, Quaternion.identity);

            newLine.transform.SetParent(canvasParent.transform, false);

            RectTransform newlineTransform = newLine.GetComponent<RectTransform>();

            Vector3 newLinePosition = new Vector3(spawnLocation.x, spawnLocation.y - posY, spawnLocation.z);

            newlineTransform.localPosition = newLinePosition;

            Text newLineName = newLine.transform.GetChild(0).GetComponent<Text>();
            Text newLineScore = newLine.transform.GetChild(1).GetComponent<Text>();

            newLineName.text = $"Player: {entry.playerName}";
            newLineScore.text = $"Score: {entry.points}";

            //StartCoroutine(MoveUp(newLine));

            posY += 40;
        }
    }

    IEnumerator MoveUp(GameObject objeto)
    {
        while (objeto.transform.position.y < topBorder)
        {
            objeto.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            yield return null;
        }

        //Destroy(objeto);
    }

    public List<T> SortListinvers<T>(List<T> originalList)
    {
        List<T> reversedList = new List<T>();
        for (int i = originalList.Count - 1; i >= 0; i--)
        {
            reversedList.Add(originalList[i]);
        }
        return reversedList;
    }
}
