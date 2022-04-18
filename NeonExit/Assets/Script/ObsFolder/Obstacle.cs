using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RhythmTool;


public class Obstacle : MonoBehaviour
{

    public RhythmData rhythmData;
    private List<Onset> places;
    private float prevTime;
    public AudioSource audioSource;
    [SerializeField] GameObject player;
    [SerializeField] float threshold;
    public GameObject[] obs = new GameObject[3];
    Dictionary<int, Queue<GameObject>> queue = new Dictionary<int, Queue<GameObject>>();
    private int poolCount = 100;
    public float arrivetime;


    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < 3; y++)
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
            Debug.Log("Obs" + ind + " appear at " + time + "sec");
            for (int x = 0; x < len; x++)
            {
                GameObject temp = obsDequeue(ind);
                temp.transform.position = new Vector3(place.line[x], 0.5f, player.transform.position.z + 50);
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
        _enqueueObs.SetActive(false);
        queue[ind].Enqueue(_enqueueObs);
    }
}