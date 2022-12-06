using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public const int obsCnt = 14;
    public GameObject[] obs = new GameObject[obsCnt];
    Dictionary<int, Queue<GameObject>> queue = new Dictionary<int, Queue<GameObject>>();
    private int poolCount = 100;
    GameObject temp;

    int typeNum = 0;
    int cnt = 0;

    void OnTriggerEnter(Collider _col)
    {

        if (_col.tag == "check")
        {
            if (GetComponent<TPlayer>().testMode == false)
            {
                cnt++;
            }
            if (cnt == 4)
            {
                typeNum++;
                cnt = 1;
            }

            if (typeNum == 0)
            {
                if (cnt == 1)
                {
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(-2, 0f, transform.position.z + 100);
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(-1, 0f, transform.position.z + 100);
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(1, 0f, transform.position.z + 100);
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(2, 0f, transform.position.z + 100);
                    temp = obsDequeue(1);
                    temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 2)
                {
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(0, 0f, transform.position.z + 100);
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(-1, 0f, transform.position.z + 100);
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(1, 0f, transform.position.z + 100);
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(2, 0f, transform.position.z + 100);
                    temp = obsDequeue(1);
                    temp.transform.position = new Vector3(-2, 0.5f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 3)
                {
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(0, 0f, transform.position.z + 100);
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(-1, 0f, transform.position.z + 100);
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(1, 0f, transform.position.z + 100);
                    temp = obsDequeue(0);
                    temp.transform.position = new Vector3(-2, 0f, transform.position.z + 100);
                    temp = obsDequeue(1);
                    temp.transform.position = new Vector3(2, 0.5f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
            }
            else if (typeNum == 1)
            {
                if (cnt == 1)
                {
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(-2, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(-2, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(-1, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(-1, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(1, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(1, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(2, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(2, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 2)
                {
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(0, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(0, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(-1, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(-1, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(1, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(1, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(2, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(2, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 3)
                {
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(-2, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(-2, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(-1, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(-1, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(1, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(1, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(2);
                    temp.transform.position = new Vector3(0, 50f, transform.position.z + 100);
                    temp = obsDequeue(3);
                    temp.transform.position = new Vector3(0, 0.1f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
            }
            else if(typeNum == 2)
            {
                if (cnt == 1)
                {
                    temp = obsDequeue(4);
                    temp.transform.position = new Vector3(1, 0f, transform.position.z + 100);
                    temp = obsDequeue(5);
                    temp.transform.position = new Vector3(-2 + 0.5f, 0f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 2)
                {
                    temp = obsDequeue(4);
                    temp.transform.position = new Vector3(-1, 0f, transform.position.z + 100);
                    temp = obsDequeue(5);
                    temp.transform.position = new Vector3(1 + 0.5f, 0f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 3)
                {
                    temp = obsDequeue(4);
                    temp.transform.position = new Vector3(1, 0f, transform.position.z + 100);
                    temp = obsDequeue(5);
                    temp.transform.position = new Vector3(-2 + 0.5f, 0f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
            }
            else if (typeNum == 3)
            {
                if (cnt == 1)
                {
                    temp = obsDequeue(6);
                    temp.transform.position = new Vector3(0, 1.2f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 2)
                {
                    temp = obsDequeue(6);
                    temp.transform.position = new Vector3(0, 1.2f, transform.position.z + 100);
                    temp = obsDequeue(4);
                    temp.transform.position = new Vector3(-1, 0f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 3)
                {
                    temp = obsDequeue(6);
                    temp.transform.position = new Vector3(0, 1.2f, transform.position.z + 100);
                    temp = obsDequeue(5);
                    temp.transform.position = new Vector3(1 + 0.5f, 0f, transform.position.z + 100);
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 105);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
            }
            else if (typeNum == 4)
            {
                temp = obsDequeue(7);
                temp.transform.position = new Vector3(0.81577f, 0, transform.position.z + 100);
                temp = obsDequeue(13);
                temp.transform.position = new Vector3(0, 2, transform.position.z + 96);
                temp.transform.localScale = new Vector3(1, 1, 85);
                temp.tag = "hammer";
                temp = obsDequeue(13);
                temp.transform.position = new Vector3(0, 2, transform.position.z + 99.8f);
                temp.transform.localScale = new Vector3(1, 1, 1);
                temp.tag = "obstacle";
                temp = obsDequeue(13);
                temp.transform.position = new Vector3(0, 2, transform.position.z + 110);
                temp.transform.localScale = new Vector3(1, 1, 2);
                temp.tag = "check";
            }
            else if (typeNum == 5)
            {
                if (cnt == 1)
                {
                    int elecSize = 10;
                    int ind = -1;
                    temp = obsDequeue(8);
                    temp.transform.position = new Vector3(ind, 0.5f, transform.position.z + 100);
                    temp = temp.transform.GetChild(10).gameObject;
                    temp.transform.position = new Vector3(ind, 0.5f, transform.position.z + 100 + elecSize - 1);
                    for (int x = 1; x < elecSize - 1; x++)
                    {
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x);
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x + 0.5f);
                    }
                    float half = elecSize / 2;
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(ind + 1, 2, transform.position.z + 97 + half);
                    temp.transform.localScale = new Vector3(1 / 7f, 1, (elecSize * 10) + 20);
                    temp.tag = "electro";
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 100 + elecSize);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 2)
                {
                    int elecSize = 5;
                    int ind = 1;
                    temp = obsDequeue(8);
                    temp.transform.position = new Vector3(ind, 0.5f, transform.position.z + 100);
                    temp = temp.transform.GetChild(10).gameObject;
                    temp.transform.position = new Vector3(ind, 0.5f, transform.position.z + 100 + elecSize - 1);
                    for (int x = 1; x < elecSize - 1; x++)
                    {
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x);
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x + 0.5f);
                    }
                    float half = elecSize / 2;
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(ind + 1, 2, transform.position.z + 97 + half);
                    temp.transform.localScale = new Vector3(1 / 7f, 1, (elecSize * 10) + 20);
                    temp.tag = "electro";
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 100 + elecSize);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 3)
                {
                    int elecSize = 20;
                    int ind = -3;
                    temp = obsDequeue(8);
                    temp.transform.position = new Vector3(ind, 0.5f, transform.position.z + 100);
                    temp = temp.transform.GetChild(10).gameObject;
                    temp.transform.position = new Vector3(ind, 0.5f, transform.position.z + 100 + elecSize - 1);
                    for (int x = 1; x < elecSize - 1; x++)
                    {
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x);
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x + 0.5f);
                    }
                    float half = elecSize / 2;
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(ind + 1, 2, transform.position.z + 97 + half);
                    temp.transform.localScale = new Vector3(1 / 7f, 1, (elecSize * 10) + 20);
                    temp.tag = "electro";
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 100 + elecSize);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
            }
            else if (typeNum == 6)
            {
                if (cnt == 1)
                {
                    int elecSize = 10;
                    float xloc = 3.3f;
                    temp = obsDequeue(10);
                    temp.transform.position = new Vector3(xloc, 0, transform.position.z + 97.5f);
                    for (int x = 0; x < elecSize / 3 + 1; x++)
                    {
                        temp = obsDequeue(11);
                        temp.transform.position = new Vector3(xloc, 0.75f, transform.position.z + 100 + (x * 3));
                    }
                    for (int x = 0; x < elecSize; x++)
                    {
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x);
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x + 0.5f);
                    }
                    float half = elecSize / 2;
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(2, 2, transform.position.z + 96 + half);
                    temp.transform.localScale = new Vector3(1 / 7f, 1, (elecSize * 10) + 40);
                    temp.tag = "rtruck";

                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 100 + elecSize);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 2)
                {
                    int elecSize = 5;
                    float xloc = -3.3f;
                    temp = obsDequeue(10);
                    temp.transform.position = new Vector3(xloc, 0, transform.position.z + 97.5f);
                    for (int x = 0; x < elecSize / 3 + 1; x++)
                    {
                        temp = obsDequeue(11);
                        temp.transform.position = new Vector3(xloc, 0.75f, transform.position.z + 100 + (x * 3));
                    }
                    for (int x = 0; x < elecSize; x++)
                    {
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x);
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x + 0.5f);
                    }
                    float half = elecSize / 2;
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(-2, 2, transform.position.z + 96 + half);
                    temp.transform.localScale = new Vector3(1 / 7f, 1, (elecSize * 10) + 40);
                    temp.tag = "ltruck";

                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 100 + elecSize);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
                else if (cnt == 3)
                {
                    int elecSize = 20;
                    float xloc = 3.3f;
                    temp = obsDequeue(10);
                    temp.transform.position = new Vector3(xloc, 0, transform.position.z + 97.5f);
                    for (int x = 0; x < elecSize / 3 + 1; x++)
                    {
                        temp = obsDequeue(11);
                        temp.transform.position = new Vector3(xloc, 0.75f, transform.position.z + 100 + (x * 3));
                    }
                    for (int x = 0; x < elecSize; x++)
                    {
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x);
                        temp = obsDequeue(9);
                        temp.transform.position = new Vector3(0, 0.5f, transform.position.z + 100 + x + 0.5f);
                    }
                    float half = elecSize / 2;
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(2, 2, transform.position.z + 96 + half);
                    temp.transform.localScale = new Vector3(1 / 7f, 1, (elecSize * 10) + 40);
                    temp.tag = "rtruck";

                    xloc = -3.3f;
                    temp = obsDequeue(10);
                    temp.transform.position = new Vector3(xloc, 0, transform.position.z + 97.5f);
                    for (int x = 0; x < elecSize / 3 + 1; x++)
                    {
                        temp = obsDequeue(11);
                        temp.transform.position = new Vector3(xloc, 0.75f, transform.position.z + 100 + (x * 3));
                    }
                    half = elecSize / 2;
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(-2, 2, transform.position.z + 96 + half);
                    temp.transform.localScale = new Vector3(1 / 7f, 1, (elecSize * 10) + 40);
                    temp.tag = "ltruck";

                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(0, 2, transform.position.z + 100 + elecSize);
                    temp.transform.localScale = new Vector3(1, 1, 2);
                    temp.tag = "check";
                }
            }

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < obsCnt; y++)
        {
            Queue<GameObject> newqueue = new Queue<GameObject>();
            queue.Add(y, newqueue);

            for (int x = 0; x < poolCount; x++)
            {
                CreatObs(y);
            }
            //수정필요
            setObs(y);
        }

        temp = obsDequeue(13);
        temp.transform.position = new Vector3(0, 2, transform.position.z + 20);
        temp.transform.localScale = new Vector3(1, 1, 2);
        temp.tag = "check";
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreatObs(int ind)
    {
        GameObject temp = Instantiate(obs[ind]);
        temp.SetActive(false);
        queue[ind].Enqueue(temp);
    }

    //수정필요
    void setObs(int ind)
    {
        for (int x = 0; x < poolCount; x++)
        {
            GameObject temp = queue[ind].Dequeue();
            //포지션 조정 필요
            temp.transform.SetParent(obs[ind].transform);
            temp.transform.localPosition = new Vector3(0, 0, 0);

            queue[ind].Enqueue(temp);
        }
    }

    public GameObject obsDequeue(int ind)
    {
        if (queue[ind].Count <= 0)
        {
            CreatObs(ind);
        }

        GameObject dequeueObs = queue[ind].Dequeue();
        dequeueObs.SetActive(true);
        return dequeueObs;
    }

    public void obsEnqueue(int ind, GameObject _enqueueObs)
    {
        //if(ind == 7) { soundManager.GetComponent<Sound>().startSound("Rocket"); }
        _enqueueObs.SetActive(false);
        queue[ind].Enqueue(_enqueueObs);
    }
}
