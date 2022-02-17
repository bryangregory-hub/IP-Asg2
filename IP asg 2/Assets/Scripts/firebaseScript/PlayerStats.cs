using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;

public class PlayerStats
{
    public string username;
    public int correct;
    public int accuracy;
    public long updateOn;
    public long createdOn;

    public PlayerStats()
    { 

    }

    public PlayerStats(string username, int correct, int accuracy )
    {
        this.username = username;   
        this.correct = correct;
        this.accuracy = accuracy;

        var timestamp = this.GetTimeUnix();
        this.updateOn = timestamp;
    }

    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }


    public string PlayerStatsToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public string PrintPlayerStats()
    {
        return string.Format("Player details {0} \n Username: {1} \n Score: {2} \n Accuracy: {3}",
            this.username, this.correct, this.accuracy);
    }
}
