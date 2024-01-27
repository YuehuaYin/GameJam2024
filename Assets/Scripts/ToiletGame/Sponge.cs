using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sponge : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;
    private Vector2 originalPos;

    //first fame
    public void Start()
    {
        originalPos = GetComponent<Transform>().position;
    }

    private void Update()
    {
       
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
}
