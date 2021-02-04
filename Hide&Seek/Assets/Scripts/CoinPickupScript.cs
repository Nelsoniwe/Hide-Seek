using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickupScript : MonoBehaviour
{
    // Start is called before the first frame update

    // private GameObject coin;
    public int value;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HunterPlayer")|| other.CompareTag("HiderPlayer")||other.CompareTag("VisibleHiderPlayer"))
        {
            Destroy(gameObject);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + value);
        }
    }
}
