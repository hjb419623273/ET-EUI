
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public  class Scroll_Item_entry : Entity,IAwake,IDestroy,IUIScrollItem<Scroll_Item_entry>,IUILogic 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_entry BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Text E_EntryNameText
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
     				if( this.m_E_EntryNameText == null )
     				{
		    			this.m_E_EntryNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_EntryName");
     				}
     				return this.m_E_EntryNameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_EntryName");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_EntryValueText
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
     				if( this.m_E_EntryValueText == null )
     				{
		    			this.m_E_EntryValueText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_EntryValue");
     				}
     				return this.m_E_EntryValueText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_EntryValue");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_EntryNameText = null;
			this.m_E_EntryValueText = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Text m_E_EntryNameText = null;
		private UnityEngine.UI.Text m_E_EntryValueText = null;
		public Transform uiTransform = null;
	}
}
