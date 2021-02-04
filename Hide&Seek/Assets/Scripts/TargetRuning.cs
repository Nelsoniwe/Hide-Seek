using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Money
{
    public static Vector3[] coinPlacesTransform;
}



public class TargetRuning : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;
    public GameObject hunter;
    Vector3 runTo;
    Vector3 tr;
    float timer = -1;
    NavMeshPath path;

    public Text Text;

    public Material Cathed;

    public LayerMask targetMask;
    public Collider[] NearestHiders;

    public GameObject cage;
    // private List<Transform> NearestHiders = new List<Transform>();

    private bool isRunningToCoin = false;
    private Vector3 CoinToRun = new Vector3();

    private GameObject[] coinPlaces = new GameObject[5];
    private GameObject[] catchedPlayer = new GameObject[5];
    private Vector3[] coinPlacesTransform;
    private Vector3[] catchedPlayerTransform;


    private Vector3 newPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HunterPlayer") || other.CompareTag("Hunter"))
        {
            if (this.tag == "HiderPlayer" || this.tag == "VisibleHiderPlayer")
            {
                this.GetComponent<CharacterController>().enabled = false;
                this.GetComponent<CharacterMechanics>().enabled = false;
                //print(gameObject);
                this.tag = "CatchedPlayer";
                StaticField.isPlayerCathced = true;
                cage.SetActive(true);
            }
            else if (this.tag == "Hider" || this.tag == "VisibleHider")
            {

                //print(gameObject);
                this.tag = "Catched";

                if (agent != null)
                    agent.enabled = false;
                cage.SetActive(true);
            }

            gameObject.GetComponent<Renderer>().material.color = Color.green;
            // gameObject.GetComponent<MeshRenderer>().enabled = true;
            //if (GameObject.FindGameObjectsWithTag("Hider").Length == 0)
            //{
            //    Text.text = "YOU WON";
            //    Text.gameObject.SetActive(true);
            //    StartCoroutine(StopforSecondsAndRestart(3));
            //}
        }
        else if (agent != null && (this.CompareTag("Catched") || this.CompareTag("CatchedPlayer")) && (other.CompareTag("HiderPlayer") || other.CompareTag("Hider") || other.tag == "VisibleHider" || other.tag == "VisibleHiderPlayer"))
        {
            agent.enabled = true;
            //print(gameObject);
            this.tag = "Hider";
            //   gameObject.GetComponent<Renderer>().material.color = Color.green;
            //   gameObject.GetComponent<MeshRenderer>().enabled = true;
            cage.SetActive(false);
        }
        if ((other.tag == "Hider"|| other.tag == "VisibleHider") && this.tag == "CatchedPlayer")
        {
            this.tag = "HiderPlayer";
            StaticField.isPlayerCathced = false;
            this.GetComponent<CharacterController>().enabled = true;
            this.GetComponent<CharacterMechanics>().enabled = true;
            cage.SetActive(false);
        }
    }

    public IEnumerator StopforSecondsAndRestart(int sec)
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene("SampleScene");
    }

    void Update()
    {
        RunFrom();
    }

    private void Start()
    {
        //agent.autoRepath = true;
        coinPlaces = GameObject.FindGameObjectsWithTag("coin");
        catchedPlayer = GameObject.FindGameObjectsWithTag("Catched");
        Money.coinPlacesTransform = new Vector3[coinPlaces.Length];
        catchedPlayerTransform = new Vector3[catchedPlayer.Length];
        for (int i = 0; i < coinPlaces.Length; i++)
        {
            Money.coinPlacesTransform[i] = coinPlaces[i].transform.position;
        }
        for (int i = 0; i < catchedPlayer.Length; i++)
        {
            catchedPlayerTransform[i] = catchedPlayer[i].transform.position;
        }
        path = new NavMeshPath();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(newPos, 2);
    }

    void RunFrom()
    {
        if (timer < 0f && agent.enabled)
        {
            //  NavMeshHit hit;
            // runTo = Vector3.Cross(hunter.transform.position, agent.transform.position);

            // var huntera = GameObject.FindWithTag("Hunter").transform;
            GameObject[] seekers = GameObject.FindGameObjectsWithTag("Hider");
            float distance = Vector3.Distance(this.transform.position, hunter.transform.position);

            // Vector3 newPoint = new Vector3(Random.Range(-65, 65), 2, Random.Range(-65, 65));
            // if (distance < 10)

            Vector3 dirToPlayer = transform.position - hunter.transform.position;
            // newPos = this.transform.position + dirToPlayer;

            RaycastHit raycastHit;
            Ray Ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));


            NearestHiders = Physics.OverlapSphere(transform.position, 3, targetMask);
            //  Debug.Log(this);

            if (Physics.Raycast(Ray, out raycastHit))  //тут происходит настройка поведения бота
            {
                // Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward));
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastHit.distance);
               // NavMesh.CalculatePath(transform.position, newPos, NavMesh.AllAreas, path);

                RaycastHit raycastHitSphere;




                if (raycastHit.distance < 1 && distance < 2 && this.gameObject.tag == "VisibleHider"/*GetComponent<MeshRenderer>().enabled*/ /*&& path.status != NavMeshPathStatus.PathComplete*/)
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.red);
                    transform.rotation.eulerAngles.Set(transform.rotation.x, transform.rotation.y + 10, 0);
                    agent.ResetPath();
                    // newPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

                    int dis = 0;

                    foreach (var item in seekers)
                    {
                        if (Vector3.Distance(this.transform.position, item.transform.position) > dis)
                        {
                            dis = (int)Vector3.Distance(this.transform.position, item.transform.position);
                            newPos = item.transform.position;
                            timer = 0.5f;
                        }
                    }
                    isRunningToCoin = false;
                    // Debug.Log(this);
                }
                else if (distance < 5)
                {
                    newPos = this.transform.position + dirToPlayer;
                    // print(this + " " + distance);
                    isRunningToCoin = false;
                    // timer = 0.5f;
                }
                else if (!isRunningToCoin)
                {
                    GameObject[] catchedbots = GameObject.FindGameObjectsWithTag("Catched");
                    GameObject[] catchedPlayerHelpArr = GameObject.FindGameObjectsWithTag("CatchedPlayer");
                    catchedPlayer = new GameObject[catchedbots.Length + catchedPlayerHelpArr.Length];
                    catchedbots.CopyTo(catchedPlayer, 0);
                    catchedPlayerHelpArr.CopyTo(catchedPlayer, catchedbots.Length);

                    if (Random.Range(0, 3) == 0 && coinPlaces.Length > 0)
                    {
                        CoinToRun = Money.coinPlacesTransform[Random.Range(0, coinPlaces.Length - 1)];
                        newPos = CoinToRun;
                        isRunningToCoin = true;
                    }
                    else if (catchedPlayer.Length > 0)
                    {
                        CoinToRun = catchedPlayer[Random.Range(0, catchedPlayer.Length - 1)].transform.position;
                        newPos = CoinToRun;
                        isRunningToCoin = true;
                    }


                    // Debug.Log(agent.pathStatus);
                }
                else if (CoinToRun != null && Vector3.Distance(this.transform.position, CoinToRun) < 2)
                {
                    isRunningToCoin = false;
                }





                //else if (NearestHiders.Length > 1 || distance < 18)
                //{
                //    if (NearestHiders[0].gameObject != this.gameObject)
                //    {
                //        newPos = this.transform.position + (transform.position - NearestHiders[0].transform.position);
                //        //print(this + " " + distance+" "+NearestHiders[0].gameObject);

                //    }
                //    else
                //    {
                //        newPos = this.transform.position + (transform.position - NearestHiders[1].transform.position);
                //        // print(this + " " + distance + " " + NearestHiders[1].gameObject);
                //    }

                //    //Physics.SphereCast(this.transform.position, 2, transform.forward, out raycastHit, 10)
                //    // if (raycastHit.transform.gameObject.)
                //    // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.yellow);
                //    //Debug.DrawRay(transform.position, raycastHit.transform.gameObject.transform.position, Color.yellow);
                //    timer = 0.1f;
                //    isRunningToCoin = false;
                //}

                

                agent.SetDestination(newPos);




                //if (distance < 25)
                //{
                //    newPos = transform.position - hunter.transform.position;
                //   // print(this + " " + distance);
                //    isRunningToCoin = false;
                //}



                //if (agent.pathStatus == NavMeshPathStatus.PathComplete)
                //{
                //    isRunningToCoin = false;
                //}


            }

        }
        timer -= Time.deltaTime;
    }
}
