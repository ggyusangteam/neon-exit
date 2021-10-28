using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public   GameObject player;
    public float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.position += new Vector3(0, 0, playerSpeed)*Time.deltaTime;
    }
}
