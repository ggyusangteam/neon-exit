using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBassInteraction : MonoBehaviour
{
    private static SubBassInteraction instance;

    public int audioChannel = 4;
    public float audioSensibility = 0.15f;
    public float threshold = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
