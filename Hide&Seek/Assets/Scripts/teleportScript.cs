using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class teleportScript : MonoBehaviour
{
    private GameObject[] teleports;
    private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        teleports = GameObject.FindGameObjectsWithTag("teleport");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            int ran;
            do
            {
                ran = Random.Range(0, teleports.Length);
            } while (teleports[ran].gameObject == this.gameObject);


            //teleports[ran].SetActive(false);

            StartCoroutine(StopTeleportForSeconds(teleports[ran].GetComponent<teleportScript>(), 1));
            //other.transform.position = new Vector3(teleports[ran].transform.position.x, teleports[ran].transform.position.y+2,teleports[ran].transform.position.z);

            if (other.GetComponent<NavMeshAgent>() != null)
                other.GetComponent<NavMeshAgent>().transform.position = new Vector3(teleports[ran].transform.position.x, teleports[ran].transform.position.y + 1, teleports[ran].transform.position.z);
            if (other.GetComponent<CharacterController>() != null)
            {
                other.GetComponent<CharacterController>().enabled = false;
                other.GetComponent<CharacterController>().transform.position = new Vector3(teleports[ran].transform.position.x, teleports[ran].transform.position.y + 1, teleports[ran].transform.position.z);
                other.GetComponent<CharacterController>().enabled = true;
            }
         //   Debug.Log(other);
        }
    }

    private IEnumerator StopTeleportForSeconds(teleportScript obj,float sec)
    {
        obj.isActive = false;
        yield return new WaitForSeconds(sec);
        obj.isActive = true;
    }
}
