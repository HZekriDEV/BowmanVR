using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemy = null;
    public GameObject spawn1 = null;
    public GameObject spawn2 = null;
    public GameObject spawn3 = null;
    public GameObject spawn4 = null;
    public GameObject spawn5 = null;
    public GameObject spawn6 = null;

    public static float rate = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnTimer");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnTimer()
    {
        int value = Random.Range(1, 6);

        switch(value)
        {
            case 1:
                Instantiate(enemy, spawn1.transform.position, spawn1.transform.rotation);
                break;
            case 2:
                Instantiate(enemy, spawn2.transform.position, spawn2.transform.rotation);
                break;
            case 3:
                Instantiate(enemy, spawn3.transform.position, spawn3.transform.rotation);
                break;
            case 4:
                Instantiate(enemy, spawn4.transform.position, spawn4.transform.rotation);
                break;
            case 5:
                Instantiate(enemy, spawn5.transform.position, spawn5.transform.rotation);
                break;
            case 6:
                Instantiate(enemy, spawn6.transform.position, spawn6.transform.rotation);
                break;
        }
        yield return new WaitForSecondsRealtime(rate);
        rate -= 0.1f;
        StartCoroutine("SpawnTimer");
    }
}
