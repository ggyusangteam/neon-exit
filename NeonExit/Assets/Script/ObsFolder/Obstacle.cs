using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RhythmTool;


public class Obstacle : MonoBehaviour
{
    public const int obsCnt = 14;
	
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
    [SerializeField] GameObject soundManager;


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

//#if UNITY_ANDROID
//        arrivetime = arrivetime;
//#endif
//#if UNITY_EDITOR
//        arrivetime = arrivetime;
//#endif
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

            if (ind == 0 || ind == 5)
            {
                for (int x = 0; x < len; x++)
                {
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(place.line[x], 0f, player.transform.position.z + 100);
                }

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 100 + 0.5f);
                temp.tag = "score";
            }

            else if (ind == 9)
            {
                for (int x = 0; x < len; x++)
                {
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(place.line[x], 0f, player.transform.position.z + 100);
                }
            }

            else if (ind == 6)
            {

                for (int x = 0; x < len; x++)
                {
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(place.line[x] + 0.5f, 0f, player.transform.position.z + 100);
                }

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 100 + 0.5f);
                temp.tag = "score";
            }

            else if (ind == 2)
            {
                temp = obsDequeue(ind);
                temp.transform.position = new Vector3(0, 1.2f, player.transform.position.z + 100);

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 100 + 0.5f);
                temp.tag = "score";
            }

            else if (ind == 10)
            {
                temp = obsDequeue(ind);
                temp.transform.position = new Vector3(0.81577f, 0, player.transform.position.z + 100);

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 96);
                temp.transform.localScale = new Vector3(1, 1, 85);
                temp.tag = "hammer";

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 99.8f);
                temp.transform.localScale = new Vector3(1, 1, 1);
                temp.tag = "obstacle";

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 100 + 0.5f);
                temp.tag = "score";
            }

            else if (ind == 3)
            {

                int elecSize = place.elecSize;

                temp = obsDequeue(4);
                temp.transform.position = new Vector3(place.line[0], 0.5f, player.transform.position.z + 100);
                temp = temp.transform.GetChild(10).gameObject;
                temp.transform.position = new Vector3(place.line[0], 0.5f, player.transform.position.z + 100 + elecSize - 1);

                //에디터 건드리는 코드 필요
                //temp = temp.transform.GetChild(6).gameObject;
                //temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, temp.transform.position.z);

                for (int x = 1; x < elecSize - 1; x++)
                {
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(0, 0.5f, player.transform.position.z + 100 + x);
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(0, 0.5f, player.transform.position.z + 100 + x + 0.5f);

                    temp = obsDequeue(11);
                    temp.transform.position = new Vector3(0, 2, player.transform.position.z + 100 + x);
                    temp.tag = "score";
                    temp.GetComponent<Col>().score = 10;
                }

                float half = elecSize / 2;
                temp = obsDequeue(11);
                temp.transform.position = new Vector3(place.line[0] + 1, 2, player.transform.position.z + 97 + half);
                temp.transform.localScale = new Vector3(1 / 7f, 1, (elecSize * 10)+20);
                temp.tag = "electro";

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 100 + elecSize);
                temp.tag = "score";
                temp.GetComponent<Col>().score = 200;
            }

            else if (ind == 7)
            {
                for (int x = 0; x < len; x++)
                {
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(place.line[x], 50f, player.transform.position.z + 100);
                    temp = obsDequeue(8);
                    temp.transform.position = new Vector3(place.line[x], 0.1f, player.transform.position.z + 100);
                }

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 100 + 0.5f);
                temp.tag = "score";
            }

            else if (ind == 12)
            {
                int elecSize = place.elecSize;
                float xloc = place.line[0];
                if (xloc == 4) xloc = 3.3f;
                else xloc -= 0.3f;

                temp = obsDequeue(ind);
                temp.transform.position = new Vector3(xloc, 0, player.transform.position.z + 97.5f);

                for (int x = 0; x < elecSize / 3 + 1; x++)
                {
                    temp = obsDequeue(13);
                    temp.transform.position = new Vector3(xloc, 0.75f, player.transform.position.z + 100 + (x * 3));
                }
				for (int x = 1; x < elecSize; x++)
				{
					temp = obsDequeue(3);
					temp.transform.position = new Vector3(0, 0.5f, player.transform.position.z + 100 + x);
					temp = obsDequeue(3);
					temp.transform.position = new Vector3(0, 0.5f, player.transform.position.z + 100 + x + 0.5f);

					temp = obsDequeue(11);
					temp.transform.position = new Vector3(0, 2, player.transform.position.z + 100 + x);
					temp.tag = "score";
				}
				float half = elecSize / 2;
                if (xloc == 3.3f)
                {
                    temp = obsDequeue(11);
                    temp.transform.position = new Vector3(2, 2, player.transform.position.z + 96 + half);
                    temp.transform.localScale = new Vector3(1 / 7f, 1, (elecSize * 10) + 40);
                    temp.tag = "rtruck";
                }
                else
                {
                    temp = obsDequeue(11);
                    temp.transform.position = new Vector3(-2, 2, player.transform.position.z + 96 + half);
                    temp.transform.localScale = new Vector3(1 / 7f, 1, (elecSize * 10) + 40);
                    temp.tag = "ltruck";
                }

                temp = obsDequeue(11);
                temp.transform.position = new Vector3(0, 2, player.transform.position.z + 100 + elecSize);
                temp.tag = "score";
                temp.GetComponent<Col>().score = 200;
            }

            else
            {
                for (int x = 0; x < len; x++)
                {
                    temp = obsDequeue(ind);
                    temp.transform.position = new Vector3(place.line[x], 0.5f, player.transform.position.z + 100);


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