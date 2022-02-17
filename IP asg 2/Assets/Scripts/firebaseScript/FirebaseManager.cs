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
    public AuthManager auth; 
    DatabaseReference dbPlayerStatsReference;
    public string uuid;

    public void Awake()
    {
        dbPlayerStatsReference = FirebaseDatabase.DefaultInstance.GetReference("playerStats");
        uuid = auth.GetCurrentUser().UserId;
    }

    public void UpdatePlayerStats(string uuid, int correct, int accuracy, string displayName )
    {
        Query playerQuery = dbPlayerStatsReference.Child(uuid);

        //read data check entry based on uid
        playerQuery.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Task is canceled" + task.Exception);
            }

            else if (task.IsFaulted)
            {
                Debug.LogError("Task is faulted" + task.Exception);
            }

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


    
    public async Task<PlayerStats> GetPlayerStats(string uuid)
    {
        Query q = dbPlayerStatsReference.Child(uuid);
        PlayerStats playerStats = null;

        await dbPlayerStatsReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error, Error: " + task.Exception);
            }

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

    
    public void DeletePlayerStats(string uuid)
    {
        dbPlayerStatsReference.Child(uuid).RemoveValueAsync();
    }
}