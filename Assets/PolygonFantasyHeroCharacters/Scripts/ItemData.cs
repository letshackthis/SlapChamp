using System;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonFantasyHeroCharacters.Scripts
{
    [Serializable]
    public class ItemTypeData
    {
        public string name;
        public List<ItemData> itemDataList= new List<ItemData>();
    }

    [Serializable]
    public class ItemData
    {
        public string name;
        public GameObject item;
    
        public bool openDefault;
        public bool gamePurchase;
        public bool blueprint;
        public bool realPurchase;

        public string iapKey;
        public int moneyAmount;
        public int blueprintAmount;
    }
}