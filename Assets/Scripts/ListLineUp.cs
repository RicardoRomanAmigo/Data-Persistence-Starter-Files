using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListLineUp : MonoBehaviour
{
    float topBorder = 250.0f;
    float distance = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position.y < topBorder)
        {
            StartCoroutine("MovingUp");
        }
        

    }

    IEnumerator MovingUP()
    {
        yield return new WaitForSeconds(1);

        

        this.gameObject.transform.position = new Vector3(0,this.gameObject.transform.position.y + distance, 0);

        distance += distance;
    }
}
