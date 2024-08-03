
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public  class Scroll_Item_production : Entity,IAwake,IDestroy,IUIScrollItem<Scroll_Item_production>,IUILogic 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_production BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			this.ES_EquipItem?.Dispose();
			return this;
		}

		public UnityEngine.UI.Text E_ItemNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_ItemNameText == null )
     				{
		    			this.m_E_ItemNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_ItemName");
     				}
     				return this.m_E_ItemNameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_ItemName");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_MakeButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_MakeButton == null )
     				{
		    			this.m_E_MakeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Make");
     				}
     				return this.m_E_MakeButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Make");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_MakeImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_MakeImage == null )
     				{
		    			this.m_E_MakeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Make");
     				}
     				return this.m_E_MakeImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Make");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_ConsumeTypeText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_ConsumeTypeText == null )
     				{
		    			this.m_E_ConsumeTypeText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ConsumeLayout/E_ConsumeType");
     				}
     				return this.m_E_ConsumeTypeText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ConsumeLayout/E_ConsumeType");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_ConsumeCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_ConsumeCountText == null )
     				{
		    			this.m_E_ConsumeCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ConsumeLayout/E_ConsumeCount");
     				}
     				return this.m_E_ConsumeCountText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ConsumeLayout/E_ConsumeCount");
     			}
     		}
     	}

		public ES_EquipItem ES_EquipItem
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_EquipItem value = this.m_es_equipitem;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ES_EquipItem");
		    	   this.m_es_equipitem = this.AddChild<ES_EquipItem,Transform>(subTrans,true);
     			}
     			return this.m_es_equipitem;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ItemNameText = null;
			this.m_E_MakeButton = null;
			this.m_E_MakeImage = null;
			this.m_E_ConsumeTypeText = null;
			this.m_E_ConsumeCountText = null;
			this.m_es_equipitem = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Text m_E_ItemNameText = null;
		private UnityEngine.UI.Button m_E_MakeButton = null;
		private UnityEngine.UI.Image m_E_MakeImage = null;
		private UnityEngine.UI.Text m_E_ConsumeTypeText = null;
		private UnityEngine.UI.Text m_E_ConsumeCountText = null;
		private EntityRef<ES_EquipItem> m_es_equipitem = null;
		public Transform uiTransform = null;
	}
}
