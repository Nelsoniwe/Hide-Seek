﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Transform Camera;
    private Vector3 velocity = Vector3.zero;
    private Vector3 velocityRotate = Vector3.zero;
    [HideInInspector]
    public Vector3 Offset;

    [HideInInspector]
    public Vector3 CameraStartPos;
    //private void Awake()
    //{
    //    PlayerPrefs.DeleteAll();
    //    //  PlayerPrefs.SetInt("Coins", 1000);
    //}

    private Quaternion CameraRotation;
    // Start is called before the first frame update
    void Start()
    {
        Offset = Camera.position - target.position;
        CameraRotation = Camera.rotation;
        CameraStartPos = this.transform.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (StaticField.ChoosedPlay != ChoosePlay.none)
        {
            Camera.rotation = SmoothDampQuaternion(Camera.rotation, CameraRotation, ref velocityRotate, 0.2f);
            Camera.position = Vector3.SmoothDamp(transform.position, target.position + Offset, ref velocity, 0.3f);
        }
    }

    public static Quaternion SmoothDampQuaternion(Quaternion current, Quaternion target, ref Vector3 currentVelocity, float smoothTime)
    {
        Vector3 c = current.eulerAngles;
        Vector3 t = target.eulerAngles;
        return Quaternion.Euler(
          Mathf.SmoothDampAngle(c.x, t.x, ref currentVelocity.x, smoothTime),
          Mathf.SmoothDampAngle(c.y, t.y, ref currentVelocity.y, smoothTime),
          Mathf.SmoothDampAngle(c.z, t.z, ref currentVelocity.z, smoothTime)
        );
    }
}
