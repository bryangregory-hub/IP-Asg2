using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;

public class PlayerStats
{
    public string username;
    public int totalTimeSpent;
    public int highScore;
    public long updateOn;
    public long createdOn;
    public int gamesPlayed;

    public PlayerStats()
    { 

    }

    public PlayerStats(string username, int highScore, int gamesPlayed, int totalTimeSpent = 0)
    {
        this.username = username;   
        this.highScore = highScore;
        this.totalTimeSpent = totalTimeSpent;
        this.gamesPlayed = gamesPlayed++;

        var timestamp = this.GetTimeUnix();
        this.updateOn = timestamp;
        this.createdOn = timestamp;
    }

    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }


    public string PlayerStatsToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
