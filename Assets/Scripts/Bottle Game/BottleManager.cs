using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BottleManager : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;

    [SerializeField] private Rigidbody2D rigidBody;
    
    //first fame
    public void Start()
    {
        
    }

    private void Update()
    {
        throw new NotImplementedException();
    }

    private void OnMouseDown()
    {
        difference = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
        
    }

    private void OnMouseUp()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        throw new NotImplementedException();
    }
}
