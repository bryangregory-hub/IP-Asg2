using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // brings users to start playing with the AR Camera
    public void PlayGame()
    {
        SceneManager.LoadScene(2);


    }
        // Quits the player from the application
    public void Quit()
    {
        Application.Quit();
    }
}
