using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public Text deadText;
    private bool valid = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && valid)
        {
            if(isPaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        GameManager.playerDead = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void FadeOut()
    {
        if(GameManager.playerDead)
        {
            switch(GameManager.whyDead)
            {
                case GameManager.CauseOfDeath.Health :
                    deadText.text = "You Are Dead";
                    deadText.color = new Color(255,0,0,0);
                    break;
                case GameManager.CauseOfDeath.Curse :
                    deadText.text = "You Are Cursed";
                    deadText.color = new Color(89,0,183,0);
                    break;
            }
            gameObject.GetComponent<Animator>().SetTrigger("gameover");
            FindObjectOfType<AudioManager>().Play("GameOverTheme");
            valid = false;
        }else
        {
            if(GameManager.level < GameManager.maxLevel)
            {
                GameManager.level++;
                SceneManager.LoadScene (SceneManager.GetActiveScene().name);
            }
            else
            {
                GameManager.level = 1;
                SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        GameManager.playerDead = false;
    }

    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
}
