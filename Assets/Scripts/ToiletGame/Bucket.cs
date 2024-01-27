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
    [SerializeField] private float timeToEmpty;
    private float timeSpentEmptying;
    private bool full;
    [SerializeField] private float timelimit;
    private float gameTime;
    //first fame
    public void Start()
    {
        originalPos = GetComponent<Transform>().position;
        piss.enableEmission = false;
        shit.enableEmission = false;
        gameTime = 0;
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
        Debug.Log("helo");
        if (collision.gameObject.tag == "drain")
        {
            EnterDrain();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("helo");
        if (collision.gameObject.tag == "drain")
        {
            EnterDrain();
        }
    }
    private void finishEmptying()
    {
        piss.enableEmission = false;
        shit.enableEmission = false;
        emptying = false;
        full = false;
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
            piss.enableEmission = true;
            shit.enableEmission = true;
            emptying = true;
        }
    }
}
