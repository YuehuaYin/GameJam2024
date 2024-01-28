using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class minigameManager : MonoBehaviour
{
    public timerBar timerbar;

    [SerializeField] public GameObject comedyGame;
    [SerializeField] public GameObject barGame;
    [SerializeField] public GameObject toiletGame;
    [SerializeField] public GameObject bouncerGame;

    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] private float timeLimit = 90;
    [SerializeField] private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerbar.updateTimerBar(timer, timeLimit);
        if (timer >= timeLimit)
        {
            switch (GameManager.level)
            {
                case 1:
                    SceneManager.LoadScene("Level1");
                    break;
                case 2:
                    SceneManager.LoadScene("Level2");
                    break;
                case 3:
                    SceneManager.LoadScene("Level3");
                    break;
                case 4:
                    SceneManager.LoadScene("Level4");
                    break;
                case 5:
                    SceneManager.LoadScene("Level5");
                    break;
                case 6:
                    SceneManager.LoadScene("Level6");
                    break;
                default:
                    SceneManager.LoadScene("Level1");
                    break;
            }
        }
    }

    public void disableComedy()
    {
        comedyGame.SetActive(false);
    }

    public void disableBar()
    {
        barGame.SetActive(false);
    }

    public void disableToilet()
    {
        toiletGame.SetActive(false);
    }

    public void disableBouncer()
    {
        bouncerGame.SetActive(false);
    }
    public void updateMoney()
    {
        moneyText.text = "Money: " + GameManager.money;
    }
    public void winGame()
    {
        GameManager.winGame();
        updateMoney();
    }
    public void loseGame()
    {
        GameManager.loseGame();
        updateMoney();
    }
}
