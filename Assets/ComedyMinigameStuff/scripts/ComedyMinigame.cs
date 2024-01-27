using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComedyMinigame : MonoBehaviour
{

    List<string> emojibank = new List<string> { "cheese", "devil", "eggplant", "ghost", "nerd", "sadcat", "shark", "skull", "smile", "think" };
    public GameObject speechPrefab;
    public timerBar timerbar;
    public GameObject jokeSelections;
    jokes[] jokeObjects;

    System.Random rand = new System.Random();
    [SerializeField] int jokeLength = 4;

    GameObject bubble = null;
    [SerializeField] bool showingEmojis = false;
    [SerializeField] int bubbleNum = 0;
    [SerializeField] float bubbleTimer = 1.0f;

    [SerializeField] float jokeTimer = 5.0f;
    [SerializeField] float timer;

    [SerializeField] int guessingNum = 0;

    List<string> joke = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        jokeObjects = jokeSelections.GetComponentsInChildren<jokes>();
        generateJoke();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (showingEmojis) {
            if (timer > bubbleTimer)
            {
                if (bubble != null)
                {
                    Destroy(bubble);
                }

                if (bubbleNum < jokeLength)
                {
                    speechBubble(joke[bubbleNum]);
                }
                else
                {
                    showingEmojis = false;
                    jokeSelections.SetActive(true);
                    setJokeSelections();
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            if (timer > jokeTimer)
            {
                failure();
            }
            else
            {
                timer += Time.deltaTime;
                timerbar.updateTimerBar(timer, jokeTimer);

            }
        }
    }

    void generateJoke() {
        timerbar.gameObject.GetComponent<Image>().fillAmount = 0;
        showingEmojis = true;
        jokeSelections.SetActive(false);
        joke.Clear();
        bubbleNum = 0;
        guessingNum = 0;

        for (int i = 0; i < jokeLength; i++)
        {
            joke.Add(emojibank[rand.Next(emojibank.Count)]);
        }
        timer = 0;
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
        bubble.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(emojiName);
        bubbleNum++;
        timer = 0;
    }

    void setJokeSelections()
    {
        if (guessingNum < jokeLength)
        {
            List<string> jokeEmojis = new List<string>();
            jokeEmojis.Add(joke[guessingNum]);
            int count = 0;

            while (count < 3)
            {
                string randEmoji = emojibank[rand.Next(emojibank.Count)];
                if (!jokeEmojis.Contains(randEmoji))
                {
                    jokeEmojis.Add(randEmoji);
                    count++;
                }
            }

            foreach (jokes obj in jokeObjects)
            {
                string emoji = jokeEmojis[rand.Next(jokeEmojis.Count)];
                obj.setEmoji(emoji);
                jokeEmojis.Remove(emoji);
            }
        }
        else
        {
            success();
        }
    }

    public void jokeClicked(string emojiName)
    {
        if (emojiName == joke[guessingNum])
        {
            guessingNum++;
            setJokeSelections();
        }
        else
        {
            failure();
        }
    }

    void success()
    {
        Debug.Log("Win");
        generateJoke();
    }

    void failure()
    {
        Debug.Log("Failed");
        generateJoke();
    }
}
