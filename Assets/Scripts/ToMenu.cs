using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour
{
   public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
