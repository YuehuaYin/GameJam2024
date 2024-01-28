using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleGame : MonoBehaviour
{
    [SerializeField] private GameObject greenBottle;
    [SerializeField] private GameObject blueBottle;
    [SerializeField] private GameObject yellowBottle;

    [SerializeField] private GameObject CustomerGreenLeft;
    [SerializeField] private GameObject CustomerGreenRight;

    [SerializeField] private GameObject CustomerBlueLeft;
    [SerializeField] private GameObject CustomerBlueRight;

    [SerializeField] private GameObject CustomerYellowLeft;
    [SerializeField] private GameObject CustomerYellowRight;

    [SerializeField] private Transform LeftSpawn;
    [SerializeField] private Transform RightSpawn;

    private int bottleNum = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.level == 2)
        {
            blueBottle.SetActive(false);
            yellowBottle.SetActive(false);
            bottleNum = 1;
        }
        else if (GameManager.level == 3)
        {
            yellowBottle.SetActive(false);
            bottleNum = 2;
        }
        else if(GameManager.level == 4)
        {
            yellowBottle.SetActive(false);
            greenBottle.GetComponent<BottleManager>().gravity = 0.2f;
            blueBottle.GetComponent<BottleManager>().gravity = 0.2f;
            bottleNum = 2;
        }
    }


    public void finishLeft()
    {
        
    }

    public void finishRight()
    {
        
    }
}
