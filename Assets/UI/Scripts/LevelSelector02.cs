using UnityEngine;
using UnityEngine.UI;

public class LevelSelector02 : MonoBehaviour
{
    public LevelChanger levelChanger;
    public Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached");

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 10 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                levelButtons[i].transform.GetChild(1).gameObject.SetActive(true);
                levelButtons[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public void Select(int levelIndex)
    {
        levelChanger.FadeToLevel(levelIndex);
    }
}
