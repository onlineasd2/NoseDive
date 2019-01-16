using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour {

    public GameObject[] prefabs;
    [Space]

    public float delay;
    [Space]

    public bool spawn = false;
    [Space]

    public float minDelay, maxDelay;

    float t;

	void Start () {
        spawn = false;
    }
	
	void Update () {
        if (spawn)
        {
            Timer();
        }
    }

    void Timer ()
    {

        if (t == 0)
        {
            Create(-1.5f, 1.5f);
            delay = Random.Range(minDelay, maxDelay);
        }

        t += Time.deltaTime;

        if (t > delay)
        {
            t = 0;
        }

    }

    void Create (float minPos, float maxPos)
    {
        int whichPrefabs = Random.Range(0, prefabs.Length);

        float positionX = Random.Range(transform.position.x + minPos, transform.position.x + maxPos);

        //Debug.Log(positionX + "( " + minPos + "/" + maxPos + ")");

        Instantiate(prefabs[whichPrefabs], new Vector2(positionX, gameObject.transform.position.y), Quaternion.identity);
    }

}
