﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static int isSoundOn = 0;
    public static AudioManager instance;

    private void Start()
    {
        isSoundOn = PlayerPrefs.GetInt("SoundTriggerValue");
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        if (isSoundOn == 0)
        {
            Sound s = System.Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.Log("Sound : " + name + " not found");
                return;
            }
            s.source.Play();
        }
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("SoundTriggerValue", isSoundOn);
        PlayerPrefs.Save();
    }
}
