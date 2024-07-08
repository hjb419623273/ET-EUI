using System.Collections.Generic;

namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgBag :Entity,IAwake,IUILogic
	{

		public DlgBagViewComponent View { get => this.GetComponent<DlgBagViewComponent>();}

		public Dictionary<int, EntityRef<Scroll_Item_bagItem>> ScrollItemBagItems;

		public ItemType CurrentItemType;

		public int CurrentPageIndex = 0;
	}
}
