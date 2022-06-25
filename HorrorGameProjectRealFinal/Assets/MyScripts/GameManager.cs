using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scoreToWin;
    public int currentScore;

    public bool gamePaused;

    public static GameManager instance;
    public Canvas canvas;

    private void Awake()
    {

       
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            TogglePausedGame();
    }

    public void TogglePausedGame()
    {
        /* if the game is not Paused then gamePaused is set to be true
         if the game is Paused then gamePaused is set to be false*/
        gamePaused = !gamePaused;

        Time.timeScale = gamePaused == true ? 0.0f : 1.0f;

        Cursor.lockState = gamePaused == true ? CursorLockMode.None : CursorLockMode.Locked;

        //toggle the pause menu
        UIManager.instance.TogglePauseMenu(gamePaused);
    }

    public void AddScore(int score)
    {
        currentScore += score; 
        UIManager.instance.UpdateScoreText(currentScore);
        if (currentScore >= scoreToWin)
            WinGame();
    }

    void WinGame()
    {
        //set the end game screen
        UIManager.instance.SetEndGameScreen(true, currentScore);
        Time.timeScale = 0.0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoseGame()
    {
        //set the end game screen
        UIManager.instance.SetEndGameScreen(false, currentScore);
        Time.timeScale = 0.0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
