using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game_Manager : MonoBehaviour
{
    GameObject destroy_sound;
    GameObject BG_Sound;
    bool death_player = false;
    GameObject Health_Sound;
    GameObject Death_Sound;
    private bool change_down = false;
    private int random_walls;
    public GameObject Health;
    public float down_walls = -2f;
    public GameObject wall_instantiate;
    SpriteRenderer[] Players;
    public GameObject Player;
    public GameObject particle;
    public int Score;
    public float health = 100f;
    private GameObject Score_gameobject;
    private GameObject Health_gameobject;
    private Text score_text;
    private Text health_text;
    private string Health_string;
    private string Score_String;
    private bool bool_position = false;
    public GameObject[] spawns;
    private int random_spawn;
    private int random_spawn2;
    public GameObject[] walls;
    private int index_random;
    private int index_random2;
    public GameObject Wall_position;
    private GameObject Wall;
    // Use this for initialization
    void Start()
    {
        destroy_sound = GameObject.Find("Destroy Object");
        BG_Sound = GameObject.Find("Background Music");
        Health_Sound = GameObject.Find("Health Sound");
        Death_Sound = GameObject.Find("Death Sound");
        Players = Player.GetComponentsInChildren<SpriteRenderer>();
        Score_gameobject = GameObject.Find("Score");
        Health_gameobject = GameObject.Find("Health");
        score_text = Score_gameobject.GetComponent<Text>();
        health_text = Health_gameobject.GetComponent<Text>();
        StartCoroutine(delayspawn());
    }

    private IEnumerator delayspawn()
    {
        yield return new WaitForSeconds(3);
        if (Score >= 50)
        {
            second_run();
        }
        else
        {
            first_run();
        }
    }

    void second_run()
    {

        if (change_down != true)
        {
            down_walls = -1f;
            change_down = true;
        }
        else
        {
            down_walls -= 0.1f;
        }
        if (GameObject.FindGameObjectWithTag("Health"))
        {
            index_random = UnityEngine.Random.Range(0, 4);
            index_random2 = UnityEngine.Random.Range(0, 4);
            spawn_walls_2();
        }
        else
        {
            index_random = UnityEngine.Random.Range(0, 5);
            index_random2 = UnityEngine.Random.Range(0, 5);
            spawn_walls_2();
        }


    }

    void spawn_walls_2()
    {
        Player.GetComponent<Controller>().Speed = 10f;
        GameObject First_Instance = Instantiate(walls[index_random]);
        GameObject Second_Instance = Instantiate(walls[index_random2]);
        random_spawn = UnityEngine.Random.Range(0, 6);
        random_spawn2 = UnityEngine.Random.Range(0, 6);
        First_Instance.transform.position = spawns[random_spawn].transform.position;
        Second_Instance.transform.position = spawns[random_spawn2].transform.position;
        if (random_spawn == random_spawn2)
        {
            DestroyObject(Second_Instance);
            StartCoroutine(delayspawn());
        }
        else
        {
            StartCoroutine(delayspawn());
        }

    }

    void first_run()
    {
        down_walls -= 0.1f;
        if (GameObject.FindGameObjectWithTag("Health"))
        {
            index_random = UnityEngine.Random.Range(0, 4);
            spawn_walls();
        }
        else
        {
            index_random = UnityEngine.Random.Range(0, 5);
            spawn_walls();
        }
    }

    void spawn_walls()
    {
        Wall = walls[index_random];
        wall_instantiate = Instantiate(Wall);
        random_spawn = UnityEngine.Random.Range(0, 6);
        Wall_position = spawns[random_spawn];
        bool_position = true;
        StartCoroutine(delayspawn());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Health = GameObject.FindGameObjectWithTag("Health");
        health = Mathf.Clamp(health, 0, 100f);
        health_text.GetComponent<Text>().text = Health_string;
        Health_string = health.ToString("Health:") + health;
        score_text.GetComponent<Text>().text = Score_String;
        Score_String = Score.ToString("Score:") + Score;
        if (bool_position != false)
        {
            Wall.transform.position = Wall_position.transform.position;
        }
        player_death();
    }

    private void player_death()
    {
        if ((health <= 0 && death_player != true))
        {
            BG_Sound.GetComponent<AudioSource>().Stop();
            destroy_sound.GetComponent<AudioSource>().Play();            
            Death_Sound.GetComponent<AudioSource>().Play();
            particle.SetActive(true);
            Player.GetComponent<Controller>().Speed = 0;
            Player.GetComponent<BoxCollider2D>().enabled = false;
            Player.GetComponent<SpriteRenderer>().enabled = false;
            foreach (SpriteRenderer SR in Players) SR.enabled = false;
            death_player = true;
            StartCoroutine(Load_scena_1());

        }
    }

    private IEnumerator Load_scena_1()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Scena 1");
    }
}
