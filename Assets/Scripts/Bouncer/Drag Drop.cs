using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    [SerializeField] private GameObject go;

    private Transform tf;
    private Rigidbody2D rb;

    private bool followingMouse = false;
    [SerializeField] private float speed;

    [SerializeField] private float mouseUpDrag;

    [SerializeField] private float mouseDownDrag;

    [SerializeField] private float pickupScaleMult;

    private Vector2 difference = Vector2.zero;

    private Vector3 baseScale;
    
    public void Awake() {
        rb = go.GetComponent<Rigidbody2D>();
        tf = go.GetComponent<Transform>();
    }

    //first frame
    public void Start()
    {
        baseScale = transform.localScale;
    }

    private void Update()
    {

    }

    private void FixedUpdate() {
        if (followingMouse) {
            difference = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) transform.position;
            rb.AddForce(difference * speed);
            rb.drag = mouseDownDrag;
            transform.rotation = Quaternion.identity;
            rb.angularVelocity = 0;

            transform.localScale = pickupScaleMult * baseScale;
        } else {
            rb.drag = mouseUpDrag;
            transform.localScale = baseScale;
        }
    }

    private void OnMouseDown()
    {
        followingMouse = true;
        //difference = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) tf.position;
        //rb.velocity = new Vector2(0, 0);
    }

    private void OnMouseDrag()
    {
        //difference = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) tf.position;
        //tf.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
        
    }

    private void OnMouseUp()
    {
        followingMouse = false;
        difference = new Vector2(0,0);
    }
}
