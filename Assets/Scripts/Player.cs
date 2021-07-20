﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Character
{
    [SerializeField] private float Speed;
    [SerializeField] private int Health;
    private int gems;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI gemText;
    private bool stopper;




    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }
    private void Start()
    {
        stopper = false;
        speed = Speed;
        health = Health;
        healthText.text = "health: " + health;
        gems = 0;
        gemText.text = "Gems: " + gems;
    }


    void Update()
    {
        Movement();
    }
    protected override void Movement()
    {
        float input_vertical = Input.GetAxis("Vertical");
        float input_horizontal = Input.GetAxis("Horizontal");
        if (input_vertical != 0)
        {
            stopper = true;
            if (input_vertical > 0 && dir != Direction.North)
            {
                playerAnim.SetTrigger("North");
                dir = Direction.North;
            }
            else if (input_vertical < 0 && dir != Direction.South)
            {
                playerAnim.SetTrigger("South");
                dir = Direction.South;
            }
            float tempv = gameObject.transform.position.y;
            tempv += speed * Time.deltaTime * input_vertical;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, tempv, gameObject.transform.position.z);
        }
        else if (input_horizontal != 0)
        {
            stopper = true;
            if (input_horizontal > 0 && dir != Direction.East)
            {
                playerAnim.SetTrigger("East");
                dir = Direction.East;
            }
            else if (input_horizontal < 0 && dir != Direction.West)
            {
                playerAnim.SetTrigger("West");
                dir = Direction.West;
            }
            float temph = gameObject.transform.position.x;
            temph += speed * Time.deltaTime * input_horizontal;
            gameObject.transform.position = new Vector3(temph, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else
        {
            if (stopper == true)
            {
                playerAnim.SetTrigger("Stop");
                stopper = false;
                dir = Direction.Null;
            }
        }

    }

    override public void DecreaseHealth()
    {
        health--;
        healthText.text = "Health: " + health;
        CheckHealth();
    }
    public void CheckWin()
    {
        if(FindObjectOfType<Enemy>()==null&&GameObject.FindGameObjectWithTag("Gem")==null)
        {
            Debug.Log("Win");
        }
        else
        {
            Debug.Log(FindObjectOfType<Enemy>());
            Debug.Log(GameObject.FindGameObjectWithTag("Gem"));
        }
    }
    public void CollectGem()
    {
        gems++;
        gemText.text = "Gems: " + gems;
        //Destroy(collision.gameObject);
        Invoke("CheckWin", 0.5f);
    }
}
