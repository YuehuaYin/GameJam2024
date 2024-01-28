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

    private ParticleSystem.EmissionModule pissEmission;
    private ParticleSystem.EmissionModule shitEmission;
    //first fame
    public void Start()
    {
        originalPos = GetComponent<Transform>().position;
        piss.enableEmission = false;
        shit.enableEmission = false;
        gameTime = 0;
        pissEmission = piss.emission;
        shitEmission = shit.emission;
    }
    public void resetBucket()
    {
        piss.enableEmission = false;
        shit.enableEmission = false;
        full = true;
        timeSpentEmptying = 0;
        emptying = false;

    }
    private void Update()
    {
        gameTime += Time.deltaTime;
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
        GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 0);
        manager.GetComponent<ToiletController>().emptyBucket();
    }
    private void stopEmptying()
    {
        pissEmission.enabled = false;
        shitEmission.enabled = false;
        emptying = false;
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
        GetComponent<Transform>().position = new Vector3(originalPos.x, originalPos.y, 0);
    }
    private void EnterDrain()
    {
        if (full)
        {
            pissEmission.enabled = true;
            shitEmission.enabled = true;
            emptying = true;
            GetComponent<Transform>().rotation = new Quaternion(0, 0, 1, 0);
        }
    }
}
