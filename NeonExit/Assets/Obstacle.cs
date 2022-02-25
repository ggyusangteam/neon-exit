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
        public AudioSource audioSource;
        [SerializeField] GameObject obstacle;
        [SerializeField] GameObject player;
        [SerializeField] float threshold;
        Vector3 sdd;
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
            sdd = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 5);
            float time = audioSource.time;
            onsets.Clear();

            rhythmData.GetFeatures<Onset>(onsets, prevTime, time);
            foreach (Onset onset in onsets)
            {
                if (onset.intensity > threshold)
                {
                    Debug.Log(".");
                    Debug.Log(onset.strength);
                    StartCoroutine(sss());
                    break;
                }

            }
            prevTime = time;

        }
        public IEnumerator sss()
        { obstacle.transform.position = sdd;
            yield return null;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}