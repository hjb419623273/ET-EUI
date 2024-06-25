using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(DlgServer))]
    [FriendOfAttribute(typeof(ET.Client.ClientServerInfosComponent))]
    [FriendOfAttribute(typeof(ET.ServerInfo))]
    public static class DlgServerSystem
    {

        public static void RegisterUIEvent(this DlgServer self)
        {
            self.View.EButton_EnterButton.AddListenerAsync(() => { return self.OnConfirmClickHandler(); });
            self.View.ELoopScrollList_SeverListLoopVerticalScrollRect.AddItemRefreshListener((Transform transform, int index) => { self.OnScrollItemRefreshHandler(transform, index); });
        }

        public static void ShowWindow(this DlgServer self, Entity contextData = null)
        {
            int count = self.Root().GetComponent<ClientServerInfosComponent>().ServerInfoList.Count;
            self.AddUIScrollItems(ref self.scrollServerItems, count);
            self.View.ELoopScrollList_SeverListLoopVerticalScrollRect.SetVisible(true, count);
        }

        public static void HideWindow(this DlgServer self)
        {
            self.RemoveUIScrollItems(ref self.scrollServerItems);
        }

        public static void OnScrollItemRefreshHandler(this DlgServer self, Transform transform, int index)
        {
            Scroll_Item_ServerInfo serverItem = self.scrollServerItems[index];
            serverItem.BindTrans(transform);
            ServerInfo info = self.Root().GetComponent<ClientServerInfosComponent>().ServerInfoList[index];
            serverItem.E_bgImage.color = info.Id == self.Root().GetComponent<ClientServerInfosComponent>().CurrentServerId ? Color.red : Color.yellow;
            serverItem.ELabel_NumText.SetText(info.Id.ToString());
            serverItem.ELabel_NameText.SetText(info.ServerName);
            serverItem.EButton_SelectButton.AddListener(() => { self.OnSelectServerItemHandler(info.Id);});
        }

        public static void OnSelectServerItemHandler(this DlgServer self, long serverId)
        {
            self.Root().GetComponent<ClientServerInfosComponent>().CurrentServerId = int.Parse(serverId.ToString());
            Log.Debug($"当前选择的服务器 Id 是: {serverId}");
            self.View.ELoopScrollList_SeverListLoopVerticalScrollRect.RefillCells();
        }

        //点击进入
        public static async ETTask OnConfirmClickHandler(this DlgServer self)
        {
            bool isSelect = self.Root().GetComponent<ClientServerInfosComponent>().CurrentServerId != 0;

            if (!isSelect)
            {
                Log.Error("未选择区服");
                return;
            }

            try
            {
                int errorCode = await LoginHelper.GetRoles(self.Root());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                self.Root().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Role);
                self.Root().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Server);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}
