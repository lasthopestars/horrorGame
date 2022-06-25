using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    

   

    public void OnPlayButton()
    {
        SceneManager.LoadScene("DungeonScene");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
   
}
