using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(DlgChat))]
    [FriendOfAttribute(typeof(ET.Client.ChatInfo))]
    public static class DlgChatSystem
    {

        public static void RegisterUIEvent(this DlgChat self)
        {
            self.RegisterCloseEvent<DlgChat>(self.View.E_CloseButton, true);
            self.View.E_SendButton.AddListenerAsync(self.OnSendMessageClickHandler);
            self.View.E_ChatLoopVerticalScrollRect.AddItemRefreshListener(self.OnChatItemLoopHandler);
        }

        public static void ShowWindow(this DlgChat self, Entity contextData = null)
        {
            self.Refresh();self.Refresh();
        }
        
        public static void HideWindow(this DlgChat self)
        {
            self.RemoveUIScrollItems(ref self.ScrollItemChats);
        }

        public static void Refresh(this DlgChat self)
        {
            int count = self.Root().GetComponent<ChatComponent>().GetChatMessageCount();
            self.AddUIScrollItems(ref self.ScrollItemChats,count);
            self.View.E_ChatLoopVerticalScrollRect.SetVisible(true, count);
            self.View.E_ChatLoopVerticalScrollRect.RefillCellsFromEnd();
        }

        public static void OnChatItemLoopHandler(this DlgChat self, Transform transform, int index)
        {
            Scroll_Item_chat scrollItemChat = self.ScrollItemChats[index];
            scrollItemChat = scrollItemChat.BindTrans(transform);
            ChatInfo chatInfo = self.Root().GetComponent<ChatComponent>().GetChatMessageByIndex(index);

            scrollItemChat.E_NameText.SetText(chatInfo.Name + " : ");
            scrollItemChat.E_ChatText.SetText(chatInfo.Message);
        }

        public static async ETTask OnSendMessageClickHandler(this DlgChat self)
        {
            try
            {
                int errorCode = await ChatHelper.SendMessage(self.Root(), self.View.E_MessageInputField.text);
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                self.Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
