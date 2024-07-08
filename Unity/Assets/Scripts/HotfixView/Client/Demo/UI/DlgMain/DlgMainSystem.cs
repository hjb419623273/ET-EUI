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
				return self.OnTaskButtonClickHandler();
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
			
			self.View.ELabel_LvText.SetText($"Lv.{numericComponent.GetAsInt((int)NumericType.Level)}");
			self.View.ELabel_CoinText.SetText($"金币: {numericComponent.GetAsInt((int)NumericType.Coin).ToString()}");
			self.View.ELabel_ExpText.SetText($"经验: {numericComponent.GetAsInt((int)NumericType.Exp).ToString()}");

			await ETTask.CompletedTask;
		}

		public static async ETTask OnRoleButtonClickHandler(this DlgMain self)
		{
			self.Scene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_RoleInfo);
			await ETTask.CompletedTask;
		}
		
		public static async ETTask OnTaskButtonClickHandler(this DlgMain self)
		{
			self.Scene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Adventure);
			await ETTask.CompletedTask;
		}
	}
}
