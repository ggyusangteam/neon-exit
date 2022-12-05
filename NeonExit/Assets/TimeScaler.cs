using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
	public AudioSource audioSource;
	public float TimeScale;
	// Start is called before the first frame update
	private void Awake()
	{

	}

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Time.timeScale = TimeScale;
		audioSource.pitch = TimeScale;
		audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1f / TimeScale);
	}
}
