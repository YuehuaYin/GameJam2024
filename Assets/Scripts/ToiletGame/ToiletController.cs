using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletController : MonoBehaviour
{
    [SerializeField] private minigameManager manager;

    [SerializeField] GameObject bucket;
    [SerializeField] GameObject sponge;

    [SerializeField] Sprite cleanEmpty;
    [SerializeField] Sprite cleanFull;
    [SerializeField] Sprite dirtyEmpty;
    [SerializeField] Sprite dirtyFull;

    [SerializeField] Sprite bg1;
    [SerializeField] Sprite bg2;
    [SerializeField] Sprite bg3;
    [SerializeField] Sprite bg4;
    [SerializeField] Sprite bg5;
    [SerializeField] Sprite bg6;
    Sprite bgSprite;

    [SerializeField] private int bucketState;
    // 0 - empty + clean, 1 - empty + dirty , 2 - clean + full, 3 - dirty + full
    [SerializeField] private float timer;
    [SerializeField] private float timeBetweenRounds;

    [SerializeField] private GameObject background;

    private int baseBucketDirt;
    private float baseTimeSpentEmptying;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("manager").GetComponent<minigameManager>();

        if (GameManager.toiletUnlocked)
        {
            changeSprite(dirtyFull);
            bucketState = 3;
            setLevelStats();
            setuplevel();
        }
        else
        {
            manager.disableToilet();
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenRounds)
        {
            timer = 0;
            if (bucketState == 0)
            {
                bucketState = 3;
                bucket.GetComponent<Bucket>().resetBucket();
                sponge.GetComponent<Sponge>().resetCleaning();
                changeSprite(dirtyFull);
            }
            else
            {
                Debug.Log("lose cleaning");
            }
        }
    }
    public void cleanBucket()
    {
        Debug.Log("Controller Cleaning");
        Bucket b = bucket.GetComponent<Bucket>();
        switch (bucketState)
        {
            case 1:
                bucketState = 0;
                changeSprite(cleanEmpty);
                break;
            case 3:
                bucketState = 2;
                changeSprite(cleanFull);
                break;
        }
    }
    public void emptyBucket()
    {
        Bucket b = bucket.GetComponent<Bucket>();
        switch (bucketState)
        {
            case 2:
                bucketState = 0;
                changeSprite(cleanEmpty);
                break;
            case 3:
                bucketState = 1;
                Debug.Log("attempting to change sprite");
                changeSprite(dirtyEmpty);
                break;
        }
    }
    private void changeSprite(Sprite sprite)
    {
        bucket.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    private void setLevelStats()
    {
        switch (GameManager.level)
        {
            case 1:
                bgSprite = bg1;
                timeBetweenRounds = 45;
                baseBucketDirt = 3;
                baseTimeSpentEmptying = 2;
                break;
            case 2:
                bgSprite = bg2;
                timeBetweenRounds = 40;
                baseBucketDirt = 4;
                baseTimeSpentEmptying = 3;
                break;
            case 3:
                bgSprite = bg3;
                timeBetweenRounds = 40;
                baseBucketDirt = 4;
                baseTimeSpentEmptying = 4;
                break;
            case 4:
                bgSprite = bg4;
                timeBetweenRounds = 38;
                baseBucketDirt = 5;
                baseTimeSpentEmptying = 4.5f;
                break;
            case 5:
                bgSprite = bg5;
                timeBetweenRounds = 35;
                baseBucketDirt = 6;
                baseTimeSpentEmptying = 4.5f;
                break;
            case 6:
                bgSprite = bg5;
                timeBetweenRounds = 35;
                baseBucketDirt = 7;
                baseTimeSpentEmptying = 5;
                break;
            default:
                bgSprite = bg1;
                timeBetweenRounds = 45;
                baseBucketDirt = 3;
                baseTimeSpentEmptying = 2;
                break;
        }
    }
    private void setuplevel()
    {
        background.GetComponent<SpriteRenderer>().sprite = bgSprite;
        bucket.GetComponent<Bucket>().timeToEmpty = baseTimeSpentEmptying;
        sponge.GetComponent<Sponge>().baseBucketDirt = baseBucketDirt;
    }
}
