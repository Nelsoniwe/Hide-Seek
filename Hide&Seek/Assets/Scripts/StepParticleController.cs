using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepParticleController : MonoBehaviour
{
    public ParticleSystem particle;
    private float time = 0;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (time<=0)
        {
            particle.Play();

            time = Random.Range(4,10);

        }
        time -= Time.deltaTime;
    }
}
