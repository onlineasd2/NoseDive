using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour {

    public Joystick joystick;

    [HideInInspector]
    public float horizontalMove = 0f;
    [HideInInspector]
    public float verticalMove = 0f;

    [Space]
    public float horizontalSpeed;
    [Space]

    public float speedUp;
    public float speedDown;

    public GameObject gameControll;

    public int score = 0;

    public bool isDead = false;

    public AudioClip hit;
    public AudioClip getBonus;

	void Start () {
		
	}
	
	void Update () {

        TouchControll();
        PlayerMove();
    }

    void PlayerMove ()
    {
        // Player moves on horizontal
        if (horizontalMove >= horizontalSpeed)
        {
            transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
        }
        else if (horizontalMove <= -horizontalSpeed)
        {
            transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
        }
        // Player moves on horizontal

        // Player moves on vertical
        if (verticalMove == speedUp)
        {
            transform.Translate(Vector3.up * speedUp * Time.deltaTime);
        }
        else if (verticalMove == speedDown)
        {
            transform.Translate(Vector3.down * speedDown * Time.deltaTime);
        }
        // Player moves on vertical
    }

    void TouchControll ()
    {
        // Horizontal
        if (joystick.Horizontal >= .2f)
        {
            horizontalMove = horizontalSpeed;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            horizontalMove = -horizontalSpeed;
        } else
        {
            horizontalMove = 0;
        }

        // Vertical
        if (joystick.Vertical >= .2f)
        {
            verticalMove = speedUp;
        }
        else if (joystick.Vertical <= -.2f)
        {
            verticalMove = speedDown;
        } else
        {
            verticalMove = 0f;
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isDead = true;
            SoundPlay(hit);
        }

        if (collision.gameObject.tag == "Bonus")
        {
            score += collision.gameObject.GetComponent<Bonus>().countBonus;
            SoundPlay(getBonus);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if ((collision.gameObject.tag == "WallR") || (collision.gameObject.tag == "WallL") || (collision.gameObject.tag == "Obstacles"))
        {
            isDead = true;
            SoundPlay(hit);
        }
    }

    public void GameObjectDestroy()
    {
        Destroy(gameObject);
    }

    public void SoundPlay(AudioClip audio)
    {
        GetComponent<AudioSource>().clip = audio;
        GetComponent<AudioSource>().Play();
    }
}
