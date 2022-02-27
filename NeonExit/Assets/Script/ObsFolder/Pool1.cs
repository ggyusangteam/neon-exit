using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool1 : MonoBehaviour
{
    [SerializeField] GameObject obs1;
    private int poolCount = 10;
    Queue<GameObject> queue1 = new Queue<GameObject>();

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
        GameObject temp = Instantiate(obs1);
        temp.GetComponent<Obs1>().Init(this);
        temp.SetActive(false);
        //포지션 조정 필요
        temp.transform.SetParent(this.transform);
        temp.transform.localPosition = new Vector3(0, 0, 0);
        queue1.Enqueue(temp);
    }

    public GameObject Dequeue()
    {
        if (queue1.Count <= 0)
        {
            CreatObs();
        }

        GameObject dequeueObs = queue1.Dequeue();
        //확인 필요
        dequeueObs.GetComponent<Obs1>().CleanUp();
        dequeueObs.SetActive(true);
        return dequeueObs;
    }

    public void Enqueue(GameObject _enqueueObs)
    {
        _enqueueObs.SetActive(false);
        queue1.Enqueue(_enqueueObs);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
