using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;

//properties of gameplayer and their statistics
public class PlayerStats
{
    public string username;
    public string displayName;
    public string email;
    public int score;
    public int totalAccuracy;

    public PlayerStats()
    { 

    }

    //create new constructor for new player to create character, active is true because they have already created the player
    public PlayerStats(string username, string displayName, string email, int score, int totalAccuracy)
    {
        this.username = username;
        this.displayName = displayName;
        this.email = email;
        this.score = score;
        this.totalAccuracy = totalAccuracy;
      
    }

    //convert object data to json
    public string PlayerStatsToJson()
    {
        return JsonUtility.ToJson(this);
    }

    //return player details
    public string PrintPlayer()
    {
        return string.Format("Player details {0} \n Username: {1} \n Email: {2} \n Active: {3}",
            this.displayName, this.username, this.email);
    }
}
