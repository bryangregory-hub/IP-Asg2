/*
Author: Charlene Soh Jing Ying

Name of Class: PlayerSatsManager

Description of Class: This class deals with the player statistics shows in unity panels 

Date Created: 17/2/2022
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;


public class PlayerStatsManager : MonoBehaviour
{

    // text mesh pro objects identifiers
    public TextMeshProUGUI correct;
    public TextMeshProUGUI accuracy;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI updatedOn;

    //firebase managers
    public AuthManager authMgr;
    public FirebaseManager firebaseMgr;

    // Start is called before the first frame update
    void Start()
    {
        //to call up reset UI function 
        ResetUI();
        // retrieve current logged in user uuid
        UpdatePlayerStats(authMgr.GetCurrentUser().UserId);
    }

    // to update player statistics 
    public async void UpdatePlayerStats(string uuid)
    {
        PlayerStats playerStats = await firebaseMgr.GetPlayerStats(uuid);

        // checking in there si a pplayer statistics 
        if (playerStats != null)
        {
            Debug.Log("playerstats.......:" + playerStats.PlayerStatsToJson());
            correct.text = playerStats.correct.ToString();
            accuracy.text = playerStats.accuracy.ToString();
            updatedOn.text = UnixToDateTime(playerStats.updateOn);
        }
        else
        {
            // calling reset ui function 
            ResetUI();
        }

        playerName.text = authMgr.GetCurrentUserDisplayName();

    }

    // resetting ui back to nil 
    public void ResetUI()
    {
        correct.text = "0";
        accuracy.text = "0";
        updatedOn.text = "NA";
    }
    // deleting player statistics manually 
    public void DeletePlayerStats()
    {
        firebaseMgr.DeletePlayerStats(authMgr.GetCurrentUser().UserId);

        UpdatePlayerStats(authMgr.GetCurrentUser().UserId);
    }
    // Unix time stamp counter
    public string UnixToDateTime(long timestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
        DateTime dateTime = dateTimeOffset.LocalDateTime;

        return dateTime.ToString("dd MMMM yyyy");
            
    }

}
