using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject cloud;
    Vector3 spawnPoint;
    float posY;
    float posX;
    float spawnRate;
    public float nextSpawn;
    public float spawnRadius;

    void Update()
    {
        if (Time.time > spawnRate)
        {
            spawnRate = Time.time + nextSpawn;
            posY = Random.Range(
                this.gameObject.transform.position.y - spawnRadius,
                this.gameObject.transform.position.y + spawnRadius
            );
            posX = Random.Range(
                this.gameObject.transform.position.x - spawnRadius,
                this.gameObject.transform.position.x + spawnRadius
            );
            spawnPoint = new Vector3(posX, posY);
            Instantiate(cloud, spawnPoint, Quaternion.identity);
        }
    }
}
