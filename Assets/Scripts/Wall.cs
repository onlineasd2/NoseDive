using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Wall : MonoBehaviour
{
    public bool moveUp = false;

    public float speed;

    public Transform[] placeForThings;
    
    [Space]
    public GameObject[] otherThings;

    public GameObject mainContoller;
    [Space]
    public bool itFirstWall = false;

    public void Start ()
    {
        mainContoller = GameObject.Find("ControllGame");
        if (!itFirstWall)
            CreateThings();
    }

    void Update ()
    {
        if (mainContoller.GetComponent<MainController>().newPlayerGlobal != null)
        {
            if (mainContoller.GetComponent<MainController>().newPlayerGlobal.GetComponent<PersonController>().isDead == true)
            {
                moveUp = false;
            }
        }

        MoveUp();
    }

    void MoveUp ()
    {
        if (moveUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (gameObject.transform.position.y >= 12)
            {
                Destroy(gameObject);
            }
        }
    }

    public int[] GetUniqueValues (int count, int s)
    {
        if (count >= s)
            throw new System.Exception("N needs to be less than S!");

        HashSet<int> values = new HashSet<int>();

        while (count != 0)
            if (values.Add(Random.Range(0, s)))
                count--;

        return values.ToArray();
    }

    void CreateThings ()
    {
        int countThings = Random.Range(0, placeForThings.Length);

        int[] ValueSet = GetUniqueValues(countThings, placeForThings.Length);

        for (int i = 0; i < countThings; i++)
        {
            int randomThing = Random.Range(0, otherThings.Length);
            
            GameObject thing = Instantiate(otherThings[randomThing], placeForThings[ValueSet[i]].transform.position, Quaternion.identity);
            thing.transform.SetParent(placeForThings[ValueSet[i]].transform);

            //Debug.Log(thing.name);
            
            if (ValueSet[i] > ((placeForThings.Length / 2) - 1))
            {
                thing.transform.Rotate(0f, -180f, 0f);
            } else if (ValueSet[i] < (placeForThings.Length / 2))
            {
                thing.transform.Rotate(0f, 0f, 0f);
            }

            //Debug.Log(ValueSet[i]);
        }
    }
}