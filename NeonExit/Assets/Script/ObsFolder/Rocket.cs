using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Obstacle obstacle;
    [SerializeField] int ind;
    [SerializeField] GameObject player;
    [SerializeField] GameObject road;
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

        transform.position = new Vector3(transform.position.x, transform.position.z - player.transform.position.z, transform.position.z);
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.tag == "destroy")
        {
            obstacle.obsEnqueue(ind, gameObject);
        }
    }
}
