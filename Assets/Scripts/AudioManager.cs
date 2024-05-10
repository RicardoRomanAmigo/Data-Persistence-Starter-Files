
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("AudioManager");
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }
    #endregion

     private AudioSource _audioSource;
    private bool isPlaying = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.playOnAwake = false;
        _audioSource.loop = false;

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Audio clip is null.");
            return;
        }

        if(!isPlaying)
        {
            isPlaying = true;

            _audioSource.clip = clip;
            _audioSource.Play();

            Invoke(nameof(ResetIsPlaying), clip.length);

        }
    }

    private void ResetIsPlaying()
    {
        isPlaying = false;
    }

    public void StopSound()
    {
        _audioSource.Stop();
    }

    public void Volume(float volume)
    {
        
        _audioSource.volume = volume;
    }
    //for more sound methods
}
