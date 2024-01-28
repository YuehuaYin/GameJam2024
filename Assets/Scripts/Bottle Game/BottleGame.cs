using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class BottleGame : MonoBehaviour
{
    [SerializeField] private minigameManager manager;

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
    [SerializeField] private Transform LeftPosition;
    [SerializeField] private Transform RightSpawn;
    [SerializeField] private Transform RightPosition;

    [SerializeField] private Sprite backgroundBasement;
    [SerializeField] private Sprite backgroundClub;
    [SerializeField] private Sprite backgroundArena;
    [SerializeField] private Sprite backgroundSpace;
    [SerializeField] private Sprite backgroundDeep;

    [SerializeField] private SpriteRenderer backgroundRenderer;

    private int bottleNum = 3;
    private Random random = new Random();

    private GameObject left;
    private GameObject right;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("manager").GetComponent<minigameManager>();

        if (!GameManager.barUnlocked)
        {
            manager.disableBar();
        }
        else if (GameManager.level == 1)
        {
            manager.disableBar();
        }
        else if (GameManager.level == 2)
        {
            blueBottle.SetActive(false);
            yellowBottle.SetActive(false);
            bottleNum = 1;
            backgroundRenderer.sprite = backgroundClub;
            StartCoroutine(StartCustomer());
        }
        else if (GameManager.level == 3)
        {
            yellowBottle.SetActive(false);
            bottleNum = 2;
            backgroundRenderer.sprite = backgroundArena;
            StartCoroutine(StartCustomer());
        }
        else if(GameManager.level == 4)
        {
            yellowBottle.SetActive(false);
            greenBottle.GetComponent<BottleManager>().gravity = 0.2f;
            blueBottle.GetComponent<BottleManager>().gravity = 0.2f;
            bottleNum = 2;
            backgroundRenderer.sprite = backgroundSpace;
            StartCoroutine(StartCustomer());
        }
        else if (GameManager.level == 5)
        {
            backgroundRenderer.sprite = backgroundDeep;
            StartCoroutine(StartCustomer());
        }
        else
        {
            backgroundRenderer.sprite = backgroundBasement;
            StartCoroutine(StartCustomer());
        }
    }

    IEnumerator StartCustomer()
    {
        while (true) //CHANGE FROM TRUE WHEN GAME ENDS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            (GameObject, GameObject) t = (CustomerGreenLeft, CustomerGreenRight);
            
            switch (random.Next(1,bottleNum+1))
            {
                case 1:
                    break;
                case 2: 
                    t = (CustomerBlueLeft, CustomerBlueRight);
                    break;
                case 3: 
                    t = (CustomerYellowLeft, CustomerYellowRight);
                    break;
            }
            
            if (random.NextDouble() > 0.5f)
            {
                if (left == null)
                {
                    left = Instantiate(t.Item1, gameObject.transform);
                    left.transform.position = LeftSpawn.position;
                    StartCoroutine(startLeft(left));
                }
                else if (right == null)
                {
                    right = Instantiate(t.Item2, gameObject.transform);
                    right.transform.position = RightSpawn.position;
                    StartCoroutine(startRight(right));
                }
            }
            else
            {
                if (right == null)
                {
                    right = Instantiate(t.Item2, gameObject.transform);
                    right.transform.position = RightSpawn.position;
                    StartCoroutine(startRight(right));
                }
                else if (left == null)
                {
                    left = Instantiate(t.Item1, gameObject.transform);
                    left.transform.position = LeftSpawn.position;
                    StartCoroutine(startLeft(left));
                }
            }

            yield return new WaitForSeconds(random.Next(10, 35));
        }
    }

    IEnumerator startLeft(GameObject customer)
    {
        customer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        while (customer.transform.localPosition.x < LeftPosition.localPosition.x)
        {
            customer.transform.localPosition = new Vector3(customer.transform.localPosition.x + 0.01f, customer.transform.localPosition.y);
            yield return null;
        }

        customer.GetComponentInChildren<Customer>().start = true;
    }

    IEnumerator startRight(GameObject customer)
    {
        customer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        while (customer.transform.localPosition.x > RightPosition.localPosition.x)
        {
            customer.transform.localPosition = new Vector3(customer.transform.localPosition.x - 0.01f, customer.transform.localPosition.y);
            yield return null;
        }

        customer.GetComponentInChildren<Customer>().start = true;
    }

    public void finishLeft(GameObject customer)
    {
        manager.winGame();
        StartCoroutine(slideOffLeft(customer)); //ADD POINTS
    }

    public void failLeft(GameObject customer)
    {
        manager.loseGame();
        StartCoroutine(slideOffLeft(customer)); //SUBTRACT POINTS??
    }

    public void finishRight(GameObject customer)
    {
        manager.winGame();
        StartCoroutine(sliderOffRight(customer));

    }

    public void failRight(GameObject customer)
    {
        manager.loseGame();
        StartCoroutine(sliderOffRight(customer)); 
    }

    IEnumerator slideOffLeft(GameObject customer)
    {
        while (customer.transform.localPosition.x > LeftSpawn.localPosition.x)
        {
            customer.transform.localPosition = new Vector3(customer.transform.localPosition.x - 0.02f, customer.transform.localPosition.y);
            yield return null;
        }
        Destroy(customer);
    }

    IEnumerator sliderOffRight(GameObject customer)
    {
        while (customer.transform.localPosition.x < RightSpawn.localPosition.x)
        {
            customer.transform.localPosition = new Vector3(customer.transform.localPosition.x + 0.02f, customer.transform.localPosition.y);
            yield return null;
        }
        Destroy(customer);
    }
}
