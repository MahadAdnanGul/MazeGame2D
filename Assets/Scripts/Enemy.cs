using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float Speed;
    [SerializeField] private int Health;
    private float timer = 0;
    private bool stopl = false;
    private bool stopr = false;
    private bool stopu = false;
    private bool stopd = false;

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
        dir = Direction.South;

    }


    void Update()
    {
        timer += Time.deltaTime;
        Movement();
    }
    protected override void Movement()
    {
        LayerMask Obsticles = LayerMask.GetMask("Obsticles");
        Debug.DrawRay(new Vector2(transform.position.x+0.17f,transform.position.y), transform.TransformDirection(Vector2.up)*0.2f,Color.red);
        Debug.DrawRay(new Vector2(transform.position.x - 0.17f, transform.position.y), transform.TransformDirection(Vector2.up) * 0.2f, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x + 0.17f, transform.position.y), transform.TransformDirection(Vector2.down) * 0.36f, Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x - 0.17f, transform.position.y), transform.TransformDirection(Vector2.down) * 0.36f, Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y+0.085f), transform.TransformDirection(Vector2.left) * 0.3f, Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.3f), transform.TransformDirection(Vector2.left) * 0.3f, Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.085f), transform.TransformDirection(Vector2.right) * 0.29f, Color.blue);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y -0.3f), transform.TransformDirection(Vector2.right) * 0.29f, Color.blue);
        RaycastHit2D hitUp1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.17f, transform.position.y), transform.TransformDirection(Vector2.up), 0.17f,Obsticles);
        RaycastHit2D hitUp2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.17f, transform.position.y), transform.TransformDirection(Vector2.up), 0.17f, Obsticles);
        RaycastHit2D hitDown1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.17f, transform.position.y), transform.TransformDirection(Vector2.down), 0.36f, Obsticles);
        RaycastHit2D hitDown2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.17f, transform.position.y), transform.TransformDirection(Vector2.down), 0.36f, Obsticles);
        RaycastHit2D hitLeft1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.085f), transform.TransformDirection(Vector2.left), 0.27f, Obsticles);
        RaycastHit2D hitLeft2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.3f), transform.TransformDirection(Vector2.left), 0.27f, Obsticles);
        RaycastHit2D hitRight1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.085f), transform.TransformDirection(Vector2.right), 0.26f, Obsticles);
        RaycastHit2D hitRight2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y -0.3f), transform.TransformDirection(Vector2.right), 0.26f, Obsticles);

        bool left = hitLeft1 && hitLeft2;
        bool right = hitRight1 && hitRight2;
        bool up = hitUp1 && hitUp2;
        bool down = hitDown1 && hitDown2;


        
        if(right)
        {
            dir = Direction.West;
            stopper = false;
        }
        else if(down)
        {
            dir = Direction.North;
            stopper = false;
        }
        else if(left)
        {
            dir = Direction.East;
            stopper = false;
        }
        else if(up)
        {
            dir = Direction.South;
            stopper=false;
        }
   

        


        switch (dir)
        {
            case Direction.North:
                Translate(1, dir);
                break;
            case Direction.South:
                Translate(-1, dir);
                break;
            case Direction.East:
                Translate(1, dir);
                break;
            case Direction.West:
                Translate(-1, dir);
                break;
            default:
                Translate(1, Direction.North);
                break;
        }

    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerCollision(collision.gameObject.GetComponent<Player>());
        }
    }

    private void PlayerCollision(Player player)
    {
        if (player.dir == dir)
        {
            DecreaseHealth();
            player.CheckWin();
        }
        else
        {
            player.DecreaseHealth();
        }
    }
    private void Translate(int val, Direction dir)
    {

        if (dir == Direction.North || dir == Direction.South)
        {
            if(val==1&&stopper==false)
            {
                stopper = true;
                playerAnim.SetTrigger("North");
            }
            else if(val==-1&& stopper == false)
            {
                stopper = true;
                playerAnim.SetTrigger("South");
            }
            float tempv = gameObject.transform.position.y;
            tempv += speed * Time.deltaTime * val;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, tempv, gameObject.transform.position.z);
        }
        else
        {
            if (val == 1&& stopper == false)
            {
                stopper = true;
                playerAnim.SetTrigger("East");
            }
            else if(val==-1&& stopper == false)
            {
                stopper = true;
                playerAnim.SetTrigger("West");
            }
            float tempv = gameObject.transform.position.x;
            tempv += speed * Time.deltaTime * val;
            gameObject.transform.position = new Vector3(tempv, gameObject.transform.position.y, gameObject.transform.position.z);
        }

    }
    public void ChangeDirection(Direction d)
    {
        stopper = false;
        
        dir = d;
    }
}
