using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public GameObject backGround_Prefab;
    public int backGroundPosition = 2;
    public GameObject player;
    public GameObject backGroundParent;
    [SerializeField] Queue<GameObject> backGround_Archive = new Queue<GameObject>();

    [SerializeField] float max_BackGround = 7;


    private Queue<GameObject> backGround_Archive2 = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < max_BackGround; i++)
        {
            GameObject clone_Note = Instantiate(backGround_Prefab);

            clone_Note.transform.SetParent(backGroundParent.transform);

            InsertQueue(clone_Note);

        }

        GetQueue(0);
        GetQueue(1);


    }


    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.z >=( backGroundPosition-1) * 70)
        {
            
            GetQueue(backGroundPosition);
            
            backGroundPosition++;
            InsertQueue(backGround_Archive2.Dequeue());
        }

    }

    public void InsertQueue(GameObject p_object)
    {
        p_object.SetActive(false);
        backGround_Archive.Enqueue(p_object);
     

    }

     public GameObject GetQueue(int position)
    {
        GameObject t_clone = backGround_Archive.Dequeue();
        backGround_Archive2.Enqueue(t_clone);
        t_clone.transform.position = new Vector3(0, 0, (position) * 70);
        t_clone.SetActive(true);
        return t_clone;

    } 
}
