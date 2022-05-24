using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RhythmTool;


public class Obstacle : MonoBehaviour
{
    public const int obsCnt = 12;
	
    public RhythmData rhythmData;
    private List<Onset> places;
    private float prevTime;
    public AudioSource audioSource;
    [SerializeField] GameObject player;
    [SerializeField] float threshold;
    public GameObject[] obs = new GameObject[obsCnt];
    Dictionary<int, Queue<GameObject>> queue = new Dictionary<int, Queue<GameObject>>();
    private int poolCount = 100;
    public float arrivetime;
    GameObject temp;


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
            //�����ʿ�
            setObs(y);
        }
    }

    private void Awake()
    {
        places = new List<Onset>();
    }

    private void FixedUpdate()
    {
        float time = audioSource.time;
        places.Clear();

        rhythmData.GetFeatures<Onset>(places, prevTime + arrivetime, time + arrivetime, "Place");
        foreach (Onset place in places)
        {
            int ind = place.ind;
            int len = place.line.Length;
            //Debug.Log("Obs" + ind + " appear at " + time + "sec");

            if(ind == 0 || ind == 5 || ind == 9)
            {
                for (int x = 0; x < len; x++)
                {
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(place.line[x], 0f, player.transform.position.z + 50);
                }

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 50 + 0.5f);
                temp.tag = "score";
            }

            else if(ind == 6)
            {
                
                for (int x = 0; x < len; x++)
                {
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(place.line[x] + 0.5f, 0f, player.transform.position.z + 50);
                }

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 50 + 0.5f);
                temp.tag = "score";
            }

            else if(ind == 2)
            {
                temp = obsDequeue(ind);
                temp.transform.position = new Vector3(0, 1.2f, player.transform.position.z + 50);

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 50 + 0.5f);
                temp.tag = "score";
            }

            else if(ind == 10)
            {
                temp = obsDequeue(ind);
                temp.transform.position = new Vector3(0, 0, player.transform.position.z + 50);

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 50);
                temp.transform.localScale = new Vector3(1, 1, 170);
                temp.tag = "hammer";

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 50 + 0.5f);
                temp.tag = "score";
            }

            else if (ind == 3)
            {
                
                int elecSize = place.elecSize;

                temp = obsDequeue(4);
                temp.transform.position = new Vector3(place.line[0], 0.5f, player.transform.position.z + 50);
                temp = temp.transform.GetChild(3).gameObject;
                temp.transform.position = new Vector3(place.line[0], 0.5f, player.transform.position.z + 50 + elecSize-1);
                //������ �ǵ帮�� �ڵ� �ʿ�

                for (int x = 1; x < elecSize-1; x++)
                {
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(0, 0.5f, player.transform.position.z + 50 + x);
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(0, 0.5f, player.transform.position.z + 50 + x + 0.5f);
                }

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(place.line[0]-1, 2, player.transform.position.z + 50 + ((elecSize-1) / 2));
                temp.transform.localScale = new Vector3(1/7f, 1, (elecSize+14)*10);
                temp.tag = "electro";
				temp = obsDequeue(11);
				temp.transform.position = new Vector3(place.line[0] + 1, 2, player.transform.position.z + 50 + ((elecSize - 1) / 2));
				temp.transform.localScale = new Vector3(1 / 7f, 1, (elecSize + 14) * 10);
				temp.tag = "electro";

				temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 50.5f + elecSize - 1);
                temp.tag = "score";
            }

            else if(ind == 7)
            {
                for (int x = 0; x < len; x++)
                {
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(place.line[x], 50f, player.transform.position.z + 50);
                    temp = obsDequeue(8);
                    temp.transform.position = new Vector3(place.line[x], 0.1f, player.transform.position.z + 50);
                }

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 50 + 0.5f);
                temp.tag = "score";
            }

            else
            {
                for (int x = 0; x < len; x++)
                {
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(place.line[x], 0.5f, player.transform.position.z + 50);

                    temp = obsDequeue(11);
                    temp.transform.position = new Vector3(place.line[x], 0.5f, player.transform.position.z + 50);
                    temp.transform.localScale = new Vector3(0.1f, 0.1f, 1);
                    temp.tag = "score";
                }

        
            }
        }
        prevTime = time;

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

    //�����ʿ�
    void setObs(int ind)
    {
        for (int x = 0; x < poolCount; x++)
        {
            GameObject temp = queue[ind].Dequeue();
            //������ ���� �ʿ�
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
        _enqueueObs.SetActive(false);
        queue[ind].Enqueue(_enqueueObs);
    }
}