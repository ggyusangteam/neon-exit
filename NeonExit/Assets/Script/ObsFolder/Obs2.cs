using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obs2 : MonoBehaviour
{
    [SerializeField] Pool2 pool2;
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
            pool2.Enqueue(gameObject);
        }
    }

    public void Init(Pool2 _pool2)
    {
        pool2 = _pool2;
    }

    public void CleanUp()
    {
        activeTime = activeTimeRate;
    }
}
