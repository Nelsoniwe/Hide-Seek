using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI text;
    private float time = 60;

    private int catchedCaunter=0;
    // Update is called once per frame
    private List<GameObject> hiders;
    private void Start()
    {
        hiders = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hider"));
        hiders.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("HiderPlayer")));
    }

    void Update()
    {
        foreach (var item in hiders)
        {
            if (item.CompareTag("Catched") || item.CompareTag("CatchedPlayer"))
                catchedCaunter++;
        }

        if (StaticField.gameStarted&&!StaticField.gameLosed&&!StaticField.gameWinned)
        {
            text.text = Convert.ToString(Convert.ToInt32(time));
            time -= Time.deltaTime;
            if (time <= 0)
            {
                StaticField.gameStarted = false;

                if (StaticField.ChoosedPlay == ChoosePlay.seek)
                    StaticField.gameLosed = true;
                else
                    StaticField.gameWinned = true;
            }
        }

        if (catchedCaunter>=6 && StaticField.ChoosedPlay == ChoosePlay.seek)
        {
            StaticField.gameStarted = false;
            StaticField.gameWinned = true;
        }
        catchedCaunter = 0;
    }
}
