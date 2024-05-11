using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonsManager : MonoBehaviour
{
    private Color colorText;
    private Text childTxt;
    Color colorOver = new Color(1.0f, 0.64f, 0.0f);
    Color colorSelect = new Color(1.0f, 0.80f, 0.0f);
    Color colorOriginal;

    private void Awake()
    {
        childTxt = transform.GetChild(0).GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        colorOriginal = Color.white;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseEnter()
    {

        if (childTxt != null)
        {
            childTxt.color = colorOver;
        }
    }

    public void OnMouseExit()
    {
        if (childTxt != null)
        {
            childTxt.color = colorOriginal;
        }
    }

    public void OnMouseDown()
    {

        if (childTxt != null)
        {
            childTxt.color = colorSelect;
        }
    }

}
