using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(DlgBag))]
    [FriendOfAttribute(typeof(ET.Client.BagComponent))]
    public static class DlgBagSystem
    {

        public static void RegisterUIEvent(this DlgBag self)
        {
            self.RegisterCloseEvent<DlgBag>(self.View.E_CloseButton);
            self.View.E_TopButtonToggleGroup.AddListener(self.OnTopToggleSelectedHandler);
            self.View.E_PreviousButton.AddListener(self.OnPreviousPageHandler);
            self.View.E_NextButton.AddListener(self.OnNextPageHandler);
            self.View.E_BagItemsLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopItemRefreshHandler);
        }

        public static void ShowWindow(this DlgBag self, Entity contextData = null)
        {
            self.View.E_WeaponToggle.IsSelected(true);
        }

        public static void HideWindow(this DlgBag self)
        {
            self.RemoveUIScrollItems(ref self.ScrollItemBagItems);
        }

        public static void Refresh(this DlgBag self)
        {
            self.RefreshItems();
            self.RefeshPageIndexInfo();
        }

        public static void RefreshItems(this DlgBag self)
        {
            self.Root().GetComponent<BagComponent>().ItemsMap.TryGetValue((int)self.CurrentItemType, out List<EntityRef<Item>> itemList);

            int showCount = itemList == null ? 0 : itemList.Count - (self.CurrentPageIndex * 30);
            showCount = showCount > 30 ? 30 : showCount;
            self.AddUIScrollItems(ref self.ScrollItemBagItems, showCount);
            self.View.E_BagItemsLoopVerticalScrollRect.SetVisible(true, showCount);   
        }

        public static void RefeshPageIndexInfo(this DlgBag self)
        {
            int itemCount = self.Root().GetComponent<BagComponent>().GetItemCountByItemType(self.CurrentItemType);
            int maxShowCount = (self.CurrentPageIndex * 30) + 30;
            self.View.E_PreviousButton.interactable = self.CurrentPageIndex != 0;
            self.View.E_NextButton.interactable = itemCount > maxShowCount;

            int maxPageIndex = Mathf.CeilToInt(itemCount / 30.0f);
            self.View.E_PageText.text = $"{self.CurrentPageIndex + 1} / {maxPageIndex}";
        }

        public static void OnTopToggleSelectedHandler(this DlgBag self, int index)
        {
            self.CurrentItemType = (ItemType)index;
            self.CurrentPageIndex = 0;
            self.Refresh();
        }

        public static void OnLoopItemRefreshHandler(this DlgBag self, Transform transform, int index)
        {
            self.Root().GetComponent<BagComponent>().ItemsMap.TryGetValue((int)self.CurrentItemType, out List<EntityRef<Item>> itemList);
            Scroll_Item_bagItem scrollItemBagItem = self.ScrollItemBagItems[index].BindTrans(transform);

            index = (self.CurrentPageIndex * 30) + index;
            Item ent = itemList[index];
            scrollItemBagItem.Refresh(ent.Id);
        }

        public static void OnNextPageHandler(this DlgBag self)
        {
            ++self.CurrentPageIndex;
            self.Refresh();
        }

        public static void OnPreviousPageHandler(this DlgBag self)
        {
            --self.CurrentPageIndex;
            self.Refresh();
        }
    }
}
