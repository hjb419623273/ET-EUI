
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ChildOf]
	[EnableMethod]
	public  class ES_AttributeItem : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy,IUILogic 
	{
		public UnityEngine.UI.Text EAttributeValueText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EAttributeValueText == null )
     			{
		    		this.m_EAttributeValueText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EAttributeValue");
     			}
     			return this.m_EAttributeValueText;
     		}
     	}

		public UnityEngine.UI.Button E_AddButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddButton == null )
     			{
		    		this.m_E_AddButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Add");
     			}
     			return this.m_E_AddButton;
     		}
     	}

		public UnityEngine.UI.Image E_AddImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddImage == null )
     			{
		    		this.m_E_AddImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Add");
     			}
     			return this.m_E_AddImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EAttributeValueText = null;
			this.m_E_AddButton = null;
			this.m_E_AddImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_EAttributeValueText = null;
		private UnityEngine.UI.Button m_E_AddButton = null;
		private UnityEngine.UI.Image m_E_AddImage = null;
		public Transform uiTransform = null;
	}
}
