using UnityEngine;
using UnityEngine.UI;

public class LevelSelector03 : MonoBehaviour
{
    public LevelChanger levelChanger;
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
        }
    }

    public void Select(int levelIndex)
    {
        levelChanger.FadeToLevel(levelIndex);
    }
}
