using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryCorridor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D c){
        Debug.Log("Adding force");
        c.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-20, 0));
    }
}
