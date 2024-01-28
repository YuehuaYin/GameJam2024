using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int level { get; private set; } = 4;

    public static int money { get; set;}

    public static int tilesOwned { get; set;}
    public static GameManager instance = instance;

    public static bool toiletUnlocked = false;
    public static bool barUnlocked = false;
    public static bool bouncerUnlocked = false;

    public static void winGame()
    {
        money = money + Mathf.RoundToInt(15 * (1 + tilesOwned * 0.1f));
    }
    public static void loseGame()
    {

    }
    public static void newStage()
    {
        toiletUnlocked = false;
        barUnlocked = false;
        bouncerUnlocked = false;
        tilesOwned = 0;
        //level = level +=1;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
