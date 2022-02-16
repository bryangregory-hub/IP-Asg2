using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;

//properties of gameplayer and their statistics
public class PlayerStats
{
    public string email;
    public int score;
    public int totalAccuracy;

    public PlayerStats()
    { 

    }

    //create new constructor for new player to create character, active is true because they have already created the player
    public PlayerStats(string email, int score, int totalAccuracy)
    {
      
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
        return string.Format(" Email: {0} ",
             this.email);
    }
}
