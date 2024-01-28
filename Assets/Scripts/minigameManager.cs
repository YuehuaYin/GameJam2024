using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class minigameManager : MonoBehaviour
{

    [SerializeField] public GameObject comedyGame;
    [SerializeField] public GameObject barGame;
    [SerializeField] public GameObject toiletGame;
    [SerializeField] public GameObject bouncerGame;

    [SerializeField] TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
