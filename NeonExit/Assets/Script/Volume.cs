using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Volume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSli;
//    public AudioSource audio;

   
    // Start is called before the first frame update
    void Start()
    {
         
        volumeSli.value= DataManager.instance.Load("player_Setting").volume;


    }

    // Update is called once per frame
    void Update()
    {
    
    }
    public void SetVolume()
    {
 
        mixer.SetFloat("MasterVolume", Mathf.Log10(volumeSli.value) * 20);
        DataManager.instance.volume = volumeSli.value;
   
        SaveData data = new SaveData(DataManager.instance.volume);
        DataManager.instance.Save(data, "player_Setting");
   
    }   
}
