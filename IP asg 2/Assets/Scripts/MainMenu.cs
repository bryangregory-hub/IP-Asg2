using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Database;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public AuthManager auth;
    public GameObject signOut;

    public GameObject gameMenu;
    public GameObject statsMenu;

    public TextMeshProUGUI displayName;

    public void Awake()
    {

        displayName.text = "Player: " + auth.GetCurrentUserDisplayName();

        statsMenu = GameObject.Find("Stats");
        statsMenu.SetActive(false);

    }

    public void DisplayGameMenuButton()
    {
        gameMenu = GameObject.Find("Menu Buttons");
        gameMenu.SetActive(true);
        statsMenu = GameObject.Find("Stats");
        statsMenu.SetActive(false);
    }

    public void DisplayStatsMenu()
    {
        statsMenu = GameObject.Find("Menu Buttons");
        statsMenu.SetActive(true);

        gameMenu = GameObject.Find("Stats");
        gameMenu.SetActive(false);
    }

    public void SignOut()
    {
        auth.SignOut();
    }

    // brings users to start playing with the AR Camera
    public void PlayGame()
    {
        SceneManager.LoadScene("Main scene");
    }
    
    public void Quiz()
    {
        SceneManager.LoadScene("Quiz");
    }
        // Quits the player from the application
    public void Quit()
    {
        Application.Quit();
    }
    private void OnTriggerEnter(Collider other)
    {
        Quiz();
    }
}
