namespace ET.Client
{
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.WindowID_Forge)]
	public  class DlgForgeEventHandler : IAUIEventHandler
	{

		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.windowType = UIWindowType.PopUp; 
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.AddComponent<DlgForge>().AddComponent<DlgForgeViewComponent>();
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
		  uiBaseWindow.GetComponent<DlgForge>().RegisterUIEvent(); 
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
		{
		  uiBaseWindow.GetComponent<DlgForge>().ShowWindow(contextData); 
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{
		}

	}
}
