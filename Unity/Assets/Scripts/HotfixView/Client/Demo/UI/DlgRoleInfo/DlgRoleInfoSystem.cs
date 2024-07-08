using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgRoleInfo))]
	public static  class DlgRoleInfoSystem
	{

		public static void RegisterUIEvent(this DlgRoleInfo self)
		{
			//UIHelper中的
			self.RegisterCloseEvent<DlgRoleInfo>(self.View.E_CloseButton);
			
			self.View.ES_AttributeItem.RegisterEvent(NumericType.Power);
			self.View.ES_AttributeItem1.RegisterEvent(NumericType.PhysicalStrength);
			self.View.ES_AttributeItem2.RegisterEvent(NumericType.Agile);
			self.View.ES_AttributeItem3.RegisterEvent(NumericType.Spirit);
			
			self.View.E_AttributesLoopVerticalScrollRect.AddItemRefreshListener((Transform transform, int index) => { self.OnAttributeItemRefreshHandler(transform,index); });
			self.View.E_UpLevelButton.AddListenerAsync(self.OnUpRoleLevelHandler);
			
			RedDotHelper.AddRedDotNodeView(self.Root(), "UpLevelButton", self.View.E_UpLevelButton.gameObject, Vector3.one, new Vector2(115f, 10f));
			RedDotHelper.AddRedDotNodeView(self.Root(), "AddAttribute", self.View.E_AttributePointText.gameObject, new Vector3(0.5f, 0.5f, 1), new Vector2(-17, 10f));
		}

		public static void ShowWindow(this DlgRoleInfo self, Entity contextData = null)
		{
			self.Refresh();
		}

		public static void OnUnLoadWindow(this DlgRoleInfo self)
		{
			RedDotMonoView redDotMonoView = self.View.E_UpLevelButton.gameObject.GetComponent<RedDotMonoView>();
			RedDotHelper.RemoveRedDotView(self.Root(), "UpLevelButton", out redDotMonoView);

			redDotMonoView = self.View.E_AttributePointText.gameObject.GetComponent<RedDotMonoView>();
			RedDotHelper.RemoveRedDotView(self.Root(), "AddAttribute", out redDotMonoView);
		}

		public static void Refresh(this DlgRoleInfo self)
		{
			self.View.ES_AttributeItem.Refresh(NumericType.Power);
			self.View.ES_AttributeItem1.Refresh(NumericType.PhysicalStrength);
			self.View.ES_AttributeItem2.Refresh(NumericType.Agile);
			self.View.ES_AttributeItem3.Refresh(NumericType.Spirit);
			
			NumericComponent numericComponent        = UnitHelper.GetMyUnitFromCurrentScene(self.Root().CurrentScene()).GetComponent<NumericComponent>();
			self.View.E_CombatEffectivenessText.text = "战力值:" + numericComponent.GetAsLong(NumericType.CombatEffectiveness).ToString();
			self.View.E_AttributePointText.text      = numericComponent.GetAsInt(NumericType.AttributePoint).ToString();
			
			int count = PlayerNumericConfigCategory.Instance.GetShowConfigCount();
			self.AddUIScrollItems(ref self.ScrollItemAttributes,count);
			self.View.E_AttributesLoopVerticalScrollRect.SetVisible(true,count);
		}
		
		public static void OnAttributeItemRefreshHandler(this DlgRoleInfo self, Transform transform, int index)
		{
			Scroll_Item_attribute ent = self.ScrollItemAttributes[index];
			Scroll_Item_attribute scrollItemAttribute     = ent.BindTrans(transform);
			PlayerNumericConfig config                    = PlayerNumericConfigCategory.Instance.GetConfigByIndex(index);
			scrollItemAttribute.E_attributeNameText.text  = config.Name + ":";
			Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.Root().CurrentScene());
			NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
			if (config.isPrecent == 0)
			{
				string strValue = numericComponent.GetAsLong(config.Id).ToString();
				scrollItemAttribute.E_attributeValueText.text = strValue;
			}
			else
			{
				string strValue = $"{numericComponent.GetAsFloat(config.Id).ToString("0.00")}%";
				scrollItemAttribute.E_attributeValueText.text = strValue;
			}
			// scrollItemAttribute.E_attributeValueText.text = config.isPrecent == 0? 
			// 		UnitHelper.GetMyUnitNumericComponent(self.Root().CurrentScene()).GetAsLong(config.Id).ToString():
			// 		$"{UnitHelper.GetMyUnitNumericComponent(self.Root().CurrentScene()).GetAsFloat(config.Id).ToString("0.00")}%";
		}

		public static async ETTask OnUpRoleLevelHandler(this DlgRoleInfo self)
		{
			try
			{
				int errorCode = await NumericHelper.RequestUpRoleLevel(self.Root());
				if (errorCode != ErrorCode.ERR_Success)
				{
					return;
				}
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
	}
}
