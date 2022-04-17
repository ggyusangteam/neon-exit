using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float SlideSpeed;
    public float UpSpeed;
    public GameObject player;
    Vector3 cameraPos;
  public static bool canSlide = true;
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
    /*
    IEnumerator SlideCo(Transform transform_param)
    {
       float dir = -0.05f;
        float destinationPos = transform_param.position.y + dir;
       
        Vector3 currentPos = transform_param.position;

       
        canSlide = false;
        while (transform_param.position.y - destinationPos >= 0.0001f || destinationPos - transform_param.position.y >= 0.0001f)
        {

            transform_param.position = Vector3.MoveTowards(transform_param.position, new Vector3(transform.position.x, destinationPos, transform.position.z), SlideSpeed * Time.deltaTime);
            yield return null;
        }
        transform_param.position = new Vector3(transform.position.x, destinationPos, transform.position.z);

        while (transform_param.position.y - currentPos.y >= 0.0001f || currentPos.y - transform_param.position.y >= 0.0001f)
        {

            transform_param.position = Vector3.MoveTowards(transform_param.position, new Vector3(transform.position.x, currentPos.y, transform.position.z),UpSpeed * Time.deltaTime);
            yield return null;

        }
        transform_param.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
       
        canSlide = true;
    }
    */
}
