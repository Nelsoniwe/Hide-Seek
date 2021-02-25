using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseSceneManager : MonoBehaviour
{
    public GameObject NextLevelButton;
    public GameObject RestartButton;
    public GameObject JoyStickPanel;

    public GameObject WinScreen;
    public GameObject LooseScreen;
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
            WinScreen.gameObject.SetActive(true);
            RestartButton.SetActive(true);
            NextLevelButton.SetActive(true);
        }
        if ((StaticField.isPlayerCathced || StaticField.gameLosed) && !StaticField.gameWinned)
        {
            LooseScreen.gameObject.SetActive(true);
            RestartButton.SetActive(true);
        }
        else if(!StaticField.gameWinned)
        {
            LooseScreen.gameObject.SetActive(false);
            RestartButton.SetActive(false);
        }
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(CheckWinLose());
        //  else
        //JoyStickPanel.SetActive(true);
    }
}
