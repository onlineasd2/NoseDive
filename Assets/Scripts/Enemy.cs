using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    public Transform target;
    [Space]

    public float speed;
    public float speedUp;
    public float distance;
    
    [HideInInspector]
    public bool directionR;

    RaycastHit2D hit;

    void Start ()
    {
        int startDirection = Random.Range(0, 2);

        if (startDirection == 0)
            directionR = true;
        else
            directionR = false;


        speed = Random.Range(3, 5);
        speedUp = Random.Range(2, 3);
    }

    void Update ()
    {
        EnemyMove();
        DestroyCheck(5.3f);
    }

    void DestroyCheck(float height)
    {
        if (gameObject.transform.position.y >= height)
        {
            Destroy(gameObject);
        }
    }

    void EnemyMove ()
    {

        Move(directionR);

        if (hit.collider == null)
        {

        } else 
        if (hit.collider.tag == "WallR")
        {
            directionR = false;
        }
        else
        if (hit.transform.tag == "WallL")
        {
            directionR = true;
            hit = Physics2D.Raycast(target.position, Vector2.right, distance);
        }
    }

    void Move (bool dirR)
    {
        if (dirR)
        {
            target.transform.localPosition = new Vector3(0.3f, 0, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            hit = Physics2D.Raycast(target.position, Vector2.right, distance);
        } else if (!dirR)
        {
            target.transform.localPosition = new Vector3(-0.3f, 0, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            hit = Physics2D.Raycast(target.position, Vector2.left, distance);
        }
        transform.Translate(Vector2.up * speedUp * Time.deltaTime);
    }
}

