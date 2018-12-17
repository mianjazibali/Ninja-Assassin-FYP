using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public LevelChanger levelChanger;

    private void Start()
    {
              
    }

    public void Play()
    {
        levelChanger.FadeToLevel(1);
    }
}
