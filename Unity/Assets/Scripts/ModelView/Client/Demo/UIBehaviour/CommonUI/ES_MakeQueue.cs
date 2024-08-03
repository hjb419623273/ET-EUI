
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ChildOf]
	[EnableMethod]
	public  class ES_MakeQueue : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy,IUILogic 
	{
		public UnityEngine.UI.Slider E_LeaftTimeSlider
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LeaftTimeSlider == null )
     			{
		    		this.m_E_LeaftTimeSlider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject,"E_LeaftTime");
     			}
     			return this.m_E_LeaftTimeSlider;
     		}
     	}

		public UnityEngine.UI.Text E_MakeTipText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MakeTipText == null )
     			{
		    		this.m_E_MakeTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MakeTip");
     			}
     			return this.m_E_MakeTipText;
     		}
     	}

		public UnityEngine.UI.Text E_MakeTimeText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MakeTimeText == null )
     			{
		    		this.m_E_MakeTimeText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MakeTime");
     			}
     			return this.m_E_MakeTimeText;
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
     			if( this.m_E_ReceiveButton == null )
     			{
		    		this.m_E_ReceiveButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Receive");
     			}
     			return this.m_E_ReceiveButton;
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
     			if( this.m_E_ReceiveImage == null )
     			{
		    		this.m_E_ReceiveImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Receive");
     			}
     			return this.m_E_ReceiveImage;
     		}
     	}

		public UnityEngine.UI.Text E_MakeOverTipText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MakeOverTipText == null )
     			{
		    		this.m_E_MakeOverTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MakeOverTip");
     			}
     			return this.m_E_MakeOverTipText;
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
			this.m_E_LeaftTimeSlider = null;
			this.m_E_MakeTipText = null;
			this.m_E_MakeTimeText = null;
			this.m_E_ReceiveButton = null;
			this.m_E_ReceiveImage = null;
			this.m_E_MakeOverTipText = null;
			this.m_es_equipitem = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Slider m_E_LeaftTimeSlider = null;
		private UnityEngine.UI.Text m_E_MakeTipText = null;
		private UnityEngine.UI.Text m_E_MakeTimeText = null;
		private UnityEngine.UI.Button m_E_ReceiveButton = null;
		private UnityEngine.UI.Image m_E_ReceiveImage = null;
		private UnityEngine.UI.Text m_E_MakeOverTipText = null;
		private EntityRef<ES_EquipItem> m_es_equipitem = null;
		public Transform uiTransform = null;
	}
}
