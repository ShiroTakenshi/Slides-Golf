using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] PlayerController player;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] Hole hole;
    [SerializeField] AudioSource audiosfx;


    private void Start()
    {
        gameOverPanel.SetActive(false);
        audiosfx.Play();
    }
    private void Update()
    {
        if (hole.Entered && gameOverPanel.activeInHierarchy == false)
        {
            pauseGame();
            gameOverPanel.SetActive(true);
            gameOverText.text = "Finished! Shoot Count : " + player.ShootCount;
            player.enabled = false;

        }
    }

    public void BackToMainMenu()
    {
        SceneLoader.Load("MainMenu");
        resumeGame();
    }

    public void Replay()
    {
        SceneLoader.ReloadLevel();
        resumeGame();
    }

    public void PlayNext()
    {
        SceneLoader.LoadNextLevel();
        resumeGame();
    }

    public void pauseGame()
    {
        Time.timeScale = 0.30f;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
    }

}
