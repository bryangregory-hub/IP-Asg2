using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;

//properties of gameplayer and their statistics
public class Player
{
    public string userName;
    public string displayName;
    public string email;
    public long updateOn;
    public long createdOn;

    public Player()
    { 

    }

    //create new constructor for new player to create character, active is true because they have already created the player
    public Player(string userName, string displayName, string email)
    {

        this.userName = userName;
        this.displayName = displayName;
        this.email = email;

        var timestamp = this.GetTimeUnix();
        this.updateOn = timestamp;
        this.createdOn = timestamp;
    }


    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }

    //convert object data to json
    public string PlayerToJson()
    {
        return JsonUtility.ToJson(this);
    }

    //return player details
    public string PrintPlayer()
    {
        return string.Format("Player details {0} \n Username: {1} \n Email: {2}",
            this.displayName, this.userName, this.email);
    }
}
