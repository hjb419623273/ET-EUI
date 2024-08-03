using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[Invoke(TimerInvokeType.MakeQueueUI)]
	public class MakeQueneUITimer : ATimer<DlgForge>
	{
		protected override void Run(DlgForge t)
		{
			if (t != null && !t.IsDisposed)
			{
				t?.RefreshMakeQueue();
			}
		}
	}
	
	[FriendOf(typeof(DlgForge))]
	public static  class DlgForgeSystem
	{

		public static void RegisterUIEvent(this DlgForge self)
		{
			self.RegisterCloseEvent<DlgForge>(self.View.E_CloseButton,true);
			self.View.E_ProductionLoopVerticalScrollRect.AddItemRefreshListener(self.OnProductionRefreshHandler);
		}

		public static void ShowWindow(this DlgForge self, Entity contextData = null)
		{
			self.Refresh();
		}

		private static void Refresh(this DlgForge self)
		{
			self.RefreshMakeQueue();
			self.RefreshProduction();
			self.RefreshMaterailCount();
		}

		public static void HideWindow(this DlgForge self)
		{
			self.Root().GetComponent<TimerComponent>().Remove(ref self.MakeQueueTimer);
			self.RemoveUIScrollItems(ref self.ScrollItemProductions);
		}

		public static void RefreshMaterailCount(this DlgForge self)
		{
			NumericComponent numericComponent = UnitHelper.GetMyUnitNumericComponent(self.Root().CurrentScene());
			self.View.E_IronStoneCountText.SetText(numericComponent.GetAsInt(NumericType.IronStone).ToString());
			self.View.E_FurCountText.SetText(numericComponent.GetAsInt(NumericType.Fur).ToString());
		}

		public static void RefreshMakeQueue(this DlgForge self)
		{
			Production production = self.Root().GetComponent<ForgeComponent>().GetProductionByIndex(0);
			self.View.ES_MakeQueueOne.Refresh(production);
			
			production = self.Root().GetComponent<ForgeComponent>().GetProductionByIndex(1);
			self.View.ES_MakeQueueTwo.Refresh(production);
            
			self.Root().GetComponent<TimerComponent>().Remove(ref self.MakeQueueTimer);
            
			int count = self.Root().GetComponent<ForgeComponent>().GetMakeingProductionQueueCount();
			if (count > 0)
			{
				self.Root().GetComponent<TimerComponent>().NewOnceTimer(TimeInfo.Instance.ServerNow() + 1000,TimerInvokeType.MakeQueueUI,self);
			}
		}

		public static void RefreshProduction(this DlgForge self)
		{
			NumericComponent numericComponent = UnitHelper.GetMyUnitNumericComponent(self.Root().CurrentScene());
			int unitLevel = numericComponent.GetAsInt(NumericType.Level);
			int count = ForgeProductionConfigCategory.Instance.GetProductionConfigCount(unitLevel);
			self.AddUIScrollItems(ref self.ScrollItemProductions, count);
			self.View.E_ProductionLoopVerticalScrollRect.SetVisible(true, count);
		}

		//刷新
		public static void OnProductionRefreshHandler(this DlgForge self, Transform transform, int index)
		{
			Scroll_Item_production scrollItemProduction = self.ScrollItemProductions[index].BindTrans(transform);
			NumericComponent numericComponent = UnitHelper.GetMyUnitNumericComponent(self.Root().CurrentScene());
			int unitLevel = numericComponent.GetAsInt(NumericType.Level);
			var config = ForgeProductionConfigCategory.Instance.GetProductionByLevelIndex(unitLevel, index);
			
			scrollItemProduction.ES_EquipItem.RefreshShowItem(config.ItemConfigId);
            scrollItemProduction.E_ItemNameText.SetText(ItemConfigCategory.Instance.Get(config.ItemConfigId).Name);
            scrollItemProduction.E_ConsumeTypeText.SetText(config.ConsumId == NumericType.IronStone ? "精铁:" : "皮革:" );
			scrollItemProduction.E_ConsumeCountText.SetText(config.ConsumeCount.ToString());

			//材料数量
			int materialCount = numericComponent.GetAsInt(config.ConsumId);
			scrollItemProduction.E_MakeButton.interactable = materialCount >= config.ConsumeCount;
			scrollItemProduction.E_MakeButton.AddListenerAsync(() => { return self.OnStartProductionHandler(config.Id);});
		}

		
		//生产事件
		public static async ETTask OnStartProductionHandler(this DlgForge self, int productionConfigId)
		{
			try
			{
				int errorCode = await ForgeHelper.StartProduction(self.Root(), productionConfigId);
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
