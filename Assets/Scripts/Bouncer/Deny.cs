using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deny : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c){
        if(c.gameObject.GetComponent<ID>() != null){
            Debug.Log("ID Denied");
        }
    }
}