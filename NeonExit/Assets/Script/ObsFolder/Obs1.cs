using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obs1 : MonoBehaviour
{
    [SerializeField] Pool1 pool1;
    private float activeTime;
    private float activeTimeRate = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        activeTime -= Time.deltaTime;
        if (activeTime <= 0)
        {
            pool1.Enqueue(gameObject);
        }
    }

    public void Init(Pool1 _pool1)
    {
        pool1 = _pool1;
    }

    public void CleanUp()
    {
        activeTime = activeTimeRate;
    }
}
