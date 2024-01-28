using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int level { get; private set; } = 4;

    public static int money { get; set;}

    public static int tilesOwned { get; set;}
    public static GameManager instance = instance;

    public static bool toiletUnlocked = true;
    public static bool barUnlocked = true;
    public static bool bouncerUnlocked = true;

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
