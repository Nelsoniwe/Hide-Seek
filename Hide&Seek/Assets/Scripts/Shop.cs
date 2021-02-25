using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject ItemTemplate;
    [SerializeField] GameObject ShopItem;
    [SerializeField] Transform ShopScrollView;


    GameObject g;
    [SerializeField] GameObject SkinSpawner;
    HumansSpawner humansSpawner;

    // Start is called before the first frame update
    void Start()
    {


        //print(PlayerPrefs.GetString("Skin Selecasdted"));

        humansSpawner = SkinSpawner.GetComponentInChildren<HumansSpawner>();


        if (PlayerPrefs.GetString("Skin Selected") == "")
        {
            PlayerPrefs.SetString("Skin Selected", humansSpawner.Skins[0].GetComponentInChildren<SkinParams>().NameSkin);
            PlayerPrefs.SetInt(humansSpawner.Skins[0].GetComponentInChildren<SkinParams>().NameSkin + " Buyed", 1);
        }

        

        //ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        ItemTemplate = ShopItem;
        for (int i = 0; i < humansSpawner.Skins.Count; i++)
        {
            string nameSkin = humansSpawner.Skins[i].GetComponentInChildren<SkinParams>().NameSkin;
            int price = humansSpawner.Skins[i].GetComponentInChildren<SkinParams>().Price;
            
            g = Instantiate(ShopItem, ShopScrollView);
            Transform button = ChildObjManager.GetChildObject(g.transform, "BuyButton");

            Debug.Log(button.GetComponentInChildren<Text>().text);

            if (PlayerPrefs.GetInt(nameSkin + " Buyed")==1)
            {
                if (PlayerPrefs.GetString("Skin Selected") == nameSkin)
                {
                    button.GetComponentInChildren<Text>().text = "Selected";
                    StaticField.SkinSelected = nameSkin;
                }
                else
                {
                    
                    button.GetComponentInChildren<Text>().text = "Select";
                }
            }
            else
            {
                
                button.GetComponentInChildren<Text>().text = Convert.ToString(price);
            }

            GameObject s = Instantiate(humansSpawner.Skins[i], g.transform);
            s.transform.localScale = new Vector3(150,150,150);
            s.transform.localPosition = new Vector3(0, -90, -50);
            s.transform.localRotation = Quaternion.Euler(0,-180,0);

            button.GetComponentInChildren<BuySkinButtonController>().Skin = s;


            Transform transform = ChildObjManager.GetChildObject(s.transform,"Body");
            transform.gameObject.layer = 20;
        }
        //Destroy(ItemTemplate);
    }
}
