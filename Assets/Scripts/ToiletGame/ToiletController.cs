using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletController : MonoBehaviour
{
    [SerializeField] GameObject bucket;
    [SerializeField] Sprite cleanEmpty;
    [SerializeField] Sprite cleanFull;
    [SerializeField] Sprite dirtyEmpty;
    [SerializeField] Sprite dirtyFull;

    [SerializeField] private int bucketState;
    // 0 - empty + clean, 1 - empty + dirty , 2 - clean + full, 3 - dirty + full
    // Start is called before the first frame update
    void Start()
    {
        changeSprite(dirtyFull);
        bucketState = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
