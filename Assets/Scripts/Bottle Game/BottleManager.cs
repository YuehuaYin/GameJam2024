using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class BottleManager : MonoBehaviour
{
    private Vector2 difference = Vector2.zero;
    private bool holdMeDaddy = false;
    private Random random = new Random();

    [SerializeField] private Transform transform;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Vector3 returnPosition;

    public float speed = 20f;

    public float gravity = 0.5f;

    //first fame
    public void Start()
    {
        
    }

    private void Update()
    {
        var emission = particleSystem.emission;
        if (transform.rotation.eulerAngles.z > 125 && transform.rotation.eulerAngles.z < 235 && holdMeDaddy)
        {
            emission.enabled = true;
        }
        else
        {
            emission.enabled = false;
        }
        
        
        if (transform.localPosition.x < -0.84 || transform.localPosition.x > 0.83 || transform.localPosition.y < -0.45)
        {
            transform.localPosition = returnPosition;
            transform.rotation = Quaternion.identity;
            rigidBody.velocity = new Vector2(0, 0);
            rigidBody.gravityScale = 0;
            holdMeDaddy = false;
            difference = new Vector2(0,0);
            rigidBody.angularVelocity = 0;
        }
    }

    private void FixedUpdate()
    {
        if (holdMeDaddy)
        {
            difference = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) transform.position;
            rigidBody.AddForce(difference * speed);
        }
    }

    private void OnMouseDown()
    {
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.angularVelocity = 0;
        rigidBody.gravityScale = gravity;
        holdMeDaddy = true;
    }

    private void OnMouseDrag()
    {
    }

    private void OnMouseUp()
    {
        if (holdMeDaddy)
        {
            if (random.NextDouble() > 0.5)
            {
                rigidBody.angularVelocity = (float) (random.NextDouble() * 30 + 100);
            }
            else
            {
                rigidBody.angularVelocity = -(float) (random.NextDouble() * 30 + 100);
            }

            holdMeDaddy = false;
            difference = new Vector2(0, 0);
        }
    }
}
