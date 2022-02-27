using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool3 : MonoBehaviour
{
    [SerializeField] GameObject obs3;
    private int poolCount = 10;
    Queue<GameObject> queue3 = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < poolCount; x++)
        {
            CreatObs();
        }
    }

    void CreatObs()
    {
        GameObject temp = Instantiate(obs3);
        temp.GetComponent<Obs3>().Init(this);
        temp.SetActive(false);
        //포지션 조정 필요
        temp.transform.SetParent(this.transform);
        temp.transform.localPosition = new Vector3(0, 0, 0);
        queue3.Enqueue(temp);
    }

    public GameObject Dequeue()
    {
        if (queue3.Count <= 0)
        {
            CreatObs();
        }

        GameObject dequeueObs = queue3.Dequeue();
        dequeueObs.GetComponent<Obs3>().CleanUp();
        dequeueObs.SetActive(true);
        return dequeueObs;
    }

    public void Enqueue(GameObject _enqueueObs)
    {
        _enqueueObs.SetActive(false);
        queue3.Enqueue(_enqueueObs);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
