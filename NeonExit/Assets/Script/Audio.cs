using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public float time;
    public AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource.PlayDelayed(time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
