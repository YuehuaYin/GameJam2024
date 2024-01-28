using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main; 
        camera.transform.position = new Vector3(0,(4-1) * -0.25f,-10);

    }

    // Update is called once per frame
    void Update()
    {
        float cameraZoom = camera.orthographicSize - Input.mouseScrollDelta.y;
        camera.orthographicSize = Mathf.Clamp(cameraZoom,2,10);
    }
}
