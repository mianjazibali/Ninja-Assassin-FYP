using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public LevelChanger levelChanger;
    public Back back;

    private void Start()
    {
        back = GameObject.FindGameObjectWithTag("Back").GetComponent<Back>();
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

    public void Setting()
    {
        levelChanger.FadeToLevel(5);
    }

    public void Previous()
    {
        levelChanger.FadeToLevel(back.getPreviousSceneIndex());
    }

    public void Current()
    {
        levelChanger.FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayLevel(int level)
    {
        levelChanger.FadeToLevel(level + 6);
    }
}
