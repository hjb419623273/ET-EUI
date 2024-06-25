
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgRole))]
	[EnableMethod]
	public  class DlgRoleViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button EButton_CreateButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CreateButton == null )
     			{
		    		this.m_EButton_CreateButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Panel/EButton_Create");
     			}
     			return this.m_EButton_CreateButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_CreateImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_CreateImage == null )
     			{
		    		this.m_EButton_CreateImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Panel/EButton_Create");
     			}
     			return this.m_EButton_CreateImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_DelButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_DelButton == null )
     			{
		    		this.m_EButton_DelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Panel/EButton_Del");
     			}
     			return this.m_EButton_DelButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_DelImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_DelImage == null )
     			{
		    		this.m_EButton_DelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Panel/EButton_Del");
     			}
     			return this.m_EButton_DelImage;
     		}
     	}

		public UnityEngine.UI.Button EButton_EnterButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_EnterButton == null )
     			{
		    		this.m_EButton_EnterButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Panel/EButton_Enter");
     			}
     			return this.m_EButton_EnterButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_EnterImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_EnterImage == null )
     			{
		    		this.m_EButton_EnterImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Panel/EButton_Enter");
     			}
     			return this.m_EButton_EnterImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_RoleLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_RoleLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_RoleLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"Panel/ELoopScrollList_Role");
     			}
     			return this.m_ELoopScrollList_RoleLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.InputField E_InputFieldInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InputFieldInputField == null )
     			{
		    		this.m_E_InputFieldInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"Panel/E_InputField");
     			}
     			return this.m_E_InputFieldInputField;
     		}
     	}

		public UnityEngine.UI.Image E_InputFieldImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InputFieldImage == null )
     			{
		    		this.m_E_InputFieldImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Panel/E_InputField");
     			}
     			return this.m_E_InputFieldImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_CreateButton = null;
			this.m_EButton_CreateImage = null;
			this.m_EButton_DelButton = null;
			this.m_EButton_DelImage = null;
			this.m_EButton_EnterButton = null;
			this.m_EButton_EnterImage = null;
			this.m_ELoopScrollList_RoleLoopVerticalScrollRect = null;
			this.m_E_InputFieldInputField = null;
			this.m_E_InputFieldImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButton_CreateButton = null;
		private UnityEngine.UI.Image m_EButton_CreateImage = null;
		private UnityEngine.UI.Button m_EButton_DelButton = null;
		private UnityEngine.UI.Image m_EButton_DelImage = null;
		private UnityEngine.UI.Button m_EButton_EnterButton = null;
		private UnityEngine.UI.Image m_EButton_EnterImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_RoleLoopVerticalScrollRect = null;
		private UnityEngine.UI.InputField m_E_InputFieldInputField = null;
		private UnityEngine.UI.Image m_E_InputFieldImage = null;
		public Transform uiTransform = null;
	}
}
