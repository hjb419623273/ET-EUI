using System.Collections.Generic;

namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgTask :Entity,IAwake,IUILogic
	{
		public DlgTaskViewComponent View { get => this.GetComponent<DlgTaskViewComponent>();} 

		public Dictionary<int, EntityRef<Scroll_Item_task>> ScrollItemTasks;
	}
}
