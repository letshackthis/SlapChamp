using System;
using System.Collections.Generic;
using Customisation.SO;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.UI
{
    public class UnlockPanel : MonoBehaviour
    {
        [SerializeField] private CharacterChannel characterChannel;
        [SerializeField] private Button backButton;
        [SerializeField] private Transform content;
        [SerializeField] private UnlockItemOption unlockItemOptionPrefab;
        [SerializeField] private GameObject panel;
       

        private List<UnlockItemOption> unlockItemOptionList = new List<UnlockItemOption>();
        private ItemData currentItemData;
        private void Awake()
        {
            backButton.onClick.AddListener(() =>
            {
                characterChannel.OnUnlockOptionState?.Invoke(false);
            });
            characterChannel.OnUnlockOptionState+= OnUnlockOptionState;
            characterChannel.OnChangeItem+= OnChangeItem;
        }

        private void OnDestroy()
        {
            characterChannel.OnUnlockOptionState-= OnUnlockOptionState;
            characterChannel.OnChangeItem-= OnChangeItem;
        }

        private void OnChangeItem(ItemData itemData)
        {
            currentItemData = itemData;
        }

        private void OnUnlockOptionState(bool state)
        {
            if (state)
            {
                Open();
            }
            panel.SetActive(state);

        }

        private void Open()
        {
            if (unlockItemOptionList.Count > 0)
            {
                foreach (UnlockItemOption unlockItemOption in unlockItemOptionList)
                {
                    Destroy(unlockItemOption.gameObject);
                }
                unlockItemOptionList.Clear(); 
            }
          

            foreach (UnlockItem unlockItem in currentItemData.buyOptions)
            {
                UnlockItemOption currentItemOption = Instantiate(unlockItemOptionPrefab, content);
                unlockItem.Initialize(currentItemData.item.name);
                unlockItem.ShowBuyOption(currentItemOption);
                unlockItemOptionList.Add(currentItemOption);
            }
        }
    }
}
