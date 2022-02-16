using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;

public class FirebaseManager : MonoBehaviour
{
    DatabaseReference dbPlayerStatsReference;
    DatabaseReference dbLeaderboardReference;

    public void Awake()
    {
        dbPlayerStatsReference = FirebaseDatabase.DefaultInstance.GetReference("playerStats");
        dbLeaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("leaderboards");
    }

    public void UpdatePlayerStats(string uuid, int score, string displayName, int gamesPlayed, int time)
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
                if (playerStats.Exists)
                {
                    //update
                    //create temp object sp which stores info from player stats
                    PlayerStats sp = JsonUtility.FromJson<PlayerStats>(playerStats.GetRawJsonValue());
                    sp.updateOn = sp.GetTimeUnix();

                    //update leaderboard if new highscore
                    if (score > sp.highScore)
                    {
                        sp.highScore = score;
                        UpdatePlayerLeaderboardEntry(uuid, sp.highScore, sp.updateOn);
                    }

                    //updating all player details
                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.PlayerStatsToJson());
                }
                else
                {
                    //create
                    //PlayerStats sp = new PlayerStats(displayName, score, accuracy);///
                    //Leaderboard lb = new Leaderboard(displayName, score);

                    //updating multiple values
                    //dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.PlayerStatsToJson());///
                    //dbLeaderboardReference.Child(uuid).SetRawJsonValueAsync(lb.SimpleLeaderboardToJson());
                }
            }
        });
    }

    public void UpdatePlayerLeaderboardEntry(string uuid, int highScore, long updateOn)
    {
        //updating specific single values
        dbLeaderboardReference.Child(uuid).Child("highScore").SetValueAsync(highScore);
        dbLeaderboardReference.Child(uuid).Child("updateOn").SetValueAsync(updateOn);
    }

    /*public async Task<List<Leaderboard>> GetLeaderboard(int limit = 5)
    {
        Query q = dbLeaderboardReference.OrderByChild("score").LimitToLast(limit);
        List<Leaderboard> leaderBoardList = new List<Leaderboard>();

        await q.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Sorry, task got canceled, ERROR:" + task.Exception);
            }

            else if (task.IsFaulted)
            {
                Debug.LogError("Sorry, task got faulted, ERROR: " + task.Exception);
            }

            else if(task.IsCompleted)
            {
                DataSnapshot ds = task.Result;
                if (ds.Exists)
                {
                    int rankCounter = 1;
                    foreach(DataSnapshot d in ds.Children)
                    {
                        Leaderboard lb = JsonUtility.FromJson<Leaderboard>(d.GetRawJsonValue());

                        leaderBoardList.Add(lb);
                    }

                    leaderBoardList.Reverse();
                    foreach (Leaderboard lb in leaderBoardList)
                    { 
                        Debug.LogFormat("Leaderboard: Rank{0} Playername {1} Score{2}", rankCounter, lb.username, lb.highScore);
                        rankCounter++;
                    }
                }
            }
        });

        return leaderBoardList;
    }*/

    /*
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
                }
            }
        });
        return playerStats;
    }

    /*
    public void DeletePlayerStats(string uuid)
    {
        dbPlayerStatsReference.Child(uuid).RemoveValueAsync();
        dbLeaderboardReference.Child(uuid).RemoveValueAsync();
    }*/
}