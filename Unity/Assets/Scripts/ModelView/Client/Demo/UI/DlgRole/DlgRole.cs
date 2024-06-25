using System.Collections.Generic;

namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgRole :Entity,IAwake,IUILogic
	{

		public DlgRoleViewComponent View { get => this.GetComponent<DlgRoleViewComponent>();}

		public Dictionary<int, EntityRef<Scroll_Item_role>> roleList = new Dictionary<int, EntityRef<Scroll_Item_role>>();

	}
}
