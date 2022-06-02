using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;
    public float audioSo_Length;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        audioSo_Length = audioSource.clip.length;
        slider.value = audioSource.time / audioSo_Length;
    }
}
