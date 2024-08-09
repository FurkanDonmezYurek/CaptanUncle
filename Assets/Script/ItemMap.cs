using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemMap : MonoBehaviour, IInteractable
{
    public GameObject button;

    public void Interact()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //player collidere girince
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            button.SetActive(true);
        }
    }

    //player colliderden çıkarken
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            button.SetActive(false);
        }
    }
}
