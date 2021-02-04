using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoosterScript : MonoBehaviour
{
    public int speed;
    public float time;
    public GameObject Icon;

    private bool isBusterRunning = false;
    private void OnTriggerEnter(Collider other)
    {
            SlowerObj slowerObj = new SlowerObj(other.gameObject);
            StartCoroutine(Booster(slowerObj, speed, time));
    }

    public IEnumerator Booster(SlowerObj slowerObj, int speed, float time)
    {
        if (!isBusterRunning)
        {
            if (/*slowerObj.obj.CompareTag("HiderPlayer") || */slowerObj.obj.GetComponent<CharacterMechanics>()!=null&&(slowerObj.obj.GetComponent<CharacterMechanics>().isActiveAndEnabled))
            {
                isBusterRunning = true;
                Icon.SetActive(false);
            //    if (slowerObj.obj.GetComponent<NavMeshAgent>() != null)
                  //  slowerObj.obj.GetComponent<NavMeshAgent>().speed += speed;
                //else
                    slowerObj.obj.GetComponent<CharacterMechanics>().speedMove += speed;


                yield return new WaitForSeconds(time);


                //if (slowerObj.obj.GetComponent<NavMeshAgent>() != null)
                //    slowerObj.obj.GetComponent<NavMeshAgent>().speed -= speed;
                //else
                    slowerObj.obj.GetComponent<CharacterMechanics>().speedMove -= speed;

                //this.gameObject.SetActive(false);
                Destroy(this);
            }

        }
    }
}
