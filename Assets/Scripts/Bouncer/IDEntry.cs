using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDEntry : MonoBehaviour
{
    [SerializeField] private AudioSource swoosh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D c){
        ID id;
        if((id = c.gameObject.GetComponent<ID>()) != null){
            Debug.Log("ID entered scene");
            swoosh.Play();
        }
    }
}
