
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[ComponentOf(typeof(DlgServer))]
	[EnableMethod]
	public  class DlgServerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_SeverListLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_SeverListLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_SeverListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"Panel/ELoopScrollList_SeverList");
     			}
     			return this.m_ELoopScrollList_SeverListLoopVerticalScrollRect;
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

		public void DestroyWidget()
		{
			this.m_ELoopScrollList_SeverListLoopVerticalScrollRect = null;
			this.m_EButton_EnterButton = null;
			this.m_EButton_EnterImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_SeverListLoopVerticalScrollRect = null;
		private UnityEngine.UI.Button m_EButton_EnterButton = null;
		private UnityEngine.UI.Image m_EButton_EnterImage = null;
		public Transform uiTransform = null;
	}
}
