using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Character : MonoBehaviour
    {
        protected float speed;
        protected int health;
        protected Animator playerAnim;
        public enum Direction { North, South, East, West, Null };
        public Direction dir;

        public Character()
        {
            speed = 1f;
            health = 1;
        }

        public Character(float setSpeed, int setHealth)
        {
            speed = setSpeed;
            health = setHealth;
        }
        public virtual void DecreaseHealth()
        {
            health--;
            CheckHealth();
        }
        protected virtual void Movement()
        {

        }
        protected void CheckHealth()
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

