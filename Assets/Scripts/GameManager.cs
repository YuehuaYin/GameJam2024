using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int level { get; private set; } = 1;

    public static int money { get; set;}

    public static int tilesOwned { get; set;}
    public static GameManager instance = instance;

    public static bool toiletUnlocked;
    public static bool barUnlocked;
    public static bool bouncerUnlocked;

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
