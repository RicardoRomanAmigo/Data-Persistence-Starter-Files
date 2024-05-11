using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class TextManager : MonoBehaviour
{
     AudioSource fxAudio;

    [SerializeField] GameObject myButton;
    //[SerializeField] GameObject myText;
    Text myText;
    Color textColor;
    Color colorOver = new Color(1.0f, 0.64f, 0.0f);
    
    Color colorSelect = new Color(1.0f, 0.80f, 0.0f);
    
    Color colorOriginal;

    [SerializeField] AudioClip  mouseEnterFx;
    [SerializeField] AudioClip  mouseClicFx;

    

    // Start is called before the first frame update
    void Start()
    {
        if(myButton != null)
        {
            myText = myButton.transform.GetChild(0).gameObject.GetComponent<Text>();

            colorOriginal = myText.color;  
        }

        //fxAudio = GameObject.FindGameObjectWithTag("FxAudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnMouseEnter()
    {
        Debug.Log("entroboton");

        if (myText != null)
        {
            myText.color = colorOver;  
        }

        MakeClick();
    }

    public void OnMouseExit()
    {
        if (myText != null)
        {
            myText.color = colorOriginal;
        }
    }

    public void OnMouseDown()
    {
        if (myText != null)
        {
            myText.color = colorSelect;
        }

        if (mouseClicFx != null)
        {
            AudioManager.Instance.PlaySound(mouseClicFx);
        }
    }

    void MakeClick()
    {
        //fxAudio.volume = 0.1f;
        //fxAudio.PlayOneShot(mouseEnterFx);

        AudioManager.Instance.PlaySound(mouseEnterFx);
    }
}
