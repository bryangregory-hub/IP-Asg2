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
    public GameObject dataButtons;
    public GameObject menuButtons;

    public TextMeshProUGUI displayName;

    public void Awake()
    {
        dataButtons = GameObject.Find("Data Buttons");
        dataButtons.SetActive(false);
        displayName.text = "Player: " + auth.GetCurrentUserDisplayName();

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

    public void ActivateDataMenu()
    {
        dataButtons = GameObject.Find("Data Buttons");
        dataButtons.SetActive(true);

        menuButtons = GameObject.Find("Menu Buttons");
        menuButtons.SetActive(false);

    }

    public void ActivateGameMenu()
    {
        menuButtons = GameObject.Find("Menu Buttons");
        menuButtons.SetActive(true);

        dataButtons = GameObject.Find("Data Buttons");
        dataButtons.SetActive(false);
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
