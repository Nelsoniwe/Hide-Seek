using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Transform Camera;
    private Vector3 velocity = Vector3.zero;
    private Vector3 Offset;
    // Start is called before the first frame update
    void Start()
    {
        Offset = Camera.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Camera.position = Vector3.SmoothDamp(transform.position, target.position+Offset,ref velocity, 0.3f);
    }
}
