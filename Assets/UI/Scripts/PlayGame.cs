using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public Back back;

    private void Start()
    {
        back = GameObject.FindGameObjectWithTag("Back").GetComponent<Back>();
    }

    public void PlayClick()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }

    public void Play()
    {
        PlayClick();
        Fader.Instance.FadeIn().LoadLevel(1).FadeOut();
    }

    public void Scene_01()
    {
        PlayClick();
        Fader.Instance.FadeIn().LoadLevel(2).FadeOut();
    }

    public void Scene_02()
    {
        PlayClick();
        Fader.Instance.FadeIn().LoadLevel(3).FadeOut();
    }

    public void Scene_03()
    {
        PlayClick();
        Fader.Instance.FadeIn().LoadLevel(4).FadeOut();
    }

    public void Setting()
    {
        PlayClick();
        Fader.Instance.FadeIn().LoadLevel(5).FadeOut();
    }

    public void Shop()
    {
        PlayClick();
        Fader.Instance.FadeIn().LoadLevel(6).FadeOut();
    }

    public void Previous()
    {
        PlayClick();
        Fader.Instance.FadeIn().LoadLevel(back.getPreviousSceneIndex()).FadeOut();
    }

    public void Current()
    {
        PlayClick();
        Fader.Instance.FadeIn().LoadLevel(SceneManager.GetActiveScene().buildIndex).FadeOut();
    }

    public void Next()
    {
        PlayClick();
        Fader.Instance.FadeIn().LoadLevel(SceneManager.GetActiveScene().buildIndex + 1).FadeOut();
    }

    public void LevelSelect()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex - 6;
        if (currentLevel <= 9)
        {
            Scene_01();
        }
        else
        if (currentLevel <= 18)
        {
            Scene_02();
        }
        else
        if (currentLevel <= 27)
        {
            Scene_03();
        }
    }

    public void PlayLevel(int level)
    {
        PlayClick();
        Fader.Instance.FadeIn().LoadLevel(level + 6).FadeOut();
    }

    public void ResetGame()
    {
        PlayClick();
        LevelSelector01.ResetProgress();
    }

    public void ExitGame()
    {
        PlayClick();
        Application.Quit();
    }
}
