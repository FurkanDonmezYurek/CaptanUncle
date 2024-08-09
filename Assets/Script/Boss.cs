using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public bool isWait;
    float AttackTime;
    float stage2Attack;
    float stageCounter;
    float turnBoss;
    public float stageSwitch;
    Animator animator;
    EnemyHealthSystem enemyHealthSystem;
    public Vector2 attackWaitRange;

    /////for Attack1
    float fireWait;
    public float fireWaitRadius;
    Transform firePoint;
    GameObject player;
    float cosFire;
    float sinFire;
    public GameObject ballPrefb;
    bool attack1IsGoing;
    float ballCountPriv;
    public float ballCount;

    /////for Attack2
    public GameObject enemy;
    public float maxEnemy;
    float i;
    Vector3 spawnPoint;
    float posX;
    public float spawnRadius;

    //LifeBar
    public Slider healthSlider;
    float maxHealth;
    float currentHealth;

    //
    GameObject Camera;
    bool shaked;

    void Start()
    {
        isWait = true;
        animator = this.gameObject.GetComponent<Animator>();
        enemyHealthSystem = this.gameObject.GetComponent<EnemyHealthSystem>();
        stageCounter = 1;
        animator.SetBool("Idle", true);

        spawnPoint = GameObject.Find("SpawnPoint").gameObject.transform.position;
        firePoint = GameObject.Find("FirePoint").gameObject.transform;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        attack1IsGoing = false;
        //LifeBar start full
        currentHealth = enemyHealthSystem.health;
        maxHealth = enemyHealthSystem.health;
        UpdateLifeBar();
        //for shake
        Camera = GameObject.Find("MainCamera");
        shaked = false;
    }

    void Update()
    {
        UpdateLifeBar();
        currentHealth = enemyHealthSystem.health;
        //Switch Stage
        if (enemyHealthSystem.health < stageSwitch)
        {
            animator.SetTrigger("StageSwitch");
            stageCounter = 2;
        }
        //Waiting for next Attack
        if (isWait)
        {
            switch (stageCounter)
            {
                case 1:
                    AttackTime = Random.Range(attackWaitRange.x, attackWaitRange.y);
                    break;
                case 2:
                    AttackTime = Random.Range(attackWaitRange.x / 1.5f, attackWaitRange.y / 1.5f);
                    break;
            }

            AttackTime += Time.time;
            isWait = false;

            //Stage 2 Next Attack
            stage2Attack = Random.Range(0, 3);

            //Turn Boss
            turnBoss = player.transform.position.x - this.gameObject.transform.position.x;
            if (turnBoss > 0)
            {
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 180, 0f);
            }
            else
            {
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 0, 0f);
            }
        }
        switch (stageCounter)
        {
            //for Stage 1
            case 1:
                if (animator.GetBool("Idle"))
                {
                    if (Time.time > AttackTime)
                    {
                        animator.ResetTrigger("HitEnemy");
                        animator.SetTrigger("Attack0");
                        animator.SetBool("Idle", false);
                    }
                }
                break;
            //for Stage 2
            case 2:
                if (animator.GetBool("Idle"))
                {
                    if (Time.time > AttackTime)
                    {
                        switch (stage2Attack)
                        {
                            case 0:
                                animator.SetTrigger("Attack0");
                                break;
                            case 1:
                                animator.SetBool("Attack1", true);
                                break;
                            case 2:
                                animator.SetBool("Attack2", true);

                                break;
                        }

                        fireWait = Time.time;

                        animator.ResetTrigger("HitEnemy");
                        animator.SetBool("Idle", false);
                    }
                }
                break;
        }

        //Boss Attack1
        if (animator.GetBool("Attack1"))
        {
            if (attack1IsGoing)
            {
                if (GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    animator.SetBool("Attack1", false);
                    animator.SetBool("Idle", true);
                    attack1IsGoing = false;

                    isWait = true;
                    ballCountPriv = 0;
                }
            }
            else
            {
                if (ballCountPriv > ballCount)
                {
                    animator.SetBool("Attack1", false);
                    animator.SetBool("Idle", true);
                    isWait = true;
                    ballCountPriv = 0;
                }
            }

            if (fireWait < Time.time)
            {
                cosFire = player.transform.position.x - firePoint.position.x;
                sinFire = player.transform.position.y + 15f;
                Vector2 fireForce = new Vector2(cosFire / 2.5f, sinFire);
                var ball = Instantiate(ballPrefb, firePoint.position, firePoint.rotation);
                ball.GetComponent<Rigidbody2D>().AddForce(fireForce, ForceMode2D.Impulse);
                fireWait += fireWaitRadius;
                ballCountPriv++;
            }
        }
        //Boss Attack2
        if (animator.GetBool("Attack2"))
        {
            if (i < maxEnemy)
            {
                posX = Random.Range(spawnPoint.x - spawnRadius, spawnPoint.x + spawnRadius);
                GameObject enemyClone = Instantiate(
                    enemy,
                    new Vector3(posX, spawnPoint.y),
                    Quaternion.identity
                );
                enemyClone.GetComponent<EnemyManager>().triggerRange = 99;
                i++;
            }
            if (i == maxEnemy)
            {
                animator.SetBool("Attack2", false);
                animator.SetBool("Attack1", true);
                attack1IsGoing = true;
                animator.ResetTrigger("HitEnemy");
                i = 0;
            }
        }

        if (animator.GetBool("IsDead") && shaked == false)
        {
            shaked = true;
            if (turnBoss > 0)
            {
                Camera.GetComponent<Animator>().SetTrigger("ShakeLeft");
            }
            else
            {
                Camera.GetComponent<Animator>().SetTrigger("ShakeRight");
            }
        }
    }

    private void UpdateLifeBar()
    {
        healthSlider.value = currentHealth / maxHealth;
    }
}
