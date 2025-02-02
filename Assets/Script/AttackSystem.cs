using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    int comboCount;
    GameObject Player;
    GameObject Enemy;
    Animator animator;
    Animator animatorEnemy;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = Player.GetComponent<Animator>();
    }

    void Update()
    {
        //Attack and combo -----Input.GetMouseButtonDown(0) ||
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (animator.GetBool("Jump") == false)
            {
                switch (comboCount)
                {
                    case 0:
                        animator.SetTrigger("Attack1");
                        comboCount++;
                        break;
                    case 1:
                        animator.SetTrigger("Attack2");
                        comboCount++;
                        break;
                    case 2:
                        animator.SetTrigger("Attack3");
                        comboCount = 0;
                        break;
                }
            }
        }
    }

    public void AttackMobile()
    {
        if (animator.GetBool("Jump") == false)
        {
            switch (comboCount)
            {
                case 0:
                    animator.SetTrigger("Attack1");
                    comboCount++;
                    break;
                case 1:
                    animator.SetTrigger("Attack2");
                    comboCount++;
                    break;
                case 2:
                    animator.SetTrigger("Attack3");
                    comboCount = 0;
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy = other.gameObject;
        animatorEnemy = Enemy.GetComponent<Animator>();
        //Enemy damge-push-animation trigger function
        if (
            LayerMask.LayerToName(other.gameObject.layer) == "Enemy"
            && other.gameObject.TryGetComponent(out EnemyHealthSystem healthEnemy)
        )
        {
            if (other.transform.tag == "Boss" && animatorEnemy.GetBool("Idle"))
            {
                if (healthEnemy.health > 0)
                {
                    healthEnemy.health--;
                    healthEnemy.EnemyPush();
                    animatorEnemy.SetTrigger("HitEnemy");
                }
            }
            if (other.transform.tag == "Enemy" && healthEnemy.health > 0)
            {
                healthEnemy.health--;
                healthEnemy.EnemyPush();
                animatorEnemy.SetTrigger("HitEnemy");
            }
        }
    }
}
