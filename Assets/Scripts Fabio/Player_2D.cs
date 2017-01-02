using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2D : MonoBehaviour {
    bool bool_collision;
    private float move;
    public float jump = 10f;
    public float Speed = 30f;
    Rigidbody2D rigid;
    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
    if ((Input.GetKeyDown("space")&& bool_collision != false))
    {
            bool_collision = false;
            rigid.AddForce(new Vector2(0, jump * 1500));
    }
        move = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(move * Speed, rigid.velocity.y);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     if (collision.gameObject.tag == ("Floor"))
     {
            bool_collision = true;
     }
    
   }

}
