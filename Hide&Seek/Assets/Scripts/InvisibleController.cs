using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleController : MonoBehaviour
{
    private Transform Skin;
    private Vector3 oldPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(tagChecker());
        StaticField.ResetStaticFields();
    }

    IEnumerator tagChecker()
    {
        if (StaticField.hideSkins)
        {
            if (Skin == null)
            {
                GetChildObject(transform, "Body");
                if (Skin != null)
                    oldPos = Skin.localPosition;
            }
            if (Skin != null)
            {
                if (this.CompareTag("Hider"))
                {
                    Skin.gameObject.layer = LayerMask.NameToLayer("nonVisible");
                }
                else
                {
                    Skin.gameObject.layer = LayerMask.NameToLayer("Default");
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(tagChecker());

    }

    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == (_tag))
            {
                Skin = (child);
            }
            else if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }
}

public static class StaticField
{
    public static void ResetStaticFields()
    {
        isPlayerCathced = false;
        hideSkins = false;
        gameStarted = false;
        gameWinned = false;
        gameLosed = false;
        ChoosedPlay = new ChoosePlay();
    }

    public static bool isPlayerCathced = false;
    public static bool hideSkins = false;
    public static bool gameStarted = false;
    public static bool gameWinned = false;
    public static bool gameLosed = false;
    public static ChoosePlay ChoosedPlay = new ChoosePlay();
}



public enum ChoosePlay
{
    hide,seek
}
