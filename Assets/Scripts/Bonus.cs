using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    public float speed;
    public int countBonus = 10;

    private void Start ()
    {
        speed = Random.Range(3, 5);
    }

    void Update () {
        BonusMove();
    }

    void BonusMove ()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        Destroy(gameObject, 5);
    }
}
