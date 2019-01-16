using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSplinter : MonoBehaviour {

    public GameObject glass;
    public GameObject window;
    public GameObject leftSideWall; // for set parent

    public GameObject brokenWindow;

    public int countSplinter;

	void Start () {
        GameObject broken = Instantiate(brokenWindow, window.transform.position, Quaternion.identity);

        broken.transform.SetParent(leftSideWall.transform);

        Destroy(window);

        for (int i = 0; i < countSplinter; i++)
        {
            float Y = Random.Range(window.transform.position.y + 0.5f, window.transform.position.y - 0.5f);
            float force = Random.Range(1, 5);

            GameObject copySplinter = Instantiate(glass, new Vector2(window.transform.position.x, Y), Quaternion.identity);

            copySplinter.GetComponent<Rigidbody2D>().AddForce(Vector2.right * force, ForceMode2D.Impulse);

            Destroy(copySplinter, 5f);
        }        
    }
}
