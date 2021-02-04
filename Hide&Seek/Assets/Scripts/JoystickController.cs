using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Objecta;
    public MobileController mobileController;
    public PointerEventData pointer;

    public void OnPointerDown(PointerEventData eventData)
    {
        mobileController.drag = true;
       // if (pointer == null)
       // {
          //  pointer = eventData;
        //}
        // Objecta.SetActive(true);
        Objecta.transform.GetComponent<RectTransform>().anchoredPosition = eventData.pointerCurrentRaycast.screenPosition - new Vector2(Objecta.transform.GetComponent<RectTransform>().rect.width / 2, Objecta.transform.GetComponent<RectTransform>().rect.height / 2);
        //print(eventData.pointerCurrentRaycast.screenPosition.x + " " + eventData.pointerCurrentRaycast.screenPosition.y);
        StartCoroutine(mobileController.OnDrag(eventData));
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //if (mobileController.joystick != null)
        //{
            mobileController.inputVector = Vector2.zero;
            mobileController.joystick.rectTransform.anchoredPosition = Vector2.zero;

            //Objecta.SetActive(false);
            Objecta.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1000, -1000, 0);
            //StopCoroutine(mobileController.dragCoroutine);
            mobileController.drag = false;
            
            //Objecta.transform.localPosition = new Vector3(-1000, -1000, 0);
        //}
    }
}
