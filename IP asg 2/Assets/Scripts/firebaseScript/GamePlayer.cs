using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
    
public class GamePlayer
{
    //properties of gameplayer
    public string userName;
    public string displayName;
    public string email;
    public bool active;
    public long lastLoggedIn;
    public long createdOn;
    public long updatedOn;

    public GamePlayer()
    {

    }

    //create new constructor for new player to create character, active is true because they have already created the player
    public GamePlayer(string userName, string displayName, string email, bool active = true)
    {
        this.userName = userName;
        this.displayName = displayName;
        this.email = email;
        this.active = active;

        //timestamp properties
        var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        this.lastLoggedIn = timestamp;
        this.updatedOn = timestamp;
        this.createdOn = timestamp;
    }

    //convert object data to json
    public string GamePlayerToJson()
    {
        return JsonUtility.ToJson(this);
    }


    //return player details
    public string PrintPlayer()
    {
        return string.Format("Player details {0} \n Username: {1} \n Email: {2} \n Active: {3}",
            this.displayName, this.userName, this.email, this.active);
    }
}   

