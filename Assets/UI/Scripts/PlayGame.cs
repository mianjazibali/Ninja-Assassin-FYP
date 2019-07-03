using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public Back back;

    private void Start()
    {
        back = GameObject.FindGameObjectWithTag("Back").GetComponent<Back>();
    }

    public void Play()
    {
        Fader.Instance.FadeIn().LoadLevel(1).FadeOut();
    }

    public void Scene_01()
    {
        Fader.Instance.FadeIn().LoadLevel(2).FadeOut();
    }

    public void Scene_02()
    {
        Fader.Instance.FadeIn().LoadLevel(3).FadeOut();
    }

    public void Scene_03()
    {
        Fader.Instance.FadeIn().LoadLevel(4).FadeOut();
    }

    public void Setting()
    {
        Fader.Instance.FadeIn().LoadLevel(5).FadeOut();
    }

    public void Shop()
    {
        Fader.Instance.FadeIn().LoadLevel(6).FadeOut();
    }

    public void Previous()
    {
        Fader.Instance.FadeIn().LoadLevel(back.getPreviousSceneIndex()).FadeOut();
    }

    public void Current()
    {
        Fader.Instance.FadeIn().LoadLevel(SceneManager.GetActiveScene().buildIndex).FadeOut();
    }

    public void Next()
    {
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
        string path = SceneUtility.GetScenePathByBuildIndex(level + 6);
        string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
        Fader.Instance.FadeIn().LoadLevel(level + 6).FadeOut();
    }

    public void ResetGame()
    {
        LevelSelector01.ResetProgress();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
