using System.Collections;
using System.Collections.Generic;
using Customisation;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterCustomisationUtilities))]
public class CustomizationUtilities : Editor
{
    //   [SerializeField] private List<ItemData> manItemDataList;
    //[SerializeField] private List<ItemData> femaleItemDataList;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CharacterCustomisationUtilities myTarget = (CharacterCustomisationUtilities) target;
        //     
        // if(GUILayout.Button("References"))
        // {
        //     myTarget.SetReferencesSO();
        // }
        //
        // if(GUILayout.Button("Set Hide"))
        // {
        //     myTarget.SetHideElements();
        // }
        if(GUILayout.Button("Set Random Category"))
        {
            myTarget.SetRandomCategory();
        }
        if(GUILayout.Button("Set Unlock By Category"))
        {
            myTarget.SetBuyOption();
        }
        
        
    }
}
