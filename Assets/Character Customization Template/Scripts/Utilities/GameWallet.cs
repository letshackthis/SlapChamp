using System;
using Customisation;
using UnityEngine;

public static class GameWallet
{
    public static Action OnChangeMoney;
    public static Action OnChangeBlueprint;
        
    private static int currentMoney;
    private static int currentBlueprint;

    public static int Money
    {
        get => currentMoney;

        set
        {
            currentMoney = value;
            OnChangeMoney?.Invoke();
            SaveWallet();
        }
    }

        
    public static int Blueprint
    {
        get => currentBlueprint;

        set
        {
            currentBlueprint = value;
            OnChangeBlueprint?.Invoke();
            SaveWallet();
        }
    }

        
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void Initialize()
    {
        currentMoney = ES3.Load(Customisation.SaveKeys.Money,50);
        currentBlueprint = ES3.Load(Customisation.SaveKeys.Blueprint,2);
    }

    public static void SaveWallet()
    {
        ES3.Save(Customisation.SaveKeys.Money,currentMoney);
        ES3.Save(Customisation.SaveKeys.Blueprint,currentBlueprint);
    }
}