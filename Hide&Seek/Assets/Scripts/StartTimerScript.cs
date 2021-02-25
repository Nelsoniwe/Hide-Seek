using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimerScript : MonoBehaviour
{
    public Text Text;
    private float time = 5;
    Vector3 oldOffset;
    Vector3 NewOffset;
    void Update()
    {
        Text.text = Convert.ToString(Convert.ToInt32(time));
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Text.text = "";
            this.enabled = false;
            StaticField.gameStarted = true;
        }
    }



    private void OnEnable()
    {
        CameraFollow cF = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        GameObject Seeker = GameObject.FindGameObjectWithTag("HunterPlayer");
        oldOffset = cF.Offset;
        NewOffset = new Vector3(Seeker.transform.position.x, Seeker.transform.position.y+6, Seeker.transform.position.z-2);
        cF.Offset = NewOffset;
    }

    private void OnDisable()
    {
        CameraFollow cF = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        cF.Offset = oldOffset;
    }
}
