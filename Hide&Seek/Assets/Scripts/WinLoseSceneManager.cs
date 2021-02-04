using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseSceneManager : MonoBehaviour
{
    public GameObject NextLevelButton;
    public GameObject RestartButton;
    public GameObject JoyStickPanel;
    // Start is called before the first frame update
    void Start()
    {
       // CheckWinLose();
        StartCoroutine(CheckWinLose());
    }



    // Update is called once per frame
    IEnumerator CheckWinLose()
    {
        if (StaticField.gameWinned && !StaticField.gameLosed)
        {
            RestartButton.SetActive(true);
            NextLevelButton.SetActive(true);
        }
        if (StaticField.isPlayerCathced || StaticField.gameLosed && !StaticField.gameWinned)
        {
            RestartButton.SetActive(true);
        }
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(CheckWinLose());
        //  else
        //JoyStickPanel.SetActive(true);
    }
}
