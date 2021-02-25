using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySkinButtonController : MonoBehaviour
{
    public GameObject Skin;
    private string name;

    private Text text;

    public void Start()
    {
        text = this.GetComponentInChildren<Text>();
        name = Skin.GetComponentInChildren<SkinParams>().NameSkin;
    }

    public void Update()
    {
        if (PlayerPrefs.GetString("Skin Selected") == name)
        {
            text.text = "Selected";
        }
        else if(PlayerPrefs.GetInt(name + " Buyed") == 1)
        {
            text.text = "Select";
        }
    }

    public void buySkin()
    {
        if (this.GetComponentInChildren<Text>().text == "Select")
        {
            PlayerPrefs.SetInt(StaticField.SkinSelected + " Selected",0);
            // PlayerPrefs.SetInt(name + " Selected", 1);
            PlayerPrefs.SetString("Skin Selected", name);
            this.GetComponentInChildren<Text>().text = "Selected";
        }
        else if (this.GetComponentInChildren<Text>().text == "Selected")
        {

        }
        else
        {
            if (PlayerPrefs.GetInt("Coins") >= Convert.ToInt32(this.GetComponentInChildren<Text>().text))
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - Convert.ToInt32(this.GetComponentInChildren<Text>().text));
                this.GetComponentInChildren<Text>().text = "Select";
                PlayerPrefs.SetInt(name + " Buyed", 1);
            }
        }
    }
}
