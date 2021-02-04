using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowerObj
{
    public SlowerObj(GameObject obj)
    {
        this.obj = obj;
    }

    public GameObject obj { get; set; }


    public bool isSlower { get; set; }
    public bool isSlowerRunning { get; set; }
    public bool isStepControllerRunning { get; set; }
}


public static class IsSlower
{
    public static List<GameObject> Hiders = new List<GameObject>();
    public static bool isSlower { get; set; }
}

public class FluidScript : MonoBehaviour
{
    public int speed;
    public float time;
    public GameObject step;



    public ParticleSystem splash;

    private float defaultSpeed;

    private List<SlowerObj> slowerObjs = new List<SlowerObj>();

    private void OnTriggerEnter(Collider other)
    {
        if (!IsSlower.Hiders.Contains(other.gameObject))
        {
            IsSlower.Hiders.Add(other.gameObject);
            IsSlower.isSlower = true;
            bool containsInSlower = false;
            int ind = 0;
            for (int i = 0; i < slowerObjs.Count; i++)
            {
                if (slowerObjs[i].obj.Equals(other.gameObject))
                {
                    containsInSlower = true;
                    ind = i;
                    break;
                }
            }
            SlowerObj slowerObj = new SlowerObj(other.gameObject);
            if (!containsInSlower)
            {
                slowerObjs.Add(slowerObj);
            }




            if (!slowerObj.isSlowerRunning && !slowerObj.isStepControllerRunning && !containsInSlower)
            {
                if (other.GetComponent<NavMeshAgent>() != null)
                    defaultSpeed = other.GetComponent<NavMeshAgent>().speed;
                else
                    defaultSpeed = other.GetComponent<CharacterMechanics>().speedMove;

                StartCoroutine(Slower(slowerObj, speed, time));
                //Debug.Log("asd");
            }

        }
    }

    public IEnumerator Slower(SlowerObj slowerObj, int speed, float time)
    {
        if (!slowerObj.isSlowerRunning)
        {
            slowerObj.isSlowerRunning = true;
            if (step != null)
                StartCoroutine(VisibleTagSpummer(slowerObj));
            if (slowerObj.obj.CompareTag("Hider") || slowerObj.obj.CompareTag("HiderPlayer") || slowerObj.obj.CompareTag("VisibleHider") || slowerObj.obj.CompareTag("VisibleHiderPlayer"))
            {
                if (slowerObj.obj.GetComponent<NavMeshAgent>() != null)
                    slowerObj.obj.GetComponent<NavMeshAgent>().speed += speed;
                else
                    slowerObj.obj.GetComponent<CharacterMechanics>().speedMove += speed;
            }

            StartCoroutine(StepController(slowerObj, step, time));
            yield return new WaitForSeconds(time);

            //if (obj.tag.Equals("Hider") || obj.tag.Equals("HiderPlayer") || obj.tag.Equals("VisibleHider"))
            {
                if (slowerObj.obj.GetComponent<NavMeshAgent>() != null)
                    slowerObj.obj.GetComponent<NavMeshAgent>().speed -= speed;
                else
                    slowerObj.obj.GetComponent<CharacterMechanics>().speedMove -= speed;
                //  Debug.Log(obj.GetComponent<CharacterMechanics>().speedMove);
            }

            //yield return "";


            for (int i = 0; i < slowerObjs.Count; i++)
            {
                if (slowerObjs[i].obj.Equals(slowerObj.obj))
                {
                    slowerObjs.RemoveAt(i);
                    break;
                }
            }

            slowerObj.isSlowerRunning = false;
            IsSlower.Hiders.Remove(slowerObj.obj);
        }
        //time -= Time.deltaTime;
        //Debug.Log(obj.GetComponent<CharacterMechanics>().speedMove);

        // yield return StartCoroutine(Slower(obj,speed,time));

    }

    public IEnumerator StepController(SlowerObj slowerObj, GameObject step, float time)
    {
        if (step != null)
        {
            slowerObj.isStepControllerRunning = true;
            yield return new WaitForSeconds(0.7f);

            RaycastHit hit;
            Ray ray = new Ray(slowerObj.obj.transform.position, slowerObj.obj.transform.TransformDirection(Vector3.down));

            if (Physics.Raycast(ray, out hit))
            {
                Quaternion rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);

                //GameObject obj = Instantiate(projector, hit.point + hit.normal * 0.25f, projectorRotation) as GameObject;

                GameObject stepCopy = Instantiate(step, hit.point + hit.normal * 0.05f, rotation) as GameObject;

                ParticleSystem particle = Instantiate(splash, hit.point + hit.normal * 0.05f, splash.transform.rotation) as ParticleSystem;

                stepCopy.transform.rotation = Quaternion.Euler(stepCopy.transform.rotation.eulerAngles.x, stepCopy.transform.rotation.eulerAngles.y, 360 - slowerObj.obj.transform.rotation.eulerAngles.y);
                // Destroy(projectorsArray[tmpCount]);
                // projectorsArray[tmpCount] = obj;

                // obj.transform.parent = hit.transform;

                //Quaternion randomRotZ = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, Random.Range(0, 360));
                //obj.transform.rotation = randomRotZ;
                if (slowerObj.isSlowerRunning)
                    yield return StartCoroutine(StepController(slowerObj, step, time));

                yield return new WaitForSeconds(1);
                Destroy(particle);
                // if (tmpCount == maxProjectors - 1) tmpCount = 0; else tmpCount++;
            }


            //GameObject stepCopy = Instantiate(step);
            //stepCopy.transform.position = obj.transform.position;


            //time -= Time.unscaledDeltaTime;
            
            

            // else
            //  {
            //   yield return "";
            //    slowerObj.isStepControllerRunning = false;
            // }
        }
    }

    public IEnumerator VisibleTagSpummer(SlowerObj slowerObj)
    {
        if (slowerObj.isSlowerRunning)
        {
            if (slowerObj.obj.CompareTag("Hider"))
            {
                slowerObj.obj.tag = "VisibleHider";
            }
            else if (slowerObj.obj.CompareTag("HiderPlayer"))
            {
                slowerObj.obj.tag = "VisibleHiderPlayer";
            }
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(VisibleTagSpummer(slowerObj));

        }
        else
        {
            if (slowerObj.obj.CompareTag("VisibleHider"))
            {
                slowerObj.obj.tag = "Hider";
            }
            else if (slowerObj.obj.CompareTag("VisibleHiderPlayer"))
            {
                slowerObj.obj.tag = "HiderPlayer";
            }
        }
    }


    // Start is called before the first frame update
    //void Start()
    //{


    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
