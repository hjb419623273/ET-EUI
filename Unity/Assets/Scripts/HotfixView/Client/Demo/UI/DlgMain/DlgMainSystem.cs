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
		}

		public static void ShowWindow(this DlgMain self, Entity contextData = null)
		{
			self.Refresh().Coroutine();
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
			try
			{
				int error = await NumericHelper.TestUpdateNumeric(self.Root());
				if (error != ErrorCode.ERR_Success)
				{
					return;
				}
				
				Log.Debug("发送更新属性测试消息成功！");
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
	}
}
