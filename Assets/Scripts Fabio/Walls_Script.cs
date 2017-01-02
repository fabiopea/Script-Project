using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Walls_Script : MonoBehaviour
{
    GameObject Destroy_Sound;
    BoxCollider2D[] colliders;
    private bool add_score_bool = false;
    public GameObject Game_Manager_GO;
    public GameObject Children;
    private int health_wall = 2;
    public GameObject particle_children;
    public GameObject bullet;
    Rigidbody2D walls_physics;
    public float down;
    // Use this for initialization
    void Start()
    {
        Destroy_Sound = GameObject.Find("Destroy Object");
        colliders = gameObject.GetComponents<BoxCollider2D>();
        Game_Manager_GO = GameObject.FindGameObjectWithTag("Game Manager");
        walls_physics = GetComponent<Rigidbody2D>();
        StartCoroutine(destroy_wall());
    }

    // Update is called once per frame
    void Update()
    {
        bullet = GameObject.FindGameObjectWithTag("Bullet");
        down = Game_Manager_GO.GetComponent<Game_Manager>().down_walls;
        walls_physics.velocity = new Vector2(0, down);
        wall_death();
    }

    private void wall_death()
    {

        if (health_wall <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            foreach (BoxCollider2D bc in colliders) bc.enabled = false;
            Children.GetComponent<SpriteRenderer>().enabled = false;
            particle_children.gameObject.SetActive(true);
            addscore();
            add_score_bool = true;
        }
    }

    void addscore()
    {
        if (add_score_bool == false)
        {
            Destroy_Sound.GetComponent<AudioSource>().Play();
            Game_Manager_GO.GetComponent<Game_Manager>().Score += 5;
        }
    }

    private IEnumerator destroy_wall()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.gameObject.tag == ("Bullet"))
            {
                Destroy(bullet);
                health_wall -= 1;
            }

        }
        if (collision.gameObject.tag == ("Trigger 1"))
        {
            Game_Manager_GO.GetComponent<Game_Manager>().health -= 2;
        }
    }


}
