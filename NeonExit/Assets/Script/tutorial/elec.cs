using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elec : MonoBehaviour
{
    public tutorial tutorial;
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

        if (player.transform.position.z - transform.position.z >= 20)
        {
            tutorial.obsEnqueue(ind, gameObject);
        }

        //if (player.transform.position.z - transform.position.z <= 0.3 && player.transform.position.z - transform.position.z >= 0 && ind == 3 )
        //{
        //    if(Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.T))
        //    { 
        //        soundManager.GetComponent<Sound>().startSound("Pole");
        //    }
        //}
    }
}
