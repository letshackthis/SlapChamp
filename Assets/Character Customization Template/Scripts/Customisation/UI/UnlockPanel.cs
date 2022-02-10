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
        [SerializeField] private Text moneyText;
        [SerializeField] private Text bluePrintText;

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
            
            GameWallet.OnChangeBlueprint+= OnChangeBlueprint;
            GameWallet.OnChangeMoney+= OnChangeMoney;
            OnChangeMoney();
            OnChangeBlueprint();
        }

        private void OnChangeMoney()
        {
            moneyText.text = GameWallet.Money.ToString();
        }
        private void OnChangeBlueprint()
        {
            bluePrintText.text = GameWallet.Blueprint.ToString();
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
            foreach (UnlockItemOption unlockItemOption in unlockItemOptionList)
            {
                Destroy(unlockItemOption.gameObject);
            }
            unlockItemOptionList.Clear();

            foreach (UnlockItem unlockItem in currentItemData.buyOptions)
            {
                UnlockItemOption currentItemOption = Instantiate(unlockItemOptionPrefab, content);
                unlockItem.Initialize(currentItemData.item.name);
                unlockItem.ShowBuyOption(currentItemOption);
                unlockItemOptionList.Add(currentItemOption);
            }
        }
        
        private void OnDestroy()
        {
            characterChannel.OnUnlockOptionState-= OnUnlockOptionState;
            GameWallet.OnChangeBlueprint-= OnChangeBlueprint;
            GameWallet.OnChangeMoney-= OnChangeMoney;
        }
    }
}
