
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgItemPopUp))]
	[EnableMethod]
	public  class DlgItemPopUpViewComponent : Entity,IAwake,IDestroy 
	{
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

		public UnityEngine.UI.Image E_QualityImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_QualityImage == null )
     			{
		    		this.m_E_QualityImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ItemBackGroup/TopGround/E_Quality");
     			}
     			return this.m_E_QualityImage;
     		}
     	}

		public UnityEngine.UI.Image E_IconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_IconImage == null )
     			{
		    		this.m_E_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ItemBackGroup/TopGround/E_Quality/E_Icon");
     			}
     			return this.m_E_IconImage;
     		}
     	}

		public UnityEngine.UI.Text E_NameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NameText == null )
     			{
		    		this.m_E_NameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ItemBackGroup/TopGround/E_Name");
     			}
     			return this.m_E_NameText;
     		}
     	}

		public UnityEngine.UI.Text E_DescText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DescText == null )
     			{
		    		this.m_E_DescText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ItemBackGroup/TopGround/E_Desc");
     			}
     			return this.m_E_DescText;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_EntrysLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EntrysLoopVerticalScrollRect == null )
     			{
		    		this.m_E_EntrysLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"ItemBackGroup/E_Entrys");
     			}
     			return this.m_E_EntrysLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Text E_ScoreText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ScoreText == null )
     			{
		    		this.m_E_ScoreText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ItemBackGroup/BottomGroup/E_Score");
     			}
     			return this.m_E_ScoreText;
     		}
     	}

		public UnityEngine.UI.Text E_PriceText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PriceText == null )
     			{
		    		this.m_E_PriceText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ItemBackGroup/BottomGroup/E_Price");
     			}
     			return this.m_E_PriceText;
     		}
     	}

		public UnityEngine.UI.Button E_EquipButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EquipButton == null )
     			{
		    		this.m_E_EquipButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"ItemBackGroup/ButtonGroup/E_Equip");
     			}
     			return this.m_E_EquipButton;
     		}
     	}

		public UnityEngine.UI.Image E_EquipImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EquipImage == null )
     			{
		    		this.m_E_EquipImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ItemBackGroup/ButtonGroup/E_Equip");
     			}
     			return this.m_E_EquipImage;
     		}
     	}

		public UnityEngine.UI.Button E_UnEquipButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UnEquipButton == null )
     			{
		    		this.m_E_UnEquipButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"ItemBackGroup/ButtonGroup/E_UnEquip");
     			}
     			return this.m_E_UnEquipButton;
     		}
     	}

		public UnityEngine.UI.Image E_UnEquipImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UnEquipImage == null )
     			{
		    		this.m_E_UnEquipImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ItemBackGroup/ButtonGroup/E_UnEquip");
     			}
     			return this.m_E_UnEquipImage;
     		}
     	}

		public UnityEngine.UI.Button E_SellButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SellButton == null )
     			{
		    		this.m_E_SellButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"ItemBackGroup/ButtonGroup/E_Sell");
     			}
     			return this.m_E_SellButton;
     		}
     	}

		public UnityEngine.UI.Image E_SellImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SellImage == null )
     			{
		    		this.m_E_SellImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ItemBackGroup/ButtonGroup/E_Sell");
     			}
     			return this.m_E_SellImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.m_E_QualityImage = null;
			this.m_E_IconImage = null;
			this.m_E_NameText = null;
			this.m_E_DescText = null;
			this.m_E_EntrysLoopVerticalScrollRect = null;
			this.m_E_ScoreText = null;
			this.m_E_PriceText = null;
			this.m_E_EquipButton = null;
			this.m_E_EquipImage = null;
			this.m_E_UnEquipButton = null;
			this.m_E_UnEquipImage = null;
			this.m_E_SellButton = null;
			this.m_E_SellImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		private UnityEngine.UI.Image m_E_QualityImage = null;
		private UnityEngine.UI.Image m_E_IconImage = null;
		private UnityEngine.UI.Text m_E_NameText = null;
		private UnityEngine.UI.Text m_E_DescText = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_EntrysLoopVerticalScrollRect = null;
		private UnityEngine.UI.Text m_E_ScoreText = null;
		private UnityEngine.UI.Text m_E_PriceText = null;
		private UnityEngine.UI.Button m_E_EquipButton = null;
		private UnityEngine.UI.Image m_E_EquipImage = null;
		private UnityEngine.UI.Button m_E_UnEquipButton = null;
		private UnityEngine.UI.Image m_E_UnEquipImage = null;
		private UnityEngine.UI.Button m_E_SellButton = null;
		private UnityEngine.UI.Image m_E_SellImage = null;
		public Transform uiTransform = null;
	}
}
