using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseHSGamePlay : MonoBehaviour
{
    public GameObject PlayerHider;
    public GameObject PlayerSeeker;
    public GameObject Camera;
    public GameObject Joystick;
    public string Choose;
    public GameObject Panel;

    private GameObject Timer;
    private NavMeshAgent agent;

    private MobileController mobileController;

    public GameObject ShopButton;


    public void ChooseGamePlay()
    {
        if (StaticField.ChoosedPlay == ChoosePlay.none)
        {
            if (Choose == "Hide")
            {
                StaticField.ChoosedPlay = ChoosePlay.hide;
                Timer.GetComponent<Text>().enabled = true;
                Timer.GetComponent<Text>().text = "";
                // GameObject.FindGameObjectsWithTag("Timer")[0].SetActive(false);
                // GameObject.FindGameObjectsWithTag("Timer")[0].GetComponent<UnityEngine.UI.Text>().text = "0";

                PlayerHider.GetComponent<CharacterController>().enabled = true;
                PlayerHider.GetComponent<CharacterMechanics>().enabled = true;
                PlayerHider.tag = "HiderPlayer";
                Destroy(agent);
                PlayerHider.GetComponent<TargetRuning>().enabled = false;

                //print(agent.enabled);

                PlayerSeeker.GetComponent<CharacterController>().enabled = false;

                PlayerSeeker.GetComponent<CharacterMechanics>().enabled = false;
                PlayerSeeker.GetComponent<SeekerRunning>().enabled = true;

                //PlayerSeeker.GetComponent<FOV>().enabled = false;
                //GameObject.Find("PanelJoystick").active = true;
                //Joystick.SetActive(true);
                //Joystick.active = true;

                Camera.GetComponent<CameraFollow>().target = PlayerHider.gameObject.transform;
                StaticField.gameStarted = true;
            }
            else
            {
                StaticField.ChoosedPlay = ChoosePlay.seek;
                Timer.GetComponent<StartTimerScript>().enabled = true;
                Timer.GetComponent<Text>().enabled = true;
                //GameObject.FindGameObjectsWithTag("Timer")[0].GetComponent<UnityEngine.UI.Text>().text = "5";

                PlayerHider.GetComponent<CharacterController>().enabled = false;
                PlayerHider.GetComponent<CharacterMechanics>().enabled = false;
                //agent.enabled = true;
                PlayerHider.GetComponent<TargetRuning>().enabled = true;

                PlayerSeeker.GetComponent<CharacterController>().enabled = true;
                PlayerSeeker.GetComponent<CharacterMechanics>().enabled = true;
                PlayerSeeker.GetComponent<NavMeshAgent>().enabled = false;

                PlayerSeeker.GetComponent<FOV>().enabled = true;
                //GameObject.Find("PanelJoystick").active = true;
                // Joystick.SetActive(true);
                //Joystick.active = true;
                Camera.GetComponent<CameraFollow>().target = PlayerSeeker.gameObject.transform;
                StaticField.hideSkins = true;


               

                //StartCoroutine("StopSeekerforSeconds");
            }
            ShopButton.SetActive(false);
            GameObject[] Hiders = GameObject.FindGameObjectsWithTag("Hider");

            for (int i = 0; i < Hiders.Length; i++)
            {
                //MeshRenderer mesh = Hiders[i].GetComponent<MeshRenderer>();
                TargetRuning target = Hiders[i].GetComponent<TargetRuning>();

                //  if (mesh != null)
                //  mesh.enabled = false;
                if (target != null && PlayerSeeker.GetComponent<CharacterController>() != null)
                    target.enabled = true;

                //  Hiders[i].GetComponent<MeshRenderer>().enabled = false;
                //  Hiders[i].GetComponent<TargetRuning>().enabled = true;
            }

            Panel.SetActive(false);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("Level "+PlayerPrefs.GetInt("CurrentLevel"));
    }
    public void NextLevel()
    {
        //PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel")+1);
        SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("CurrentLevel"));
    }
    


    // Start is called before the first frame update
    void Start()
    {
       
       // Joystick.SetActive(false);
        agent = PlayerHider.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Timer = GameObject.FindGameObjectsWithTag("Timer")[0];
        
        // Timer.GetComponent<StartTimerScript>().enabled = false;
        // Timer.GetComponent<Text>().enabled = true;
    }

}
