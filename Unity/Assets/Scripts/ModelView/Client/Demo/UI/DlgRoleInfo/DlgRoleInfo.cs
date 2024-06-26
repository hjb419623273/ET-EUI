using System.Collections.Generic;

namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgRoleInfo :Entity,IAwake,IUILogic
	{

		public DlgRoleInfoViewComponent View { get => this.GetComponent<DlgRoleInfoViewComponent>();} 
		public Dictionary<int, EntityRef<Scroll_Item_attribute>> ScrollItemAttributes;

	}
}
