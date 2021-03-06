﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MobileController : MonoBehaviour, IPointerUpHandler
{
    private Image joystickBG;
    public Image joystick;
    public Vector3 inputVector;

    [HideInInspector]
    public Coroutine dragCoroutine;

    [HideInInspector]
    public bool drag = false;

    private void Start()
    {
        joystickBG = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
        
        //joystickBG.gameObject.
    }
    

    //public virtual void OnPointerDown(PointerEventData ped)
    //{
       // OnDrag(ped);
    //}

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public IEnumerator OnDrag(PointerEventData ped)
    {
        if (!(StaticField.gameLosed || StaticField.gameWinned))
        {
            if (drag)
            {
                Vector2 pos;
                if (joystickBG != null && RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, ped.position, ped.pressEventCamera, out pos))
                {
                    pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);
                    pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.x);

                    inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
                    inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                    joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));
                    yield return new WaitForSeconds(0.01f);
                    //if (!joystickBG.IsActive())
                    dragCoroutine = StartCoroutine(OnDrag(ped));
                }
            }
        }
    }

    public float Horizontal()
    {
        if (inputVector.x != 0) return inputVector.x;
        else return Input.GetAxis("Horizontal");
    }
    public float Vertical()
    {
        if (inputVector.y != 0) return inputVector.y;
        else return Input.GetAxis("Vertical");
    }
}