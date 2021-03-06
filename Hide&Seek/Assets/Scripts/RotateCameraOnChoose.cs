﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraOnChoose : MonoBehaviour
{
    public GameObject Center;

    [SerializeField]
    float speed;

    private Vector3 aroundWhat;
    // Start is called before the first frame update
    void Start()
    {
        aroundWhat = new Vector3(Center.transform.position.x, this.transform.position.y, Center.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(StaticField.ChoosedPlay == ChoosePlay.none)
        transform.RotateAround(aroundWhat,Vector3.up, speed);
        else
        {
            this.enabled = false;
        }
    }
}
