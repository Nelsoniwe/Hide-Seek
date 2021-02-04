using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimerScript : MonoBehaviour
{
    public Text Text;
    private float time = 5;
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
}
