using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BossLevelManager : MonoBehaviour
{
    public GameObject redPotion;
    Transform redPotionSpawnPoint;
    bool redPotionTimerStart;
    bool redPotionSpawnStart;
    float redPotionTimer;
    public Vector2 redPotionTimerRadius;
    public float redPotionSpawnRadius;
    float redPotionSpawn;

    private void Start()
    {
        redPotionSpawnPoint = GameObject.Find("SpawnPoint").transform;
        redPotionTimerStart = true;
    }

    public void Update()
    {
        RedPotionTimer();
        RedPotionSpawn();
    }

    void RedPotionTimer()
    {
        if (redPotionTimerStart)
        {
            redPotionTimer = Random.Range(redPotionTimerRadius.x, redPotionTimerRadius.y);
            redPotionSpawn = Random.Range(
                redPotionSpawnPoint.position.x - redPotionSpawnRadius,
                redPotionSpawnPoint.position.x + redPotionSpawnRadius
            );
            redPotionTimer += Time.time;
            redPotionTimerStart = false;
            redPotionSpawnStart = true;
        }
    }

    void RedPotionSpawn()
    {
        if (redPotionSpawnStart)
        {
            if (redPotionTimer < Time.time)
            {
                Instantiate(
                    redPotion,
                    new Vector2(redPotionSpawn, redPotionSpawnPoint.position.y),
                    Quaternion.identity
                );
                redPotionSpawnStart = false;
                redPotionTimerStart = true;
            }
        }
    }
}
