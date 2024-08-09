using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour
{
    public Vector2 firePoint;
    public Transform ballSpawnPoint;
    public GameObject ballPrefb;
    public float ballSpeed;

    void Update() { }

    void Fire()
    {
        var ball = Instantiate(ballPrefb, ballSpawnPoint.position, ballSpawnPoint.rotation);
        if (ball.transform.rotation == Quaternion.Euler(0f, 0f, 0f))
        {
            firePoint = new Vector2(-1f, 0.2f);
        }
        else
        {
            firePoint = new Vector2(1f, 0.2f);
        }
        ball.GetComponent<Rigidbody2D>().AddForce(firePoint * ballSpeed, ForceMode2D.Impulse);
    }
}
