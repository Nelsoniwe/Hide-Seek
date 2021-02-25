using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCountVisualisationUI : MonoBehaviour
{
   // Text text;
    public TextMeshProUGUI text;
 //   void Start()
 //   {
        //text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    //    StartCoroutine(CoinCounterCoroutine());
   // }

    private void OnEnable()
    {
        StartCoroutine(CoinCounterCoroutine());
    }

    IEnumerator CoinCounterCoroutine()
    {
      //  Debug.Log("Started Coroutine at timestamp : " + Time.time);
        text.text = Convert.ToString(PlayerPrefs.GetInt("Coins"));
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(CoinCounterCoroutine());
    }
}
