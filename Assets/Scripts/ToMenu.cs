using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour
{
   public void GoToMenu()
    {
        

        if(MainUIManager.Instance != null)
        {
            if (MainUIManager.Instance.IsPaused)
            {
                MainUIManager.Instance.IsPaused = false;
                Time.timeScale = 1f;

            }
        }

        SceneManager.LoadScene(1);
    }
}
