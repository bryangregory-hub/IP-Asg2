/*
Author: Charlene Soh Jing Ying

Name of Class: MainMenu

Description of Class: This class deals with the mainmenu and the canvas 

Date Created: 11/2/2022
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Database;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // firebase managers
    public AuthManager auth;
    public GameObject signOut;

    // game object variables 
    public GameObject gameMenu;
    public GameObject statsMenu;

    // text mesh pro s
    public TextMeshProUGUI displayName;

    public void Awake()
    {

        displayName.text = "Player: " + auth.GetCurrentUserDisplayName();

        statsMenu = GameObject.Find("StatMenu Variant");
        statsMenu.SetActive(false);

    }

    public void DisplayGameMenuButton()
    {
        gameMenu = GameObject.Find("Menu Buttons");
        gameMenu.SetActive(true);
        statsMenu = GameObject.Find("StatMenu Variant");
        statsMenu.SetActive(false);
    }

    public void DisplayStatsMenu()
    {
        statsMenu = GameObject.Find("Menu Buttons");
        statsMenu.SetActive(true);

        gameMenu = GameObject.Find("StatMenu Variant");
        gameMenu.SetActive(false);
    }

    // sign out users from game 
    public void SignOut()
    {
        auth.SignOut();
    }
    //start game 
    public void startGAME()
    {
        SceneManager.LoadScene("Game Scene");
    }

    // directs users to game scene 
    public void PlayGame()
    {
        SceneManager.LoadScene("Main scene");
    }
    
    // quiz scene 
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
