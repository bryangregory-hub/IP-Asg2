using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;

public class PlayerStats
{
    public string username;
    public int score;
    public int accuracy;
    public long updateOn;
    public long createdOn;
    public int gamesPlayed;

    public PlayerStats()
    { 

    }

    public PlayerStats(string username, int score, int accuracy )
    {
        this.username = username;   
        this.score = score;
        this.accuracy = accuracy;

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
