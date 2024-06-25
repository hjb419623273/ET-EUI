using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(DlgRole))]
    [FriendOfAttribute(typeof(ET.Client.RoleInfosComponent))]
    [FriendOfAttribute(typeof(ET.RoleInfo))]
    public static class DlgRoleSystem
    {

        public static void RegisterUIEvent(this DlgRole self)
        {
            //创建角色
            self.View.EButton_CreateButton.AddListenerAsync(() => { return self.OnCreateRoleClickHandler();});

            //删除角色
            self.View.EButton_DelButton.AddListenerAsync(() => { return self.OnDeleteRoleClickHandler();});

            // 滚动事件
            self.View.ELoopScrollList_RoleLoopVerticalScrollRect.AddItemRefreshListener((Transform transform,int index) => { self.OnRoleListRefreshHandler(transform,index);});
            
            //进入Game
            self.View.EButton_EnterButton.AddListenerAsync(() => { return self.OnConfirmClickHandler(); });
        }

        public static void ShowWindow(this DlgRole self, Entity contextData = null)
        {
            self.RefreshRoleItems();
        }

        public static void HideWindow(this DlgRole self)
        {
            self.RemoveUIScrollItems(ref self.roleList);
        }

        public static void RefreshRoleItems(this DlgRole self)
        {
            int count = self.Root().GetComponent<RoleInfosComponent>().RoleInfos.Count;
            self.AddUIScrollItems(ref self.roleList, count);
            self.View.ELoopScrollList_RoleLoopVerticalScrollRect.SetVisible(true, count);
        }

        public static void OnRoleListRefreshHandler(this DlgRole self, Transform transform, int index)
        {
            Scroll_Item_role itemRole = self.roleList[index];
            itemRole.BindTrans(transform);

            RoleInfo roleInfo = self.Root().GetComponent<RoleInfosComponent>().RoleInfos[index];
            itemRole.ELabel_NumText.text = (index + 1) + "";
            itemRole.ELabel_RoleNameText.text = roleInfo.Name;
            itemRole.EButton_SelectButton.onClick.AddListener(() =>
            {
                self.Root().GetComponent<RoleInfosComponent>().CurrentRoleId = roleInfo.Id;
                self.View.ELoopScrollList_RoleLoopVerticalScrollRect.RefillCells();
            });

            itemRole.E_BgImage.color = roleInfo.Id == self.Root().GetComponent<RoleInfosComponent>().CurrentRoleId ? Color.red : Color.yellow;
        }

        public static async ETTask OnCreateRoleClickHandler(this DlgRole self)
        {
            string roleName = self.View.E_InputFieldInputField.text.Trim();
            if (string.IsNullOrEmpty(roleName))
            {
                Log.Error("Name is null");
                return;
            }

            try
            {
                int errorCode = await LoginHelper.CreateRole(self.Root(), roleName);
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                self.RefreshRoleItems();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask OnDeleteRoleClickHandler(this DlgRole self)
        {
            if (self.Root().GetComponent<RoleInfosComponent>().CurrentRoleId == 0)
            {
                Log.Error("未选择删除的角色");
                return;
            }
            
            try
            {
                int errorCode = await LoginHelper.DeleteRole(self.Root());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                self.RefreshRoleItems();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask OnConfirmClickHandler(this DlgRole self)
        {
            if (self.Root().GetComponent<RoleInfosComponent>().CurrentRoleId == 0)
            {
                Log.Error("进入游戏前未选择角色");
                return;
            }

            try
            {
                //获取realmKey
                int errorCode = await LoginHelper.GetRealmKey(self.Root());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                
                //EnterGam
                errorCode = await LoginHelper.EnterGame(self.Root());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                
                //self.Root().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Main); //放在SceneChangeFinishEvent_ShowCurrentSceneUI中
                self.Root().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Role);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            await ETTask.CompletedTask;
        }
    }
}
