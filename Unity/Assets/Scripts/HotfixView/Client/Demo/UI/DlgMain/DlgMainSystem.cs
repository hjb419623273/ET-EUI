using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgMain))]
	public static  class DlgMainSystem
	{

		public static void RegisterUIEvent(this DlgMain self)
		{
			self.View.E_RoleInfoButtonButton.AddListenerAsync(() =>
			{
				return self.OnRoleButtonClickHandler();
			});
			
			self.View.E_TaskButtonButton.AddListenerAsync(() =>
			{
				return self.OnAdventrureButtonClickHandler();
			});
			
			self.View.E_BagButtonButton.AddListenerAsync(() =>
			{
				return self.OnBagButtonClickHandler();
			});
			
			self.View.E_ForgeButtonButton.AddListenerAsync(() =>
			{
				return self.OnForgeButtonClickHandler();
			});

			self.View.E_TasksButtonButton.AddListenerAsync(() =>
			{
				return self.OnTaskButtonClickHandler();
			});
			
			self.View.E_RankButtonButton.AddListenerAsync(() =>
			{
				return self.OnRankButtonClickHandler();
			});
			//红点显示
			RedDotHelper.AddRedDotNodeView(self.Root(), "Role",self.View.E_RoleInfoButtonButton.gameObject, Vector3.one, new Vector2(75, 55));
		}

		public static void ShowWindow(this DlgMain self, Entity contextData = null)
		{
			self.Refresh().Coroutine();
		}

		public static void OnUnLoadWindow(this DlgMain self)
		{
			RedDotMonoView redDotMonoView = self.View.E_RoleInfoButtonButton.GetComponent<RedDotMonoView>();
			RedDotHelper.RemoveRedDotView(self.Root(), "Role", out redDotMonoView);
		}

		public static async ETTask Refresh(this DlgMain self)
		{
			Unit unit = UnitHelper.GetMyUnitFromClientScene(self.Root());
			NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
			
			self.View.E_Label_LvText.SetText($"Lv.{numericComponent.GetAsInt((int)NumericType.Level)}");
			self.View.E_Label_CoinText.SetText($"金币: {numericComponent.GetAsInt((int)NumericType.Coin).ToString()}");
			self.View.E_Label_ExpText.SetText($"经验: {numericComponent.GetAsInt((int)NumericType.Exp).ToString()}");
			await ETTask.CompletedTask;
		}

		public static async ETTask OnRoleButtonClickHandler(this DlgMain self)
		{
			self.Scene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_RoleInfo);
			await ETTask.CompletedTask;
		}
		
		public static async ETTask OnAdventrureButtonClickHandler(this DlgMain self)
		{
			self.Scene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Adventure);
			await ETTask.CompletedTask;
		}
		
		public static async ETTask OnBagButtonClickHandler(this DlgMain self)
		{
			self.Scene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Bag);
			await ETTask.CompletedTask;
		}
		
		public static async ETTask OnForgeButtonClickHandler(this DlgMain self)
		{
			self.Scene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Forge);
			await ETTask.CompletedTask;
		}
		
		public static async ETTask OnTaskButtonClickHandler(this DlgMain self)
		{
			self.Root().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Task);
			await ETTask.CompletedTask;
		}
		
		public static async ETTask OnRankButtonClickHandler(this DlgMain self)
		{
			self.Root().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Rank);
			await ETTask.CompletedTask;
		}
	}
}
