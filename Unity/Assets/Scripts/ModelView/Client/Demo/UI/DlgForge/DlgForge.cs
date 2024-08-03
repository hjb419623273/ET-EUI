using System.Collections.Generic;

namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgForge :Entity,IAwake,IUILogic
	{

		public DlgForgeViewComponent View { get => this.GetComponent<DlgForgeViewComponent>();}

		public Dictionary<int, EntityRef<Scroll_Item_production>> ScrollItemProductions;

		public long MakeQueueTimer = 0;

	}
}
