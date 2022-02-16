using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;

public class PlayerStats
{
    public string username;
    public int highScore;
    public int accuracy;
    public long updateOn;
    public long createdOn;
<<<<<<< Updated upstream
=======
    public int gamesPlayed;
>>>>>>> Stashed changes

    public PlayerStats()
    { 

    }

    public PlayerStats(string username, int highScore, int accuracy )
    {
        this.username = username;   
        this.highScore = highScore;
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
