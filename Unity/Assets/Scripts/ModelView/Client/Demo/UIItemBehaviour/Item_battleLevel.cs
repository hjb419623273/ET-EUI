
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public  class Scroll_Item_battleLevel : Entity,IAwake,IDestroy,IUIScrollItem<Scroll_Item_battleLevel>,IUILogic 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_battleLevel BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Text E_LevelNameText
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
     				if( this.m_E_LevelNameText == null )
     				{
		    			this.m_E_LevelNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_LevelName");
     				}
     				return this.m_E_LevelNameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_LevelName");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_GoButton
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
     				if( this.m_E_GoButton == null )
     				{
		    			this.m_E_GoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Go");
     				}
     				return this.m_E_GoButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Go");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_GoImage
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
     				if( this.m_E_GoImage == null )
     				{
		    			this.m_E_GoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Go");
     				}
     				return this.m_E_GoImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Go");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_GoTextText
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
     				if( this.m_E_GoTextText == null )
     				{
		    			this.m_E_GoTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Go/E_GoText");
     				}
     				return this.m_E_GoTextText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Go/E_GoText");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_LevelNotEnoughText
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
     				if( this.m_E_LevelNotEnoughText == null )
     				{
		    			this.m_E_LevelNotEnoughText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_LevelNotEnough");
     				}
     				return this.m_E_LevelNotEnoughText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_LevelNotEnough");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_InAdventureTipText
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
     				if( this.m_E_InAdventureTipText == null )
     				{
		    			this.m_E_InAdventureTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_InAdventureTip");
     				}
     				return this.m_E_InAdventureTipText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_InAdventureTip");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_LevelNameText = null;
			this.m_E_GoButton = null;
			this.m_E_GoImage = null;
			this.m_E_GoTextText = null;
			this.m_E_LevelNotEnoughText = null;
			this.m_E_InAdventureTipText = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Text m_E_LevelNameText = null;
		private UnityEngine.UI.Button m_E_GoButton = null;
		private UnityEngine.UI.Image m_E_GoImage = null;
		private UnityEngine.UI.Text m_E_GoTextText = null;
		private UnityEngine.UI.Text m_E_LevelNotEnoughText = null;
		private UnityEngine.UI.Text m_E_InAdventureTipText = null;
		public Transform uiTransform = null;
	}
}
