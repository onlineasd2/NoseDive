using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreater : MonoBehaviour {


    public bool createWall = false;
    // All walls
    public GameObject[] walls;

    // Delay between creat walls
    public float delay;

    float t = 0;
	
	void Update ()
    {
        if (createWall)
            Timer();
    }

    void Timer ()
    {

        if (t == 0)
        {
            CreateWall();
        }

        t += Time.deltaTime;

        if (t > delay)
        {
            t = 0;
        }

    }

    void CreateWall ()
    {
        Instantiate(walls[Random.Range(0, walls.Length)], transform.position, Quaternion.identity);
    }
}
