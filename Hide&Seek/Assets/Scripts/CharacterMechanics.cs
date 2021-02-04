using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanics : MonoBehaviour
{
    public float speedMove;
    public float jumpPower;

    private Vector3 moveVector;

    private CharacterController ch_controller;
    private MobileController mContr;

    private GameObject Timer;

    private float oldSpeed;
    private void Start()
    {
        oldSpeed = speedMove;
        mContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();
        ch_controller = GetComponent<CharacterController>();
        Timer = GameObject.FindGameObjectsWithTag("Timer")[0];
        StartCoroutine(StopSeekerforSeconds());
    }

    public IEnumerator StopSeekerforSeconds()
    {
        if (Timer.GetComponent<UnityEngine.UI.Text>().text == "")
        {
            speedMove = oldSpeed;
            gameObject.SetActive(true);
            GameObject.FindGameObjectsWithTag("HunterPlayer")[0].GetComponent<FOV>().viewRadius = 10;

            yield return "";
        }
        else
        {
            speedMove = 0;
            yield return new WaitForSeconds(0.5f);

            yield return StartCoroutine(StopSeekerforSeconds());
        }
    }

    public IEnumerator WaitForChoose()
    {
        if (GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>() != null)
        {
           // ch_controller = GetComponent<CharacterController>();
            //mContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();
          //  GameObject timer = GameObject.FindGameObjectsWithTag("Timer")[0];
           // GameObject gameObject2 = GameObject.FindGameObjectsWithTag("JoystickPanel")[0];
            //StartCoroutine(StopforSeconds(timer, gameObject2));

            yield return("");
        }
        else
        {
            yield return (StartCoroutine("WaitForChoose"));
        }
    }

    private void Update()
    {
        if (StaticField.gameWinned)
            speedMove = 0;
        CharacterMove();
    }

    private void CharacterMove()
    {
        moveVector = Vector3.zero;
        moveVector.x = mContr.Horizontal() * speedMove;
        moveVector.z = mContr.Vertical() * speedMove;

        
        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        if (!ch_controller.isGrounded)
        {
            //transform.position.y -= 100 * Time.deltaTime;
            moveVector.y -= 400 * Time.deltaTime;
           // transform.position = new Vector3(transform.position.x, transform.position.y - 1 * Time.deltaTime, transform.position.z);
        }

        ch_controller.Move(moveVector * Time.deltaTime);
    }
}
