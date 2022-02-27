using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool2 : MonoBehaviour
{
    [SerializeField] GameObject obs2;
    private int poolCount = 10;
    Queue<GameObject> queue2 = new Queue<GameObject>();

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
        GameObject temp = Instantiate(obs2);
        temp.GetComponent<Obs2>().Init(this);
        temp.SetActive(false);
        //포지션 조정 필요
        temp.transform.SetParent(this.transform);
        temp.transform.localPosition = new Vector3(0, 0, 0);
        queue2.Enqueue(temp);
    }

    public GameObject Dequeue()
    {
        if (queue2.Count <= 0)
        {
            CreatObs();
        }

        GameObject dequeueObs = queue2.Dequeue();
        dequeueObs.GetComponent<Obs2>().CleanUp();
        dequeueObs.SetActive(true);
        return dequeueObs;
    }

    public void Enqueue(GameObject _enqueueObs)
    {
        _enqueueObs.SetActive(false);
        queue2.Enqueue(_enqueueObs);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
