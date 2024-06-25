using System.Collections.Generic;

namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgServer :Entity,IAwake,IUILogic
	{

		public DlgServerViewComponent View { get => this.GetComponent<DlgServerViewComponent>();}

		public Dictionary<int, EntityRef<Scroll_Item_ServerInfo>> scrollServerItems = new Dictionary<int, EntityRef<Scroll_Item_ServerInfo>>();
	}
}
