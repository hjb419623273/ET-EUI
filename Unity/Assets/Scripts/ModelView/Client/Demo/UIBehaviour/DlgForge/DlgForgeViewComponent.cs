
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgForge))]
	[EnableMethod]
	public  class DlgForgeViewComponent : Entity,IAwake,IDestroy 
	{
		public ES_MakeQueue ES_MakeQueueOne
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_MakeQueue value = this.m_es_makequeueone;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/LayoutGroup/MakeQueueGroup/ES_MakeQueueOne");
		    	   this.m_es_makequeueone = this.AddChild<ES_MakeQueue,Transform>(subTrans,true);
     			}
     			return this.m_es_makequeueone;
     		}
     	}

		public ES_MakeQueue ES_MakeQueueTwo
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			ES_MakeQueue value = this.m_es_makequeuetwo;
     			if( value == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/LayoutGroup/MakeQueueGroup/ES_MakeQueueTwo");
		    	   this.m_es_makequeuetwo = this.AddChild<ES_MakeQueue,Transform>(subTrans,true);
     			}
     			return this.m_es_makequeuetwo;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_ProductionLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ProductionLoopVerticalScrollRect == null )
     			{
		    		this.m_E_ProductionLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"BackGround/LayoutGroup/ProductionGroup/E_Production");
     			}
     			return this.m_E_ProductionLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Text E_IronStoneCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_IronStoneCountText == null )
     			{
		    		this.m_E_IronStoneCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"BackGround/LayoutGroup/BottomGroup/E_IronStoneCount");
     			}
     			return this.m_E_IronStoneCountText;
     		}
     	}

		public UnityEngine.UI.Text E_FurCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FurCountText == null )
     			{
		    		this.m_E_FurCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"BackGround/LayoutGroup/BottomGroup/E_FurCount");
     			}
     			return this.m_E_FurCountText;
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
		    		this.m_E_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BackGround/E_Close");
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
		    		this.m_E_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BackGround/E_Close");
     			}
     			return this.m_E_CloseImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_es_makequeueone = null;
			this.m_es_makequeuetwo = null;
			this.m_E_ProductionLoopVerticalScrollRect = null;
			this.m_E_IronStoneCountText = null;
			this.m_E_FurCountText = null;
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.uiTransform = null;
		}

		private EntityRef<ES_MakeQueue> m_es_makequeueone = null;
		private EntityRef<ES_MakeQueue> m_es_makequeuetwo = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_ProductionLoopVerticalScrollRect = null;
		private UnityEngine.UI.Text m_E_IronStoneCountText = null;
		private UnityEngine.UI.Text m_E_FurCountText = null;
		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		public Transform uiTransform = null;
	}
}
