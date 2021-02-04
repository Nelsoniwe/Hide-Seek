using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponentInChildren<Camera>();
        camera.enabled = false;
        camera.enabled = true;
    }

  
}
