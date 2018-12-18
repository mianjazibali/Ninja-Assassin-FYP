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

    public void Scene_01()
    {
        levelChanger.FadeToLevel(2);
    }

    public void Scene_02()
    {
        levelChanger.FadeToLevel(3);
    }

    public void Scene_03()
    {
        levelChanger.FadeToLevel(4);
    }
}
