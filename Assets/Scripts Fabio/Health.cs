using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    bool death_health = false;
    GameObject destroy_sound;
    public GameObject Game_Manager_GO;
    public int health_wall = 2;
    public GameObject particle_children;
    public GameObject bullet;
    Rigidbody2D walls_physics;
    public float down;
    // Use this for initialization
    void Start()
    {
        destroy_sound = GameObject.Find("Destroy Object");
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

        if ((health_wall <= 0 && death_health != true))
        {
            destroy_sound.GetComponent<AudioSource>().Play();
            particle_children.gameObject.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            death_health = true;          
        }
    }

    private IEnumerator destroy_wall()
    {
        yield return new WaitForSeconds(6);
        DestroyObject(gameObject);
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

    }

}

