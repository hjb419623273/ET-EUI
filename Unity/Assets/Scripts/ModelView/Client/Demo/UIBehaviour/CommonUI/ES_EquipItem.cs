
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ChildOf]
	[EnableMethod]
	public  class ES_EquipItem : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy,IUILogic 
	{
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
		    		this.m_E_QualityImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Quality");
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
		    		this.m_E_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Icon");
     			}
     			return this.m_E_IconImage;
     		}
     	}

		public UnityEngine.UI.Button E_SelecteButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SelecteButton == null )
     			{
		    		this.m_E_SelecteButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Selecte");
     			}
     			return this.m_E_SelecteButton;
     		}
     	}

		public UnityEngine.UI.Image E_SelecteImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SelecteImage == null )
     			{
		    		this.m_E_SelecteImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Selecte");
     			}
     			return this.m_E_SelecteImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_QualityImage = null;
			this.m_E_IconImage = null;
			this.m_E_SelecteButton = null;
			this.m_E_SelecteImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_QualityImage = null;
		private UnityEngine.UI.Image m_E_IconImage = null;
		private UnityEngine.UI.Button m_E_SelecteButton = null;
		private UnityEngine.UI.Image m_E_SelecteImage = null;
		public Transform uiTransform = null;
	}
}
