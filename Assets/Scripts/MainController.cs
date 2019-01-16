using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

    public bool gameStart = false;
    public bool playFirstScene = false;
    public bool spawnWallsCheck = false;
    public bool gameOver = false;
    [Space]

    public Text Score;
    public Text ScoreBoard;
    public Text RecordBoard;
    public GameObject Hud;
    [Space]

    public int record;
    public int score;

    [Space]
    // Controll
    public GameObject playerPrefab;

    public GameObject player;
    public GameObject spawnWalls;
    public GameObject firstWall;
    [Space]

    public GameObject newPlayerGlobal;
    [Space]

    public GameObject creater;
    [Space]

    public GameObject[] walls;

    public Joystick joystick;

    [SerializeField] PlayableDirector director;
    [Space]
    public Animator animatorGameOver;

    public AudioClip clickSound;
    [Space]

    // For once use in method Update
    bool create = false;
    [Space]
    // Timer
    public float t;

    void Start () {
        player.gameObject.SetActive(false);
    }
	
	void Update ()
    {
        // What will be after cutscene
        if (director.time >= 0.6)
        {
            Destroy(player);
            if (!create)
            {
                newPlayerGlobal = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                newPlayerGlobal.name = "Player";

                newPlayerGlobal.GetComponent<PersonController>().gameControll = gameObject;
                newPlayerGlobal.GetComponent<Rigidbody2D>().simulated = true;

                newPlayerGlobal.GetComponent<PersonController>().joystick = joystick;

                create = true;

                gameStart = true;
            }

            if (gameStart)
            {
                spawnWalls.GetComponent<WallCreater>().createWall = true;
                for (int i = 0; i < walls.Length; i++)
                {
                    walls[i].GetComponent<Wall>().moveUp = true;
                    firstWall.GetComponent<Wall>().moveUp = true;
                }

                // Spawn enemy and bonus
                creater.GetComponent<Creater>().spawn = true;

                Hud.SetActive(true);
            }
            Complexity(); // Start timer and complexity check

            if (newPlayerGlobal != null)
            {
                GameOver(newPlayerGlobal);
            }
            Bonus();
            
            gameStart = false;
            playFirstScene = false;
        }
    }

    // if start button pressed
    public void ButtonStartClick ()
    {
        playFirstScene = true;
    }
    
    // Play first cutscene
    public void FirstScene()
    {
        player.gameObject.SetActive(true);
        director.Play();
    }

    public void Bonus ()
    {
        if (newPlayerGlobal != null)
        {
            score = newPlayerGlobal.GetComponent<PersonController>().score;
        }
        Score.text = score.ToString();
    }

    // Save record
    public void SetRecord ()
    {
        record = PlayerPrefs.GetInt("savescore");

        if (score > record)
        {
            PlayerPrefs.SetInt("savescore", score);
            PlayerPrefs.Save();
            Debug.Log("Record Save");
        }
    }

    // if player is dead
    public void GameOver (GameObject newPlayer)
    {
        if (newPlayer.GetComponent<PersonController>().isDead)
        {
            newPlayer.GetComponent<Animator>().SetBool("Dead", true);

            animatorGameOver.SetBool("GameOver", true);

            // Hidden HUD
            Hud.SetActive(false);

            // Save record
            SetRecord();

            // Write on board Score
            ScoreBoard.text = newPlayerGlobal.GetComponent<PersonController>().score.ToString();

            // Write on board record
            RecordBoard.text = record.ToString();

            spawnWalls.GetComponent<WallCreater>().createWall = false;

            if (firstWall != null)
            {
                firstWall.GetComponent<Wall>().moveUp = false;
            }
            
            // Search all walls
            foreach (GameObject wall in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if ((wall.name == "RandomWall(Clone)") || (wall.name == "RandomWallWithWire"))
                {
                    wall.GetComponent<Wall>().moveUp = false;
                }
            }
            creater.GetComponent<Creater>().spawn = false;
        }
    }

    // Сomplexity grows
    public void Complexity ()
    {
        if (Timer() >= 60f)
        {
            creater.GetComponent<Creater>().minDelay = 3;
            creater.GetComponent<Creater>().maxDelay = 10;
        }

        if (Timer() >= 120f)
        {
            creater.GetComponent<Creater>().minDelay = 1;
            creater.GetComponent<Creater>().maxDelay = 5;
        }

        if (Timer() >= 300f)
        {
            creater.GetComponent<Creater>().minDelay = 0;
            creater.GetComponent<Creater>().maxDelay = 1;
        }
    }

    float Timer ()
    {
        return t += Time.deltaTime;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

    public void SoundClick()
    {
        GetComponent<AudioSource>().clip = clickSound;
        GetComponent<AudioSource>().Play();
    }

}
