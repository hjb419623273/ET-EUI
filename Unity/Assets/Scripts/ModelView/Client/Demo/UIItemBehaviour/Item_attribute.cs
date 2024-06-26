
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public  class Scroll_Item_attribute : Entity,IAwake,IDestroy,IUIScrollItem<Scroll_Item_attribute>,IUILogic 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_attribute BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Text E_attributeNameText
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
     				if( this.m_E_attributeNameText == null )
     				{
		    			this.m_E_attributeNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_attributeName");
     				}
     				return this.m_E_attributeNameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_attributeName");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_attributeValueText
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
     				if( this.m_E_attributeValueText == null )
     				{
		    			this.m_E_attributeValueText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_attributeValue");
     				}
     				return this.m_E_attributeValueText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_attributeValue");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_attributeNameText = null;
			this.m_E_attributeValueText = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Text m_E_attributeNameText = null;
		private UnityEngine.UI.Text m_E_attributeValueText = null;
		public Transform uiTransform = null;
	}
}
