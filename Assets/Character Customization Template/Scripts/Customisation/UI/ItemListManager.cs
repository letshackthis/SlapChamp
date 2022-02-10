using System.Collections.Generic;
using System.Linq;
using Customisation.SO;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.UI
{
    public class CellData
    {
        public ItemCell ItemCell;
        public int Order;
    }

    public class ItemListManager : MonoBehaviour
    {
        [SerializeField] private Swiper swiper;
        [SerializeField] private Text nameListText;
        [SerializeField] private ItemListController itemListController;
        [SerializeField] private SelectorCellImage selectorImage;
        [SerializeField] private ItemCell itemCellPrefab;
        [SerializeField] private Transform content;
        [SerializeField] private CharacterChannel characterChannel;
        [SerializeField] private Button attachmentButton;
        [SerializeField] private ButtonHeaderData[] attachmentButtons;
        [SerializeField] private int maxElements = 8;
        [SerializeField] private PointerList pointerList;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private AttachInfoText attachInfoText;
        private List<ItemData> itemList;
        private Sprite[] iconDataList;
        private CellData currentItemCell;
        private int indexSelectedItem;
        private ItemHolderType currentItemHolderType;
        private ItemHolder itemHolder;
        private readonly List<CellData> temporaryItemCellList = new List<CellData>();
        private int indexList;
        private int maxListIndexOrder;
        private bool isAttachmentOpen;

        private void Awake()
        {
            for (int index = 0; index < attachmentButtons.Length; index++)
            {
                Button button = attachmentButtons[index].CurrentButton;
                int index1 = index;
                button.onClick.AddListener(() => OpenAttachmentList(index1));
                button.gameObject.SetActive(false);
            }

            currentItemHolderType = characterChannel.DefaultOpenItemHolderType;
            characterChannel.OnItemListOpen += OnItemListOpen;
            characterChannel.OnItemHolderChange += OnItemHolderChange;
            characterChannel.OnChangeItem += OnChangeItem;
            characterChannel.OnItemListClose += OnItemListClose;
            characterChannel.OnItemHolderSelect += OnItemHolderSelect;
            swiper.OnSwipeHorizontal += OnSwipeHorizontal;
            itemListController.Hide();
            attachmentButton.onClick.AddListener(() => OpenAttachmentList(0));
        }

        private void OnSwipeHorizontal(bool value)
        {
            if (value)
            {
                if (indexList >= maxListIndexOrder) return;
                indexList++;
                ConfigCurrentList();
                pointerList.ActivateCurrentPointer(indexList);
            }
            else
            {
                if (indexList <= 0) return;
                indexList--;
                ConfigCurrentList();
                pointerList.ActivateCurrentPointer(indexList);
            }
        }

        private void OnDisable()
        {
            swiper.OnSwipeHorizontal -= OnSwipeHorizontal;
            characterChannel.OnItemListOpen -= OnItemListOpen;
            characterChannel.OnItemHolderChange -= OnItemHolderChange;
            characterChannel.OnChangeItem -= OnChangeItem;
            characterChannel.OnItemListClose -= OnItemListClose;
            characterChannel.OnItemHolderSelect -= OnItemHolderSelect;
        }

        private void OnItemHolderSelect(ItemHolder obj)
        {
            itemHolder = obj;
            attachmentButton.gameObject.SetActive(itemHolder.AttachmentList.Count > 0);
            isAttachmentOpen = false;
        }


        private void OpenAttachmentList(int index)
        {
            
            if (itemHolder.CurrentHolderData.CurrentItemData != 0)
            {
                isAttachmentOpen = true;
                characterChannel.OnItemListClose?.Invoke();
                itemHolder.SetCurrentAttachment(index);
                HolderData currentAttachment = itemHolder.CurrentAttachmentHolderData;
                CameraViewChanger.OnCameraViewChange?.Invoke(currentAttachment.CurrentItemType);
                characterChannel.OnItemListOpen?.Invoke(currentAttachment);
                attachmentButton.gameObject.SetActive(false);

                ActivateAttachmentButtons(index);
            }
            else
            {
                attachInfoText.Show();
            }
      
        }

        private void OnItemListClose()
        {
            isAttachmentOpen = false;
            DestroyPreviousCells();
            itemListController.Hide();
        }

        private void OnChangeItem(ItemData itemData)
        {
            int newSelectItemIndex = itemList.IndexOf(itemData);
            if (indexSelectedItem != newSelectItemIndex)
            {
                indexSelectedItem = newSelectItemIndex;
                ConfigSelectedItem();
            }
        }

        private void OnItemHolderChange(ItemHolderType obj)
        {
            foreach (ButtonHeaderData button in attachmentButtons)
            {
                button.CurrentButton.gameObject.SetActive(false);
            }


            if (currentItemHolderType == obj) return;
            characterChannel.OnItemListClose?.Invoke();
            currentItemHolderType = obj;
            isAttachmentOpen = false;
            DestroyPreviousCells();
            itemListController.Hide();
        }

        private void ActivateAttachmentButtons(int indexValue)
        {
            if (itemHolder.AttachmentList.Count > 1)
            {
                itemListController.ShowAttachment();
                for (int index = 0; index < attachmentButtons.Length; index++)
                {
                    attachmentButtons[index].TextButton.text = itemHolder.AttachmentList[index].Name;
                    attachmentButtons[index].TextButton.text = itemHolder.AttachmentList[index].Name;
                    attachmentButtons[index].TextButton.color = activeColor;
                    attachmentButtons[index].ImageButton.color = inactiveColor;
                    attachmentButtons[index].CurrentButton.gameObject.SetActive(true);
                }

                attachmentButtons[indexValue].TextButton.color = inactiveColor;
                attachmentButtons[indexValue].ImageButton.color = activeColor;
            }
            else
            {
                foreach (ButtonHeaderData button in attachmentButtons)
                {
                    button.CurrentButton.gameObject.SetActive(false);
                }
            }
        }

        private void OnItemListOpen(HolderData holderData)
        {
            itemList = holderData.ItemList;
            iconDataList = holderData.IconDataList;
            indexSelectedItem = holderData.CurrentItemData;
            nameListText.text = holderData.Name;
            InstantiateItemCells();
            itemListController.Show();

            foreach (ButtonHeaderData button in attachmentButtons)
            {
                button.CurrentButton.gameObject.SetActive(false);
            }
        }

        private void InstantiateItemCells()
        {
            DestroyPreviousCells();
            int indexOrder = 0;
            int itemCellOrder = 0;

            for (int index = 0; index < itemList.Count; index++)
            {
                ItemData item = itemList[index];
                ItemCell newItemCell = Instantiate(itemCellPrefab, content);
                newItemCell.Initialize(item, iconDataList[index]);

                if (indexOrder < maxElements)
                {
                    indexOrder++;
                }
                else
                {
                    indexOrder = 0;
                    itemCellOrder++;
                }

                CellData currentCellData = new CellData()
                {
                    Order = itemCellOrder,
                    ItemCell = newItemCell
                };
                temporaryItemCellList.Add(currentCellData);
            }

            maxListIndexOrder = itemCellOrder;

            ConfigSelectedItem();
            pointerList.Initialize(maxListIndexOrder);
            indexList = temporaryItemCellList[indexSelectedItem].Order;
            ConfigCurrentList();
            pointerList.ActivateCurrentPointer(indexList);
        }

        private void ConfigSelectedItem()
        {
            currentItemCell = temporaryItemCellList[indexSelectedItem];
            selectorImage.SelectCurrentCell(currentItemCell.ItemCell.gameObject);
        }

        private void ConfigCurrentList()
        {
            foreach (CellData cellData in temporaryItemCellList)
            {
                cellData.ItemCell.gameObject.SetActive(cellData.Order == indexList);
            }
        }

        private void DestroyPreviousCells()
        {
            selectorImage.Unselect();
            foreach (CellData cellData in temporaryItemCellList)
            {
                cellData.ItemCell.DestroyCell();
            }

            temporaryItemCellList.Clear();
        }
    }
}