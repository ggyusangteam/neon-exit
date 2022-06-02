using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : SubBassInteraction
{
    public Material material;
    public float lerpTime;
    public float r;
    public float g;
    public float b;
    public float intensity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Audio._freqBand[audioChannel]*threshold >= audioSensibility)
        {
            Change_color();
     
        }
        else
        {

            Color color = material.GetColor("_Color");
            material.SetColor("_Color", Color.Lerp(color, new Color(0, 0, 0), Time.deltaTime * lerpTime));



        }
    }
    public void Change_color()
    {


        material.SetColor("_Color", new Color(r, g, b) * intensity);
    }

    private void OnApplicationQuit()
    {

        material.SetColor("_Color", Color.black);
    }
}
