using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Enemy : Character
    {
        [SerializeField] private float Speed;
        [SerializeField] private int Health;
        private float timer = 0;
        private bool stopl = false;
        private bool stopr = false;
        private bool stopu = false;
        private bool stopd = false;
        LayerMask Obsticles;


        private bool stopper;
        public delegate void EnemyDelegate();
        public static EnemyDelegate eDelegate;

        private void Awake()
        {
            playerAnim = GetComponent<Animator>();
            Obsticles = LayerMask.GetMask("Obsticles");
        }
        private void Start()
        {
            stopper = false;
            speed = Speed;
            health = Health;
            dir = Direction.South;
            eDelegate += DecreaseHealth;
    

        }


        void Update()
        {
            timer += Time.deltaTime;
            Movement();
        }
        private bool FireRay(Direction dir)
        {
            if (dir == Direction.South)
            {
                RaycastHit2D hitDown1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.08f, transform.position.y), transform.TransformDirection(Vector2.down), 0.48f, Obsticles);
                RaycastHit2D hitDown2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.1f, transform.position.y), transform.TransformDirection(Vector2.down), 0.48f, Obsticles);
                return hitDown1 || hitDown2;
            }
            else if (dir == Direction.North)
            {
                RaycastHit2D hitUp1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.08f, transform.position.y), transform.TransformDirection(Vector2.up), 0.31f, Obsticles);
                RaycastHit2D hitUp2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.1f, transform.position.y), transform.TransformDirection(Vector2.up), 0.31f, Obsticles);
                return hitUp1 || hitUp2;
            }
            else if (dir == Direction.West)
            {
                RaycastHit2D hitLeft1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.left), 0.38f, Obsticles);
                RaycastHit2D hitLeft2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.15f), transform.TransformDirection(Vector2.left), 0.38f, Obsticles);
                return hitLeft1 || hitLeft2;
            }
            else if (dir == Direction.East)
            {
                RaycastHit2D hitRight1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.right), 0.37f, Obsticles);
                RaycastHit2D hitRight2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.15f), transform.TransformDirection(Vector2.right), 0.37f, Obsticles);
                return hitRight1 || hitRight2;
            }
            else
                return false;
        }
        protected override void Movement()
        {
            
            Debug.DrawRay(new Vector2(transform.position.x + 0.08f, transform.position.y), transform.TransformDirection(Vector2.up) * 0.31f, Color.red);
            Debug.DrawRay(new Vector2(transform.position.x - 0.1f, transform.position.y), transform.TransformDirection(Vector2.up) * 0.31f, Color.red);
            Debug.DrawRay(new Vector2(transform.position.x + 0.08f, transform.position.y), transform.TransformDirection(Vector2.down) * 0.48f, Color.blue);
            Debug.DrawRay(new Vector2(transform.position.x - 0.1f, transform.position.y), transform.TransformDirection(Vector2.down) * 0.48f, Color.blue);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.left) * 0.38f, Color.blue);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.15f), transform.TransformDirection(Vector2.left) * 0.38f, Color.blue);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.right) * 0.37f, Color.blue);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.15f), transform.TransformDirection(Vector2.right) * 0.37f, Color.blue);
            /*            RaycastHit2D hitUp1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.08f, transform.position.y), transform.TransformDirection(Vector2.up), 0.31f, Obsticles);
                        RaycastHit2D hitUp2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.1f, transform.position.y), transform.TransformDirection(Vector2.up), 0.31f, Obsticles);
                        RaycastHit2D hitDown1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.08f, transform.position.y), transform.TransformDirection(Vector2.down), 0.48f, Obsticles);
                        RaycastHit2D hitDown2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.1f, transform.position.y), transform.TransformDirection(Vector2.down), 0.48f, Obsticles);
                        RaycastHit2D hitLeft1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.left), 0.38f, Obsticles);
                        RaycastHit2D hitLeft2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.15f), transform.TransformDirection(Vector2.left), 0.38f, Obsticles);
                        RaycastHit2D hitRight1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.right), 0.37f, Obsticles);
                        RaycastHit2D hitRight2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.15f), transform.TransformDirection(Vector2.right), 0.37f, Obsticles);*/

            bool result = FireRay(dir);

            if(result)
            {
                bool[] dirs = new bool[4];
                dirs[0]= FireRay(Direction.North);
                dirs[1] = FireRay(Direction.South);
                dirs[2] = FireRay(Direction.West);
                dirs[3] = FireRay(Direction.East);
                List<Direction> avails = new List<Direction>();
                if(!dirs[0])
                {
                    avails.Add(Direction.North);
                }
                if(!dirs[1])
                {
                    avails.Add(Direction.South);
                }
                if(!dirs[2])
                {
                    avails.Add(Direction.West);
                }
                if (!dirs[3])
                {
                    avails.Add(Direction.East);
                }
                dir = avails[Random.Range(0, avails.Count)];
                stopper = false;
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


        
        private void Translate(int val, Direction dir)
        {

            if (dir == Direction.North || dir == Direction.South)
            {
                if (val == 1 && stopper == false)
                {
                    stopper = true;
                    playerAnim.SetTrigger("North");
                }
                else if (val == -1 && stopper == false)
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
                if (val == 1 && stopper == false)
                {
                    stopper = true;
                    playerAnim.SetTrigger("East");
                }
                else if (val == -1 && stopper == false)
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
}

