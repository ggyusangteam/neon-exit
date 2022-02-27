using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RhythmTool
{
    public class Obstacle : MonoBehaviour
    {

        public RhythmData rhythmData;
        private List<Onset> onsets;
        private float prevTime;
        [SerializeField] float delayTime;
        public AudioSource audioSource;
        [SerializeField] GameObject player;
        [SerializeField] float threshold;
        [SerializeField] Pool1 pool1;
        [SerializeField] Pool2 pool2;
        [SerializeField] Pool3 pool3;
        
        // Start is called before the first frame update
        void Start()
        {

        }
        private void Awake()
        {
            onsets = new List<Onset>();
        }
        private void FixedUpdate()
        {
            float time = audioSource.time + delayTime;
            onsets.Clear();

            rhythmData.GetFeatures<Onset>(onsets, prevTime, time);
            foreach (Onset onset in onsets)
            {
                if (onset.intensity > threshold)
                {
                    Debug.Log("Obs1 appear at " + time + "sec");
                    GameObject obs1 = pool1.Dequeue();
                    obs1.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 50);
                }

                if (onset.intensity > 1)
                {
                    Debug.Log("Obs2 appear at " + time + "sec");
                    GameObject obs2 = pool2.Dequeue();
                    obs2.transform.position = new Vector3(player.transform.position.x-1, player.transform.position.y, player.transform.position.z + 50);
                }

                if (onset.intensity > 2)
                {
                    Debug.Log("Obs3 appear at " + time + "sec");
                    GameObject obs3 = pool3.Dequeue();
                    obs3.transform.position = new Vector3(player.transform.position.x+1, player.transform.position.y, player.transform.position.z + 50);
                }
            }
            prevTime = time;

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}