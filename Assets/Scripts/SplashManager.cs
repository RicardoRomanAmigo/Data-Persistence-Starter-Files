using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayMethod(3.0f));  
    }

    IEnumerator DelayMethod(float delayTime)
    {

        yield return new WaitForSeconds(delayTime);

        ToMenu toMenu = GetComponent<ToMenu>();

        toMenu.GoToMenu();
    }
}
