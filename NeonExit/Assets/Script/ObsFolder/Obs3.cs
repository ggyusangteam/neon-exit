using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obs3 : MonoBehaviour
{
    [SerializeField] Pool3 pool3;
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
            pool3.Enqueue(gameObject);
        }
    }

    public void Init(Pool3 _pool3)
    {
        pool3 = _pool3;
    }

    public void CleanUp()
    {
        activeTime = activeTimeRate;
    }
}
