/*
Author: Charlene Soh Jing Ying

Name of Class: FirebaseManager

Description of Class: This class deals with the firebase storing of player statistics into the realtime database in firebase 

Date Created: 5/2/2022
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


public class FirebaseManager : MonoBehaviour
{
    // database caling
    public AuthManager auth; 
    DatabaseReference dbPlayerStatsReference;

    // public variable 
    public string uuid;

    public void Awake()
    {
        // database initialising 
        dbPlayerStatsReference = FirebaseDatabase.DefaultInstance.GetReference("playerStats");
        uuid = auth.GetCurrentUser().UserId;
    }

    // Updating playerstatistics 
    public void UpdatePlayerStats(string uuid, int correct, int accuracy, string displayName )
    {
        Query playerQuery = dbPlayerStatsReference.Child(uuid);

        //read data check entry based on uid
        playerQuery.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            // checking task
            if (task.IsCanceled)
            {
                Debug.LogError("Task is canceled" + task.Exception);
            }
            // checking task
            else if (task.IsFaulted)
            {
                Debug.LogError("Task is faulted" + task.Exception);
            }
            // checking task and completing 
            else if (task.IsCompleted)
            {
                DataSnapshot playerStats = task.Result;
                // check if there is a playerstats
                if (playerStats.Exists)
                {
                    //update
                    //create temp object sp which stores info from player stats
                    QuizManager sp = JsonUtility.FromJson<QuizManager>(playerStats.GetRawJsonValue());
                    //updating all player details
                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.QuizManagerToJson());
                }
                else
                {
                    //create
                    PlayerStats sp = new PlayerStats(displayName, correct, accuracy);

                    //updating multiple values
                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.PlayerStatsToJson());
                }
            }
        });
    }


    // getting playerstatistics from playerstats script 
    public async Task<PlayerStats> GetPlayerStats(string uuid)
    {
        Query q = dbPlayerStatsReference.Child(uuid);
        PlayerStats playerStats = null;

        await dbPlayerStatsReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            // checking task
            if (task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error, Error: " + task.Exception);
            }
            // checking task
            else if (task.IsFaulted)
            {
                Debug.LogError("Sorry, task was faulted, Error: " + task.Exception);
            }

            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;
                

                if (ds.Child(uuid).Exists)
                {
                    playerStats = JsonUtility.FromJson<PlayerStats>(ds.Child(uuid).GetRawJsonValue());
                    Debug.Log("ds....:" + ds.GetRawJsonValue());
                    Debug.Log("player stats values..." + playerStats.PlayerStatsToJson());
                    Debug.Log(playerStats);
                }
            }
        });
        return playerStats;
    }

    // deleting player statistics s
    public void DeletePlayerStats(string uuid)
    {
        dbPlayerStatsReference.Child(uuid).RemoveValueAsync();
    }
}