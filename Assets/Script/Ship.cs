using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
