using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComedyMinigame : MonoBehaviour
{

    List<string> emojibank = new List<string> { "cheese", "devil", "eggpplant", "ghost", "nerd", "sadcat", "shark", "skull", "smile", "think" };
    public GameObject speechPrefab;
    System.Random rand = new System.Random();
    [SerializeField] int jokeLength = 4;

    GameObject bubble;
    [SerializeField] bool showingEmojis = true;
    [SerializeField] int bubbleNum = 0;
    [SerializeField] float bubbleTimer = 1.0f;

    [SerializeField] float jokeTimer = 5.0f;
    [SerializeField] float timer = 0.0f;

    List<string> joke = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        generateJoke();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (showingEmojis) {
            if (timer > bubbleTimer)
            {
                Destroy(bubble.gameObject);

                if (bubbleNum <= jokeLength)
                {

                }
            }
        }
        else
        {

        }
    }

    void generateJoke()
    {
        joke.Clear();
        bubbleNum = 0;

        for (int i = 0; i < jokeLength; i++)
        {
            joke.Add(emojibank[rand.Next(emojibank.Count)]);
        }

        for (int i = 0; i < joke.Count; i++)
        {
           Debug.Log(joke[i]);
        }
    }

    public double NextDouble(double MinValue, double MaxValue)
    {
        return rand.NextDouble() * (MaxValue - MinValue) + MinValue;
    }

    void speechBubble(string emojiName)
    {
        float randX = (float) NextDouble(-2, 2);
        float randY = (float) NextDouble(-1.8, -0.9);
        bubble = Instantiate(speechPrefab, new Vector2(randX, randY), Quaternion.identity);
    }
}
