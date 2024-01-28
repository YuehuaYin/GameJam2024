using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bucket : MonoBehaviour
{
    [SerializeField] ParticleSystem piss;
    [SerializeField] ParticleSystem shit;
    private Vector2 difference = Vector2.zero;
    private Vector2 originalPos;
    private bool emptying;
    [SerializeField] public float timeToEmpty;
    [SerializeField] private float timeSpentEmptying;
    [SerializeField] private bool full = true;
    [SerializeField] private float timelimit;
    [SerializeField] private float gameTime;

    [SerializeField] private Transform drainTransform;

    [SerializeField] private GameObject manager;

    [SerializeField] AudioSource emptyBucketSound;
    [SerializeField] AudioSource peeArrivalSound;
    private ParticleSystem.EmissionModule pissEmission;
    private ParticleSystem.EmissionModule shitEmission;
    //first fame
    [SerializeField] Vector3 pointA;
    [SerializeField] Vector3 pointB;
    [SerializeField] Vector3 pointC;
    public void Start()
    {
        originalPos = GetComponent<Transform>().localPosition;
        
        gameTime = 0;
        pissEmission = piss.emission;
        shitEmission = shit.emission; 
        pissEmission.enabled = false;
        shitEmission.enabled = false;

        peeArrivalSound.Play();
        aimedPoint = originalPos;
    }
    public void resetBucket()
    {
        pissEmission.enabled = false;
        shitEmission.enabled = false; 
        full = true;
        timeSpentEmptying = 0;
        emptying = false;
        emptyBucketSound.Stop();
        peeArrivalSound.Play();
    }
    private Vector3 aimedPoint;
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
            if (!emptying && aimedPoint == transform.localPosition)
            {
                choosePoint();
            }
            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, aimedPoint, 0.0005f * (GameManager.level - 3));
            }
        }
        if (emptying)
        {
            timeSpentEmptying += Time.deltaTime;
            if (timeToEmpty <= timeSpentEmptying)
            {
                finishEmptying();
            }
        }
        if (gameTime >= timelimit)
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "drain")
        {  
            EnterDrain();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "drain")
        {
            stopEmptying();
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("helo");
    //    if (collision.gameObject.tag == "drain")
    //    {
    //        EnterDrain();
    //    }
    //}
    private void finishEmptying()
    {
        pissEmission.enabled = false;
        shitEmission.enabled = false;
        emptying = false;
        full = false;
        emptyBucketSound.Stop();
        GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
        manager.GetComponent<ToiletController>().emptyBucket();
    }
    private void stopEmptying()
    {
        pissEmission.enabled = false;
        shitEmission.enabled = false;
        emptying = false;
        emptyBucketSound.Stop();
        GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
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

    }
    private void EnterDrain()
    {
        if (full)
        {
            pissEmission.enabled = true;
            shitEmission.enabled = true;
            emptying = true;
            emptyBucketSound.Play();
            GetComponent<Transform>().rotation = new Quaternion(0, 0, 1, 0);
        }
    }
}
