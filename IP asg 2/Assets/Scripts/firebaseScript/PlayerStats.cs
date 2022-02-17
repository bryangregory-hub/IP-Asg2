/*
Author: Charlene Soh Jing Ying

Name of Class: PlayerStats

Description of Class: This class deals with Player's Statistics and their respective stored variables  

Date Created: 17/2/2022
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;


public class PlayerStats
{
    // variables 
    public string username;
    public int correct;
    public int accuracy;
    public long updateOn;
    public long createdOn;

    // empty constructor 
    public PlayerStats()
    { 

    }

    // defining variables 
    public PlayerStats(string username, int correct, int accuracy )
    {
        this.username = username;   
        this.correct = correct;
        this.accuracy = accuracy;

        // timestamp items 
        var timestamp = this.GetTimeUnix();
        this.updateOn = timestamp;
    }

    // unix conversion for timestamps
    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }

    // Json creation 
    public string PlayerStatsToJson()
    {
        return JsonUtility.ToJson(this);
    }

    // Checking print format 
    public string PrintPlayerStats()
    {
        return string.Format("Player details {0} \n Username: {1} \n Score: {2} \n Accuracy: {3}",
            this.username, this.correct, this.accuracy);
    }
}
