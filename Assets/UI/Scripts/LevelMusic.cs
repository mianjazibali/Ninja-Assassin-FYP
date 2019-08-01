using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    public static int isMusicOn = 0; //0 for On because 0 is default value
    public float volume;

    private void Start()
    {
        isMusicOn = PlayerPrefs.GetInt("MusicTriggerValue");
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = volume;
    }

    void Update()
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