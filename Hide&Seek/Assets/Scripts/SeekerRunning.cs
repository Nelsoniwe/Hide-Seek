using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeekerRunning : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;
    NavMeshPath path;


    public LayerMask targetMask;
    public Collider[] NearestHiders;

    private bool isRunningToCoin = false;
    private Vector3 CoinToRun = new Vector3();

    private GameObject[] coinPlaces = new GameObject[5];
    private Vector3[] coinPlacesTransform;

    private Vector3 newPos;

    private GameObject LastVisibleHider;

    bool isStartetHunter = false;

    void Update()
    {
        // if(isStartetHunter)

    }

    private void Start()
    {
        //agent.autoRepath = true;
        coinPlaces = GameObject.FindGameObjectsWithTag("coin");
        coinPlacesTransform = new Vector3[coinPlaces.Length];
        for (int i = 0; i < coinPlaces.Length; i++)
        {
            coinPlacesTransform[i] = coinPlaces[i].transform.position;
        }
        path = new NavMeshPath();
        StartCoroutine("waitForHunterStart");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(newPos, 2);
    }

    public IEnumerator waitForHunterStart()
    {
        GameObject.FindGameObjectsWithTag("HunterPlayer")[0].GetComponent<FOV>().viewRadius = 0;
        yield return new WaitForSeconds(5);
        GameObject.FindGameObjectsWithTag("HunterPlayer")[0].GetComponent<FOV>().viewRadius = 10;
        isStartetHunter = true;
        StartCoroutine(RunTo());
    }



    IEnumerator RunTo()
    {
        {
            Vector3 targetPos = new Vector3();
            List<GameObject> hiders = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hider"));

            hiders.AddRange(GameObject.FindGameObjectsWithTag("HiderPlayer"));

            List<GameObject> visibleHiders = new List<GameObject>(GameObject.FindGameObjectsWithTag("VisibleHider"));
            visibleHiders.AddRange(GameObject.FindGameObjectsWithTag("VisibleHiderPlayer"));

            


            if (LastVisibleHider == null || visibleHiders.Count > 0)
            {
                if (visibleHiders.Count > 0)
                {
                    float newDis = 0;
                    float oldDis = 100;
                    for (int i = 0; i < visibleHiders.Count; i++) //find nearest visible hider
                    {
                        newDis = Vector3.Distance(visibleHiders[i].transform.position, agent.transform.position);

                      //  Ray ray = new Ray(visibleHiders[i].transform.position, agent.transform.position);
                        

                        if (newDis < oldDis)
                        {
                             oldDis = newDis;
                             

                            //if (oldDis<= 5)
                            //{
                                targetPos = visibleHiders[i].transform.position;
                           // }

                             LastVisibleHider = visibleHiders[i];
                        }
                    }
                   // Debug.Log(oldDis);
                }
                //else if(LastVisibleHider != null && !LastVisibleHider.CompareTag("Catched") && !LastVisibleHider.CompareTag("CatchedPlayer"))
                //{
                //    targetPos = LastVisibleHider.transform.position;
                //}
                //else
                //{
                //    LastVisibleHider = null;
                //    targetPos = hiders[0].transform.position;
                //}


                // Vector3 newPoint = new Vector3(Random.Range(-65, 65), 2, Random.Range(-65, 65));
                // if (distance < 10)


                // newPos = this.transform.position + dirToPlayer;

                //Debug.Log(LastVisibleHider);
            }
            if (LastVisibleHider != null && (LastVisibleHider.CompareTag("VisibleHider") || LastVisibleHider.CompareTag("VisibleHiderPlayer")))
            {
                targetPos = LastVisibleHider.transform.position;
            }
            else 
            {
                LastVisibleHider = null;
                targetPos = hiders[0].transform.position;
            }


            agent.SetDestination(targetPos);
            //if (distance < 25)
            //{
            //    newPos = transform.position - hunter.transform.position;
            //   // print(this + " " + distance);
            //    isRunningToCoin = false;
            //}

            yield return new WaitForSeconds(0.3f);

            //if (agent.pathStatus == NavMeshPathStatus.PathComplete)
            //{
            //    isRunningToCoin = false;
            //}



            StartCoroutine(RunTo());


        }
        // timer -= Time.deltaTime;
    }
}
