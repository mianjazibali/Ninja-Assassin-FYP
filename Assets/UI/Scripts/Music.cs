using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    private AudioSource _audioSource;
    public static int isMusicOn = 0; //0 for On because 0 is default value

    private void Start()
    {
        isMusicOn = PlayerPrefs.GetInt("MusicTriggerValue");
        _audioSource = GetComponent<AudioSource>();
    }

    /*
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }
    */

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 100)
        {
            _audioSource.Pause();
            //Destroy(this.gameObject);
        }
        else
        {
            if (!_audioSource.isPlaying && isMusicOn == 0)
            {
                _audioSource.UnPause();
                PlayerPrefs.SetInt("MusicTriggerValue", 0);
                PlayerPrefs.Save();
            }
            else
            if ( (!_audioSource.isPlaying && isMusicOn == 1) || (_audioSource.isPlaying && isMusicOn == 0) )
            {
                return;
            }
            else
            if (_audioSource.isPlaying && isMusicOn == 1)
            {
                _audioSource.Pause();
                PlayerPrefs.SetInt("MusicTriggerValue", 1);
                PlayerPrefs.Save();
            }
        }
    }
}