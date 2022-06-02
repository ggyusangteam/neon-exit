using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
   
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
	}
    void Start()
    {
        
    }

    void Save()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
   
	private void OnApplicationQuit()
	{
		
	}
}
