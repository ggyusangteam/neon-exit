using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col : MonoBehaviour
{
    public tutorial tutorial;
    [SerializeField] int ind;
    [SerializeField] GameObject player;
    public int score = 100;
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

        if (player.transform.position.z - transform.position.z >= transform.localScale.z / 20 + 3)
        {
            transform.localScale = new Vector3(1, 1, 1);
            tutorial.obsEnqueue(ind, gameObject);
        }
    }
}
