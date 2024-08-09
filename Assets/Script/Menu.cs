using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public int isMobile;
    public TMP_Text a;

    private void Start()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                GameObject button4 = GameObject.Find("BButton");
                if (PlayerPrefs.GetInt("isMobile") == 0)
                {
                    button4.SetActive(false);
                }
                else
                {
                    button4.SetActive(true);
                }
            }
            GameObject button = GameObject.Find("LeftButton");
            GameObject button1 = GameObject.Find("RightButton");
            GameObject button2 = GameObject.Find("JumpButton");
            GameObject button3 = GameObject.Find("AButton");
            if (PlayerPrefs.GetInt("isMobile") == 0)
            {
                button.SetActive(false);
                button1.SetActive(false);
                button2.SetActive(false);
                button3.SetActive(false);
            }
            else
            {
                button.SetActive(true);
                button1.SetActive(true);
                button2.SetActive(true);
                button3.SetActive(true);
            }
        }
        else
        {
            switch (Application.isMobilePlatform)
            {
                case true:
                    PlayerPrefs.SetInt("isMobile", 1);
                    break;
                case false:
                    PlayerPrefs.SetInt("isMobile", 0);
                    break;
            }
            a.text = Application.isMobilePlatform + "";
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchMobile()
    {
        isMobile = 1;
        PlayerPrefs.SetInt("isMobile", isMobile);
    }

    public void SwitchPC()
    {
        isMobile = 0;
        PlayerPrefs.SetInt("isMobile", isMobile);
    }

    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void BossFight()
    {
        SceneManager.LoadScene(2);
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
