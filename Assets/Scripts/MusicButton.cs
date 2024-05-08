using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    [SerializeField] GameObject onButton;
    [SerializeField] GameObject offButton;
    [SerializeField] AudioClip clickSound;
    AudioSource music;
    AudioSource fxSounds;
    
    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("MainContainer").GetComponent<AudioSource>();
        fxSounds = GameObject.FindGameObjectWithTag("FxAudioSource").GetComponent<AudioSource>();
        

        if (music.isPlaying)
        {
            MainUIManager.Instance.MusicOn = true;
            onButton.SetActive(true);
            offButton.SetActive(false);
        }
        else
        {
            MainUIManager.Instance.MusicOn = false;
            onButton.SetActive(false);
            offButton.SetActive(true);
        }
        Debug.Log(MainUIManager.Instance.MusicOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MusicOff()
    {
        if (onButton != null )
        {
            onButton.SetActive(false);
            offButton.SetActive(true);
            MakeClick();
            music.Pause();
            MainUIManager.Instance.MusicOn = false;
            MainUIManager.Instance.MusicPauseTime = Time.time;
        }
    }

    public void MusicOn()
    {
        if (offButton != null)
        {
            offButton.SetActive(false);
            onButton.SetActive(true);
            MakeClick();
            music.Play();
            float pauseTime = MainUIManager.Instance.MusicPauseTime;
            float elapsedTime = Time.time - pauseTime;  
            music.time = elapsedTime;
            MainUIManager.Instance.MusicOn = true;
        }
    }

    void MakeClick()
    {
        fxSounds.volume = 0.3f;
        fxSounds.PlayOneShot(clickSound);
    }
}
