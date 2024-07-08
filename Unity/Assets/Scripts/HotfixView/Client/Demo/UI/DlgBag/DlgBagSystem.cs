using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgBag))]
	public static  class DlgBagSystem
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
			
		}
		
		public static void RefeshPageIndexInfo(this DlgBag self)
		{
			
		}
		
		public static void OnTopToggleSelectedHandler(this DlgBag self, int index)
		{
			//self.Cur
		}

		public static void OnLoopItemRefreshHandler(this DlgBag self, Transform transform, int index)
		{
			
		}
		
		public static void OnNextPageHandler(this DlgBag self)
		{
			
		}

		public static void OnPreviousPageHandler(this DlgBag self)
		{
			--self.CurrentPageIndex;
			
		}
	}
}
