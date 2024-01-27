using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BottleManager : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;

    [SerializeField] private Transform transform;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private ParticleSystem particleSystem;
    
    //first fame
    public void Start()
    {
        
    }

    private void Update()
    {
        var emission = particleSystem.emission;
        if (transform.rotation.eulerAngles.z > 125 && transform.rotation.eulerAngles.z < 235)
        {
            emission.enabled = true;
        }
        else
        {
            emission.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        difference = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) transform.position;
        rigidBody.velocity = new Vector2(0, 0);
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
        
    }

    private void OnMouseUp()
    {
    }
}
