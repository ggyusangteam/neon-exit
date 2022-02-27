using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    Vector3 cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        cameraPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x + cameraPos.x, cameraPos.y, player.transform.position.z + cameraPos.z);
    }
}
