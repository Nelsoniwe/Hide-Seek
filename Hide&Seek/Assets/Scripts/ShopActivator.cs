using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopActivator : MonoBehaviour
{
    [SerializeField]
    GameObject ShopPanel;
    [SerializeField]
    GameObject ChoosePanel;


    public void ChangeShop()
    {
        if (ShopPanel.active)
        {
            ShopPanel.SetActive(false);
            ChoosePanel.SetActive(true);
        }
        else
        {
            ShopPanel.SetActive(true);
            ChoosePanel.SetActive(false);
        }
        //this.transform.gameObject.SetActive(false);
        
    }
}
