using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sponge : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;
    private Vector2 originalPos;
    [SerializeField] int bucketDirt;
    private bool cleaning = false;
    [SerializeField] AudioSource scrubbing;
    //first fame
    [SerializeField] GameObject manager;
    [SerializeField] public int baseBucketDirt;

    [SerializeField] Vector3 pointA;
    [SerializeField] Vector3 pointB;
    [SerializeField] Vector3 pointC;

    private Vector3 aimedPoint;
    public void Start()
    {
        originalPos = GetComponent<Transform>().localPosition;
        bucketDirt = baseBucketDirt;
        aimedPoint = originalPos;
    }
    public void choosePoint()
    {
        int i = Mathf.RoundToInt(Random.Range(1, 4));
        switch (i)
        {
            case 1:
                aimedPoint = pointA;
                break;
            case 2:
                aimedPoint = pointB;
                break;
            case 3:
                aimedPoint = pointC;
                break;
            case 4:
                aimedPoint = originalPos;
                break;
        }
    }
    private void Update()
    {
        if (GameManager.level > 3)
        {
            if (!cleaning && aimedPoint == transform.localPosition)
            {
                choosePoint();
            }
            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, aimedPoint, 0.0005f * (GameManager.level - 3));
            }
        }
        float mouseXSpeed = Input.GetAxis("Mouse X");
        float mouseYSpeed = Input.GetAxis("Mouse Y");
        float mouseSpeed = Mathf.Sqrt(Mathf.Pow(mouseYSpeed, 2) + Mathf.Pow(mouseXSpeed, 2));
       if (cleaning)
        {
            if (bucketDirt == 0)
            {
                Debug.Log("Clean");
                cleaning = false;
                scrubbing.Stop();
                manager.GetComponent<ToiletController>().cleanBucket();
            }
            else
            {
                if (mouseSpeed > 1)
                {
                    Debug.Log("Cleaning");
                    bucketDirt -= 1;
                    cleaning = false;
                    scrubbing.Stop();
                }
            }
        }
    }

    private void OnMouseDown()
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;

    }

    private void OnMouseUp()
    {
        GetComponent<Transform>().localPosition = new Vector3(originalPos.x, originalPos.y, 0);
        cleaning = false;
        scrubbing.Stop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bucket")
        {
            cleaning = true;
            scrubbing.Play();
        }
    }
    public void resetCleaning()
    {
        bucketDirt = baseBucketDirt;
    }
    
}
