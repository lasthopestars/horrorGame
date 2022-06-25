using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;
    public Image healthBarFill;

    [Header("Paused Menu")]
    public GameObject pauseMenu;

    [Header("End Game Screen")]
    public GameObject endGameScreen;
    public TextMeshProUGUI endGameHeaderText;
    public TextMeshProUGUI endGameScoreText;

    public static UIManager instance;
    /*Instance or Singleton allows us to get access from anywhere else
    in this project instantly. We don't have to do GameObject.find or any GetComponents
    Downside: you can have only one singleton of script so that means you are
    only going to have one in your project*/

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHealthBar(int currentHP, int maxHP)
    {
        healthBarFill.fillAmount = (float)currentHP / (float)maxHP;

    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score " + score;
    }

    public void UpddateAmmoText(int currentAmmo, int maxAmmo)
    {
        ammoText.text = "Ammo: " + currentAmmo + " / " + maxAmmo;
    }

    public void TogglePauseMenu(bool paused)//when the game is paused
    {
        pauseMenu.SetActive(paused);//then this menu would be activated
    }

    public void SetEndGameScreen(bool won, int score)
    {
        endGameScreen.SetActive(true);
        endGameHeaderText.text = won == true ? "You Won" : "You Lose";
        endGameHeaderText.color = won == true ? Color.green : Color.red;
        endGameScoreText.text = "<b>Score</b>\n" + score;
    }

    public void OnResumeButton()
    {
        GameManager.instance.TogglePausedGame();
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene("DungeonScene");

    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

}
