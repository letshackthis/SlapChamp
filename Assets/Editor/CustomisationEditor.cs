using Customisation;
using UnityEditor;
using UnityEngine;


    [CustomEditor(typeof(CharacterCustomisation))]
    public class CustomisationEditor :Editor
    {
        //   [SerializeField] private List<ItemData> manItemDataList;
        //[SerializeField] private List<ItemData> femaleItemDataList;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CharacterCustomisation myTarget = (CharacterCustomisation) target;
            
            if(GUILayout.Button("Collect Reference"))
            {
                myTarget.CollectReference();
            }
        }

    }

