using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestRoc : MonoBehaviour
{
    public Obstacle obstacle;
    [SerializeField] int ind;
    [SerializeField] GameObject player;
    //float activeTime = 5.0f;
    //float activeTimeRate = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        activeTime -= Time.deltaTime;
        if (activeTime <= 0)
        {
            activeTime = activeTimeRate;
            //obstacle.obsEnqueue(ind, gameObject);
        }
        */

        if (player.transform.position.z - transform.position.z >= 3)
        {
            obstacle.obsEnqueue(ind, gameObject);
        }
    }
}
