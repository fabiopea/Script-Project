using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    GameObject Health_Sound;
    GameObject Damage_Sound;
    GameObject Bullet_Sound;
    public GameObject Health;
    private GameObject Main_Camera;
    public GameObject Game_Manager;
    Rigidbody2D Player_Physics;
    public float Speed = 5f;
    public GameObject Bullet;
    Rigidbody2D Physics_bullet;


    // Use this for initialization
    void Start()
    {
        Health_Sound = GameObject.Find("Health Sound");
        Damage_Sound = GameObject.Find("Damage Sound");
        Bullet_Sound = GameObject.FindGameObjectWithTag("Bullet_Sound");
        Main_Camera = GameObject.Find("Main Camera");
        Player_Physics = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move_horizontal = Input.GetAxis("Horizontal");
        float move_vertical = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(move_horizontal, move_vertical);
        Player_Physics.velocity = direction * Speed;

    }
    private void LateUpdate()
    {

        Health = GameObject.FindGameObjectWithTag("Health");
        if (Input.GetKeyDown("space"))
        {
            GameObject bullet = Instantiate(Bullet);
            bullet.transform.parent = gameObject.transform;
            bullet.transform.position = gameObject.transform.position;
            Physics_bullet = bullet.GetComponent<Rigidbody2D>();
            Physics_bullet.velocity = new Vector2(0, 4);
            Bullet_Sound.GetComponent<AudioSource>().Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Wall"))
        {
            Game_Manager.GetComponent<Game_Manager>().health -= 2;
            Main_Camera.GetComponent<postVHSPro>().jitterHAmount = 5;
            Main_Camera.GetComponent<postVHSPro>().filmGrainAmount = 0.1f;
            Damage_Sound.GetComponent<AudioSource>().Play();
            StartCoroutine(return_amount());
        }
        if (collision.gameObject.tag == ("Health"))
        {
            Health_Sound.GetComponent<AudioSource>().Play();
            Health.GetComponent<BoxCollider2D>().enabled = false;
            Health.GetComponent<SpriteRenderer>().enabled = false;
            Game_Manager.GetComponent<Game_Manager>().health += 5;
        }
    }



    private IEnumerator return_amount()
    {
        yield return new WaitForSeconds(1);
        Main_Camera.GetComponent<postVHSPro>().jitterHAmount = 1;
        Main_Camera.GetComponent<postVHSPro>().filmGrainAmount = 0.016f;
    }
}
