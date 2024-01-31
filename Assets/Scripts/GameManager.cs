using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int level { get; private set; } = 1;

    public static int money { get; set;} = 30;

    public static int tilesOwned { get; set;}
    public static GameManager instance = instance;

    public static bool toiletUnlocked = false;
    public static bool barUnlocked = false;
    public static bool bouncerUnlocked = false;
    public static Dictionary<Vector2Int, bool> savedTiles = new Dictionary<Vector2Int, bool>();
    public static void winGame()
    {
        money = money + Mathf.RoundToInt(15 * (1 + level * 0.2f) * (1 + tilesOwned * 0.1f));
    }
    public static void loseGame()
    {
        money -= 5;
    }
    public static void newStage()
    {
        toiletUnlocked = false;
        barUnlocked = false;
        bouncerUnlocked = false;
        tilesOwned = 0;
        level = level +=1;
        savedTiles = new Dictionary<Vector2Int, bool>();
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
