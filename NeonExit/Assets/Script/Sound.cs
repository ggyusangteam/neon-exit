using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startSound(string childName)
    {
        transform.FindChild(childName).GetComponent<AudioSource>().Play();
    }
}
