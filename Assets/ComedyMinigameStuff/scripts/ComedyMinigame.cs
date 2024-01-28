using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComedyMinigame : MonoBehaviour
{

    List<string> emojibank = new List<string> { "cheese", "devil", "eggplant", "ghost", "nerd", "sadcat", "shark", "skull", "smile", "think" };
    List<string> hecklebank = new List<string> { "booooooooo!!!", "you suck!", "who is this loser?", "get a real job", "not funny.."};

    public GameObject background;
    public GameObject speechPrefab;
    public GameObject hecklePrefab;

    public timerBar timerbar;
    public GameObject jokeSelections;

    public GameObject successSound;
    public GameObject cheerSound;
    public GameObject failSound;
    public GameObject promptSound;

    jokes[] jokeObjects;

    System.Random rand = new System.Random();
    [SerializeField] int jokeLength = 4;

    GameObject bubble = null;
    GameObject heckle = null;

    [SerializeField] bool showingEmojis = false;
    [SerializeField] bool breakTime = false;
    [SerializeField] float breakLength;

    [SerializeField] int bubbleNum = 0;
    [SerializeField] float bubbleTimer = 3.0f;

    [SerializeField] float jokeTimer = 20.0f;
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
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgbasement");
                heckleRate = 0;
                breakLength = 10;
                break;
            case 2:
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgclub");
                heckleRate = 1;
                breakLength = 9;
                break;
            case 3:
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgarena");
                heckleRate = 2;
                breakLength = 8;
                break;
            case 4:
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgspace");
                heckleRate = 3;
                breakLength = 7;
                break;
            case 5:
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgcthulu");
                heckleRate = 4;
                breakLength = 6;
                break;
            case 6:
                background.GetComponent<Image>().sprite = Resources.Load<Sprite>("bgbasement");
                heckleRate = 5;
                breakLength = 5;
                break;
        }

        jokeObjects = jokeSelections.GetComponentsInChildren<jokes>();
        resetstuff();
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
            if (timer > breakLength)
            {
                timer = 0;
                breakTime = false;
                generateJoke();
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
            if (rand.Next(500) < heckleRate)
            {
                spawnHeckle();
            }
        }
    }

    void generateJoke() {

        joke.Clear();

        for (int i = 0; i < jokeLength; i++)
        {
            joke.Add(emojibank[rand.Next(emojibank.Count)]);
        }

        timer = 0;
        showingEmojis = true;
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
        promptSound.GetComponent<AudioSource>().Play();
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
            if (guessingNum < jokeLength-1)
            {
                successSound.GetComponent<AudioSource>().Play();
            }
            else
            {
                cheerSound.GetComponent<AudioSource>().Play();
            }

            guessingNum++;
            setJokeSelections();
        }
        else
        {
            failure();
        }
    }

    void resetstuff()
    {
        timerbar.gameObject.GetComponent<Image>().fillAmount = 0;
        jokeSelections.SetActive(false);
        bubbleNum = 0;
        guessingNum = 0;
        timer = 0;
    }

    void success()
    {
        Debug.Log("Win");
        resetstuff();
        breakTime = true;
    }

    void failure()
    {
        Debug.Log("Failed");
        failSound.GetComponent<AudioSource>().Play();
        resetstuff();
        breakTime = true;
    }
}
