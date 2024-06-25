
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgMain))]
	[EnableMethod]
	public  class DlgMainViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_BagButtonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagButtonButton == null )
     			{
		    		this.m_E_BagButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"UI/Bottom/E_BagButton");
     			}
     			return this.m_E_BagButtonButton;
     		}
     	}

		public UnityEngine.UI.Image E_BagButtonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagButtonImage == null )
     			{
		    		this.m_E_BagButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"UI/Bottom/E_BagButton");
     			}
     			return this.m_E_BagButtonImage;
     		}
     	}

		public UnityEngine.UI.Button E_ChatButtonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChatButtonButton == null )
     			{
		    		this.m_E_ChatButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"UI/Bottom/E_ChatButton");
     			}
     			return this.m_E_ChatButtonButton;
     		}
     	}

		public UnityEngine.UI.Image E_ChatButtonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChatButtonImage == null )
     			{
		    		this.m_E_ChatButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"UI/Bottom/E_ChatButton");
     			}
     			return this.m_E_ChatButtonImage;
     		}
     	}

		public UnityEngine.UI.Button E_RoleInfoButtonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleInfoButtonButton == null )
     			{
		    		this.m_E_RoleInfoButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"UI/Bottom/E_RoleInfoButton");
     			}
     			return this.m_E_RoleInfoButtonButton;
     		}
     	}

		public UnityEngine.UI.Image E_RoleInfoButtonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoleInfoButtonImage == null )
     			{
		    		this.m_E_RoleInfoButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"UI/Bottom/E_RoleInfoButton");
     			}
     			return this.m_E_RoleInfoButtonImage;
     		}
     	}

		public UnityEngine.UI.Button E_ForgeButtonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ForgeButtonButton == null )
     			{
		    		this.m_E_ForgeButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"UI/Bottom/E_ForgeButton");
     			}
     			return this.m_E_ForgeButtonButton;
     		}
     	}

		public UnityEngine.UI.Image E_ForgeButtonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ForgeButtonImage == null )
     			{
		    		this.m_E_ForgeButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"UI/Bottom/E_ForgeButton");
     			}
     			return this.m_E_ForgeButtonImage;
     		}
     	}

		public UnityEngine.UI.Button E_RankButtonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RankButtonButton == null )
     			{
		    		this.m_E_RankButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"UI/Bottom/E_RankButton");
     			}
     			return this.m_E_RankButtonButton;
     		}
     	}

		public UnityEngine.UI.Image E_RankButtonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RankButtonImage == null )
     			{
		    		this.m_E_RankButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"UI/Bottom/E_RankButton");
     			}
     			return this.m_E_RankButtonImage;
     		}
     	}

		public UnityEngine.UI.Button E_TaskButtonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TaskButtonButton == null )
     			{
		    		this.m_E_TaskButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"UI/Bottom/E_TaskButton");
     			}
     			return this.m_E_TaskButtonButton;
     		}
     	}

		public UnityEngine.UI.Image E_TaskButtonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TaskButtonImage == null )
     			{
		    		this.m_E_TaskButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"UI/Bottom/E_TaskButton");
     			}
     			return this.m_E_TaskButtonImage;
     		}
     	}

		public UnityEngine.UI.Text ELabel_LvText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_LvText == null )
     			{
		    		this.m_ELabel_LvText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"UI/Top/ELabel_Lv");
     			}
     			return this.m_ELabel_LvText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_CoinText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_CoinText == null )
     			{
		    		this.m_ELabel_CoinText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"UI/Top/ELabel_Coin");
     			}
     			return this.m_ELabel_CoinText;
     		}
     	}

		public UnityEngine.UI.Text ELabel_ExpText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_ExpText == null )
     			{
		    		this.m_ELabel_ExpText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"UI/Top/ELabel_Exp");
     			}
     			return this.m_ELabel_ExpText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BagButtonButton = null;
			this.m_E_BagButtonImage = null;
			this.m_E_ChatButtonButton = null;
			this.m_E_ChatButtonImage = null;
			this.m_E_RoleInfoButtonButton = null;
			this.m_E_RoleInfoButtonImage = null;
			this.m_E_ForgeButtonButton = null;
			this.m_E_ForgeButtonImage = null;
			this.m_E_RankButtonButton = null;
			this.m_E_RankButtonImage = null;
			this.m_E_TaskButtonButton = null;
			this.m_E_TaskButtonImage = null;
			this.m_ELabel_LvText = null;
			this.m_ELabel_CoinText = null;
			this.m_ELabel_ExpText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BagButtonButton = null;
		private UnityEngine.UI.Image m_E_BagButtonImage = null;
		private UnityEngine.UI.Button m_E_ChatButtonButton = null;
		private UnityEngine.UI.Image m_E_ChatButtonImage = null;
		private UnityEngine.UI.Button m_E_RoleInfoButtonButton = null;
		private UnityEngine.UI.Image m_E_RoleInfoButtonImage = null;
		private UnityEngine.UI.Button m_E_ForgeButtonButton = null;
		private UnityEngine.UI.Image m_E_ForgeButtonImage = null;
		private UnityEngine.UI.Button m_E_RankButtonButton = null;
		private UnityEngine.UI.Image m_E_RankButtonImage = null;
		private UnityEngine.UI.Button m_E_TaskButtonButton = null;
		private UnityEngine.UI.Image m_E_TaskButtonImage = null;
		private UnityEngine.UI.Text m_ELabel_LvText = null;
		private UnityEngine.UI.Text m_ELabel_CoinText = null;
		private UnityEngine.UI.Text m_ELabel_ExpText = null;
		public Transform uiTransform = null;
	}
}
