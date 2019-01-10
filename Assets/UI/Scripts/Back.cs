using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    [SerializeField]
    public int[] array;
    public int currentSceneIndex;
    public int currentArrayIndex;

    private void Start()
    {
        array = new int[10];
        currentSceneIndex = 0;
        for(int i = 0; i < 10; i++)
        {
            array[i] = -1;
        }
        currentArrayIndex = 0;
        array[currentArrayIndex] = currentSceneIndex;
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Back");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        int temp = SceneManager.GetActiveScene().buildIndex;
        if (temp != currentSceneIndex) //1-0 
        {
            currentSceneIndex = temp;
            foreach (int i in array)
            {
                if(i == currentSceneIndex)
                {
                    currentArrayIndex--;
                    return;
                }
            }
            Debug.Log(currentSceneIndex);
            array[++currentArrayIndex] = currentSceneIndex;
        }
    }

    public int getPreviousSceneIndex()
    {
        array[currentArrayIndex] = -1;
        return array[currentArrayIndex - 1];
    } 
}
