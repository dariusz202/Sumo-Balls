using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI pauzeText;
    public TextMeshProUGUI pauzeButtonText;
    public TextMeshProUGUI speedBoostText;
    public TextMeshProUGUI powerUpText;
    public TextMeshProUGUI projectilesText;
    public TextMeshProUGUI jumpBoostText;
    public TextMeshProUGUI jumpAndProjectilesBoostText;
    public Button restartButton;
    public Button pauzeButton;
    public int lives = 3;
    private PlayerController player;
    public bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        livesText.text = "Lives: " + lives;
      
    }

    // Update is called once per frame
    void Update()
    {
        if(player.pickJumpBoost == true)
        {
            jumpBoostText.gameObject.SetActive(true);
        }
        else
        {
            jumpBoostText.gameObject.SetActive(false);
        }
        if (player.pickPowerUp == true)
        {
            powerUpText.gameObject.SetActive(true);
        }
        else
        {
            powerUpText.gameObject.SetActive(false);
        }
        if (player.pickProjectiles == true)
        {
            projectilesText.gameObject.SetActive(true);
        }
        else
        {
            projectilesText.gameObject.SetActive(false);
        }
        if (player.pickSpeedBoost== true)
        {
            speedBoostText.gameObject.SetActive(true);
        }
        else
        {
            speedBoostText.gameObject.SetActive(false);
        }
    }
    public void Pauze()
    {
        if(pauzeButtonText.text == "Pauze" && isGameActive)
        {
            Time.timeScale = 0;
            pauzeButtonText.text = "Resume";
            pauzeText.gameObject.SetActive(true);
            isGameActive = false;
        }
        else if(pauzeButtonText.text == "Resume")
        {
            Time.timeScale = 1;
            pauzeButtonText.text = "Pauze";
            pauzeText.gameObject.SetActive(false);
            isGameActive = true;
        }
    }

    public void UpdateLives()
    {

        lives--;
        livesText.text = "Lives: " + lives;
        if(lives == 0)
        {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            isGameActive = false;
        }
    }

    public void UpdateWaveNumber(int waveNumber)
    {
        waveText.text = "Wave: " + waveNumber;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
