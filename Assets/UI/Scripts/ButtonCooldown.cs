using UnityEngine.UI;
using UnityEngine;

public class ButtonCooldown : MonoBehaviour
{
    public Transform LoadingBar;
    [SerializeField] private float currentAmount;
    public enum Weapon
    {
        Shuriken,
        Sword,
    }
    public Weapon weapon = Weapon.Shuriken;
    [SerializeField] private float maxCooldownTime;
    [SerializeField] private float animationTime;
    private float speed;

    private Image LoadingBarImage;


    void Start()
    {
        LoadingBarImage = LoadingBar.GetComponent<Image>();
        if (weapon == Weapon.Sword)
            maxCooldownTime -= PlayerPrefs.GetInt("currentDash");
        else
            maxCooldownTime -= PlayerPrefs.GetInt("shurikenDash");
        speed = 100 / (maxCooldownTime + animationTime);
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
