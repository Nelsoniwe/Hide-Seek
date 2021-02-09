using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimationController : MonoBehaviour
{
    private Animator animator;
    private Vector3 vector = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        animator = this.transform.GetComponentInChildren<Animator>();
        vector = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator == null)
            animator = this.transform.GetComponentInChildren<Animator>();

        if (vector.x != this.transform.position.x || vector.z != this.transform.position.z)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
        vector = this.transform.position;
    }
}
