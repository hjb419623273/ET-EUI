
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EnableMethod]
	public  class Scroll_Item_task : Entity,IAwake,IDestroy,IUIScrollItem<Scroll_Item_task>,IUILogic 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_task BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Text E_TaskNameText
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
     				if( this.m_E_TaskNameText == null )
     				{
		    			this.m_E_TaskNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskName");
     				}
     				return this.m_E_TaskNameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskName");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_TaskDescText
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
     				if( this.m_E_TaskDescText == null )
     				{
		    			this.m_E_TaskDescText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskDesc");
     				}
     				return this.m_E_TaskDescText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskDesc");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_TaskProgressTipText
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
     				if( this.m_E_TaskProgressTipText == null )
     				{
		    			this.m_E_TaskProgressTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskProgressTip");
     				}
     				return this.m_E_TaskProgressTipText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskProgressTip");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_TaskProgressText
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
     				if( this.m_E_TaskProgressText == null )
     				{
		    			this.m_E_TaskProgressText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskProgress");
     				}
     				return this.m_E_TaskProgressText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskProgress");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_TaskRewardTipText
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
     				if( this.m_E_TaskRewardTipText == null )
     				{
		    			this.m_E_TaskRewardTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskRewardTip");
     				}
     				return this.m_E_TaskRewardTipText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskRewardTip");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_TaskRewardCountText
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
     				if( this.m_E_TaskRewardCountText == null )
     				{
		    			this.m_E_TaskRewardCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskRewardCount");
     				}
     				return this.m_E_TaskRewardCountText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskRewardCount");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_ReceiveButton
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
     				if( this.m_E_ReceiveButton == null )
     				{
		    			this.m_E_ReceiveButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Receive");
     				}
     				return this.m_E_ReceiveButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Receive");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_ReceiveImage
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
     				if( this.m_E_ReceiveImage == null )
     				{
		    			this.m_E_ReceiveImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Receive");
     				}
     				return this.m_E_ReceiveImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Receive");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_ReceiveTipText
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
     				if( this.m_E_ReceiveTipText == null )
     				{
		    			this.m_E_ReceiveTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Receive/E_ReceiveTip");
     				}
     				return this.m_E_ReceiveTipText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Receive/E_ReceiveTip");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_TaskNameText = null;
			this.m_E_TaskDescText = null;
			this.m_E_TaskProgressTipText = null;
			this.m_E_TaskProgressText = null;
			this.m_E_TaskRewardTipText = null;
			this.m_E_TaskRewardCountText = null;
			this.m_E_ReceiveButton = null;
			this.m_E_ReceiveImage = null;
			this.m_E_ReceiveTipText = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Text m_E_TaskNameText = null;
		private UnityEngine.UI.Text m_E_TaskDescText = null;
		private UnityEngine.UI.Text m_E_TaskProgressTipText = null;
		private UnityEngine.UI.Text m_E_TaskProgressText = null;
		private UnityEngine.UI.Text m_E_TaskRewardTipText = null;
		private UnityEngine.UI.Text m_E_TaskRewardCountText = null;
		private UnityEngine.UI.Button m_E_ReceiveButton = null;
		private UnityEngine.UI.Image m_E_ReceiveImage = null;
		private UnityEngine.UI.Text m_E_ReceiveTipText = null;
		public Transform uiTransform = null;
	}
}
