using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sponge : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;
    private Vector2 originalPos;
    [SerializeField] int bucketDirt = 10;
    private Rigidbody2D rb;
    private bool cleaning = false;
    //first fame
    [SerializeField] GameObject manager;
    public void Start()
    {
        originalPos = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float mouseXSpeed = Input.GetAxis("Mouse X");
        float mouseYSpeed = Input.GetAxis("Mouse Y");
        float mouseSpeed = Mathf.Sqrt(Mathf.Pow(mouseYSpeed, 2) + Mathf.Pow(mouseXSpeed, 2));
       if (cleaning)
        {
            if (bucketDirt == 0)
            {
                Debug.Log("Clean");
                cleaning = false;
                manager.GetComponent<ToiletController>().cleanBucket();
            }
            else
            {
                if (mouseSpeed > 1)
                {
                    Debug.Log("Cleaning");
                    bucketDirt -= 1;
                    cleaning = false;
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
        GetComponent<Transform>().position = new Vector3(originalPos.x, originalPos.y, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bucket")
        {
            cleaning = true;
        }
    }
}
