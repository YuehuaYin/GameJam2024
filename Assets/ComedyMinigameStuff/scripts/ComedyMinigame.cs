using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComedyMinigame : MonoBehaviour
{

    List<string> emojibank = new List<string> { "cheese", "devil", "eggplant", "ghost", "nerd", "sadcat", "shark", "skull", "smile", "think" };
    List<string> hecklebank = new List<string> { "booooooooo!!!", "you suck!", "who is this loser?", "get a real job", "not funny..."};

    public GameObject background;
    public GameObject speechPrefab;
    public GameObject hecklePrefab;
    public timerBar timerbar;
    public GameObject jokeSelections;
    jokes[] jokeObjects;

    System.Random rand = new System.Random();
    [SerializeField] int jokeLength = 4;

    GameObject bubble = null;
    GameObject heckle = null;

    [SerializeField] bool showingEmojis = false;
    [SerializeField] bool breakTime = false;

    [SerializeField] int bubbleNum = 0;
    [SerializeField] float bubbleTimer = 3.0f;

    [SerializeField] float jokeTimer = 15.0f;
    [SerializeField] float timer;

    [SerializeField] bool heckling = false;
    [SerializeField] float heckleTimer;
    [SerializeField] int heckleRate;

    [SerializeField] int guessingNum = 0;

    List<string> joke = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.level)
        {
            case 1:
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Stand-up background basement");
                heckleRate = 0;
                break;
            case 2:
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Stand-up background comedy club");
                heckleRate = 1;
                break;
            case 3:
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Stand-up background arena");
                heckleRate = 2;
                break;
            case 4:
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Stand-up background space");
                heckleRate = 3;
                break;
            case 5:
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Stand-up background cthulu");
                heckleRate = 4;
                break;
            case 6:
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Stand-up background basement");
                heckleRate = 5;
                break;
        }

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
                    timer = 0;
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        else if (breakTime)
        {
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
                    timer = 0;
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

        if (heckling)
        {
            if (heckleTimer > 1.5f)
            {
                if (heckle != null)
                {
                    Destroy(heckle);
                }

                heckleTimer = 0;
                heckling = false;

            }
            else
            {
                heckleTimer += Time.deltaTime;
            }
        }
        else
        {
            if (rand.Next(1000) < heckleRate)
            {
                spawnHeckle();
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
        float randX = (float) NextDouble(-8.0, -0.9);
        float randY = (float) NextDouble(0.75, 1.6);
        bubble = Instantiate(speechPrefab, new Vector2(randX, randY), Quaternion.identity);
        bubble.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(emojiName);
        bubbleNum++;
        timer = 0;
    }

    void spawnHeckle()
    {
        float randX = (float)NextDouble(-7.5, -1.2);
        float randY = (float)NextDouble(0.75, 1.6);
        heckle = Instantiate(hecklePrefab, new Vector2(randX, randY), Quaternion.identity);
        heckle.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshPro>().text = hecklebank[rand.Next(hecklebank.Count)];
        heckleTimer = 0;
        heckling = true;
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
