using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;


public class PlayerStatsManager : MonoBehaviour
{

    public TextMeshProUGUI correct;
    public TextMeshProUGUI accuracy;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI updatedOn;

    public AuthManager authMgr;
    public FirebaseManager firebaseMgr;

    // Start is called before the first frame update
    void Start()
    {
        ResetUI();
        // retrieve current logged in user uuid
        UpdatePlayerStats(authMgr.GetCurrentUser().UserId);
    }

    public async void UpdatePlayerStats(string uuid)
    {
        PlayerStats playerStats = await firebaseMgr.GetPlayerStats(uuid);

        if (playerStats != null)
        {
            Debug.Log("playerstats.......:" + playerStats.PlayerStatsToJson());
            correct.text = playerStats.correct.ToString();
            accuracy.text = playerStats.accuracy.ToString();
            updatedOn.text = UnixToDateTime(playerStats.updateOn);
        }
        else
        {
            ResetUI();
        }

        playerName.text = authMgr.GetCurrentUserDisplayName();

    }

    public void ResetUI()
    {
        correct.text = "0";
        accuracy.text = "0";
        updatedOn.text = "NA";
    }

    public void DeletePlayerStats()
    {
        firebaseMgr.DeletePlayerStats(authMgr.GetCurrentUser().UserId);

        UpdatePlayerStats(authMgr.GetCurrentUser().UserId);
    }
    public string UnixToDateTime(long timestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
        DateTime dateTime = dateTimeOffset.LocalDateTime;

        return dateTime.ToString("dd MMMM yyyy");
            
    }

}
