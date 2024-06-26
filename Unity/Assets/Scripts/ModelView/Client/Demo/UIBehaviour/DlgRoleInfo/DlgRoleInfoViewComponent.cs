
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgRoleInfo))]
	[EnableMethod]
	public  class DlgRoleInfoViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_CombatEffectivenessText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CombatEffectivenessText == null )
     			{
		    		this.m_E_CombatEffectivenessText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"BackGround/TopBackGround/CombatEffectivenessGroup/E_CombatEffectiveness");
     			}
     			return this.m_E_CombatEffectivenessText;
     		}
     	}

		public UnityEngine.UI.Button E_UpLevelButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpLevelButton == null )
     			{
		    		this.m_E_UpLevelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BackGround/TopBackGround/E_UpLevel");
     			}
     			return this.m_E_UpLevelButton;
     		}
     	}

		public UnityEngine.UI.Image E_UpLevelImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpLevelImage == null )
     			{
		    		this.m_E_UpLevelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BackGround/TopBackGround/E_UpLevel");
     			}
     			return this.m_E_UpLevelImage;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Head
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_EquipItem value = this.m_es_equipitem_head;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/TopBackGround/ES_EquipItem_Head");
		    	   this.m_es_equipitem_head = this.AddChild<ES_EquipItem,Transform>(subTrans,true);
     			}
     			return this.m_es_equipitem_head;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Clothes
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_EquipItem value = this.m_es_equipitem_clothes;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/TopBackGround/ES_EquipItem_Clothes");
		    	   this.m_es_equipitem_clothes = this.AddChild<ES_EquipItem,Transform>(subTrans,true);
     			}
     			return this.m_es_equipitem_clothes;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Shoes
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_EquipItem value = this.m_es_equipitem_shoes;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/TopBackGround/ES_EquipItem_Shoes");
		    	   this.m_es_equipitem_shoes = this.AddChild<ES_EquipItem,Transform>(subTrans,true);
     			}
     			return this.m_es_equipitem_shoes;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Ring
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_EquipItem value = this.m_es_equipitem_ring;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/TopBackGround/ES_EquipItem_Ring");
		    	   this.m_es_equipitem_ring = this.AddChild<ES_EquipItem,Transform>(subTrans,true);
     			}
     			return this.m_es_equipitem_ring;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Weapon
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_EquipItem value = this.m_es_equipitem_weapon;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/TopBackGround/ES_EquipItem_Weapon");
		    	   this.m_es_equipitem_weapon = this.AddChild<ES_EquipItem,Transform>(subTrans,true);
     			}
     			return this.m_es_equipitem_weapon;
     		}
     	}

		public ES_EquipItem ES_EquipItem_Shield
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_EquipItem value = this.m_es_equipitem_shield;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/TopBackGround/ES_EquipItem_Shield");
		    	   this.m_es_equipitem_shield = this.AddChild<ES_EquipItem,Transform>(subTrans,true);
     			}
     			return this.m_es_equipitem_shield;
     		}
     	}

		public UnityEngine.UI.Text E_AttributePointText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AttributePointText == null )
     			{
		    		this.m_E_AttributePointText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"BackGround/AttributeBackGround/AttributeAddGroup/E_AttributePoint");
     			}
     			return this.m_E_AttributePointText;
     		}
     	}

		public ES_AttributeItem ES_AttributeItem
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_AttributeItem value = this.m_es_attributeitem;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/AttributeBackGround/AttributeAddGroup/ES_AttributeItem");
		    	   this.m_es_attributeitem = this.AddChild<ES_AttributeItem,Transform>(subTrans,true);
     			}
     			return this.m_es_attributeitem;
     		}
     	}

		public ES_AttributeItem ES_AttributeItem1
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_AttributeItem value = this.m_es_attributeitem1;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/AttributeBackGround/AttributeAddGroup/ES_AttributeItem1");
		    	   this.m_es_attributeitem1 = this.AddChild<ES_AttributeItem,Transform>(subTrans,true);
     			}
     			return this.m_es_attributeitem1;
     		}
     	}

		public ES_AttributeItem ES_AttributeItem2
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_AttributeItem value = this.m_es_attributeitem2;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/AttributeBackGround/AttributeAddGroup/ES_AttributeItem2");
		    	   this.m_es_attributeitem2 = this.AddChild<ES_AttributeItem,Transform>(subTrans,true);
     			}
     			return this.m_es_attributeitem2;
     		}
     	}

		public ES_AttributeItem ES_AttributeItem3
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_AttributeItem value = this.m_es_attributeitem3;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/AttributeBackGround/AttributeAddGroup/ES_AttributeItem3");
		    	   this.m_es_attributeitem3 = this.AddChild<ES_AttributeItem,Transform>(subTrans,true);
     			}
     			return this.m_es_attributeitem3;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_AttributesLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AttributesLoopVerticalScrollRect == null )
     			{
		    		this.m_E_AttributesLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"BackGround/AttributeBackGround/AttributeInfoGroup/E_Attributes");
     			}
     			return this.m_E_AttributesLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Button E_CloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseButton == null )
     			{
		    		this.m_E_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Close");
     			}
     			return this.m_E_CloseButton;
     		}
     	}

		public UnityEngine.UI.Image E_CloseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseImage == null )
     			{
		    		this.m_E_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Close");
     			}
     			return this.m_E_CloseImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CombatEffectivenessText = null;
			this.m_E_UpLevelButton = null;
			this.m_E_UpLevelImage = null;
			this.m_es_equipitem_head = null;
			this.m_es_equipitem_clothes = null;
			this.m_es_equipitem_shoes = null;
			this.m_es_equipitem_ring = null;
			this.m_es_equipitem_weapon = null;
			this.m_es_equipitem_shield = null;
			this.m_E_AttributePointText = null;
			this.m_es_attributeitem = null;
			this.m_es_attributeitem1 = null;
			this.m_es_attributeitem2 = null;
			this.m_es_attributeitem3 = null;
			this.m_E_AttributesLoopVerticalScrollRect = null;
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_CombatEffectivenessText = null;
		private UnityEngine.UI.Button m_E_UpLevelButton = null;
		private UnityEngine.UI.Image m_E_UpLevelImage = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_head = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_clothes = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_shoes = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_ring = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_weapon = null;
		private EntityRef<ES_EquipItem> m_es_equipitem_shield = null;
		private UnityEngine.UI.Text m_E_AttributePointText = null;
		private EntityRef<ES_AttributeItem> m_es_attributeitem = null;
		private EntityRef<ES_AttributeItem> m_es_attributeitem1 = null;
		private EntityRef<ES_AttributeItem> m_es_attributeitem2 = null;
		private EntityRef<ES_AttributeItem> m_es_attributeitem3 = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_AttributesLoopVerticalScrollRect = null;
		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		public Transform uiTransform = null;
	}
}
