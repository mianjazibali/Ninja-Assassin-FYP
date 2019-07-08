using UnityEngine.UI;
using UnityEngine;

public class ButtonCooldown : MonoBehaviour
{
    public Transform LoadingBar;
    [SerializeField] private float currentAmount;
    [SerializeField] private float time;
    [SerializeField] private float animationTime;
    private float speed;

    private Image LoadingBarImage;


    void Start()
    {
        LoadingBarImage = LoadingBar.GetComponent<Image>();
        speed = 100 / (time + animationTime);
    }

    private void OnEnable()
    {
        currentAmount = 0f;
    }

    // Update is called once per frame.
    void Update()
    {
        if(currentAmount < 100)
        {
            currentAmount += speed * Time.deltaTime;
            //Loading Wait...
        }
        else
        {
            gameObject.SetActive(false);
        }
        LoadingBarImage.fillAmount = currentAmount / 100;
    }
}
