using System;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class Events : MonoBehaviour
{
    void Start()
    {
        bool registered = ES3.Load("Registered", false);

        if (registered==false)
        {
            registered = true;
            ES3.Load("Registered", true);
            
            ES3.Save("reg_day", DateTime.Now);
        }
        
        int gameStart = ES3.Load(global::SaveKeys.GameStart, 0);
        gameStart++;
        ES3.Save(global::SaveKeys.GameStart, gameStart);
                
        Dictionary<string, object> fields = new Dictionary<string, object>();
        fields.Add("count", gameStart);

        
        GameAnalytics.NewDesignEvent("game_start", fields);
        
        DateTime registerDateTime= ES3.Load("reg_day", DateTime.Now);
        int currentDay =(int) (registerDateTime - DateTime.Now).TotalDays;
        String eventParameters = "{\"session_count\":\""+gameStart+"\", \"current_soft\":\""+GameWallet.Money+"\", \"reg_day\":\""+registerDateTime.ToString("dd/mm/yyyy")+"\", \"days_in_game\":\""+currentDay+"\"}";

        AppMetrica.Instance.ReportEvent("User", eventParameters);
    }
}
