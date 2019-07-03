using UnityEngine;
using UnityEngine.UI;

public class LevelSelector03 : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached");

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 19 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                levelButtons[i].transform.GetChild(1).gameObject.transform.GetChild(PlayerPrefs.GetInt((i + 1).ToString())).gameObject.SetActive(true);
                levelButtons[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public void Select(int levelIndex)
    {
        Fader.Instance.FadeIn().LoadLevel(levelIndex).FadeOut();
    }
}
