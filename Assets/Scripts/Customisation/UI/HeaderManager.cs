using System;
using Customisation.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.UI
{
    public class HeaderManager : MonoBehaviour
    {
        [SerializeField] private UIChannel uiChannel;
        [SerializeField] private Button equipButton;
        [SerializeField] private Button editButton;
        

        private void Awake()
        {
            equipButton.onClick.AddListener(()=>uiChannel.OnItemHolderChange?.Invoke(ItemHolderType.EquipHolder));
            editButton.onClick.AddListener(()=>uiChannel.OnItemHolderChange?.Invoke(ItemHolderType.EditHolder));
        }

        private void OnDisable()
        {
            
        }
    }
}
