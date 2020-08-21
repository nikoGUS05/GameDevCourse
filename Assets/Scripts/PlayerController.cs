﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    public int jumpForce;

    public Transform groundPoint;

    public LayerMask groundLayer;

    bool grounded;

    public Transform effectPosition;
    
    public GameObject deathEffect;

    public GameObject gameOver;

    public GameObject shield;

    public bool shieldOn = false;

    public float shieldCDTime = 2f;
    float shieldCD = 0;
    bool shieldOnCD = false;


     
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        shield.gameObject.SetActive(false);
    }
    //Update method used for physics in our game, because it happens every fixed amount of time

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapPoint(groundPoint.position, groundLayer);
    }
    // Update is called once per frame
    void Update()
    {
        

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            rb.AddForce(Vector2.up * jumpForce);
        }

       anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("Grounded", grounded);


        if (!shieldOnCD && Input.GetKeyDown(KeyCode.S))
        {
            TurnOnShield();
        }
        if (shieldOnCD)
        {
            shieldCD += Time.deltaTime;
            if(shieldCD >= shieldCDTime * 2)
            {
               shieldCD = 0f;
                shieldOnCD = false;
            }
        }
    }
   public void jump()
    {
        if (grounded) 
        {
            anim.SetTrigger("Jump");
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
    public void TurnOnShield()
    {
        StartCoroutine(ShieldUpCo());
    }

    IEnumerator ShieldUpCo()
    {
        shieldOnCD = true;
        shield.gameObject.SetActive(true);
        shieldOn = true;
        yield return new WaitForSeconds(shieldCDTime);
        shield.gameObject.SetActive(false);
        shieldOn = false;
    }
    public void GameOver()
    {
        GameManager.instance.cam.followplayer = false;
        Instantiate(deathEffect, effectPosition.position, Quaternion.identity);
        gameOver.SetActive(true);
        Destroy(gameObject);
    }
}
