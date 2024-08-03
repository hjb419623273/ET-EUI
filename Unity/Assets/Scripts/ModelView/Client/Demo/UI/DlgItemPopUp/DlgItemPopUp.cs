using System.Collections.Generic;

namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgItemPopUp :Entity,IAwake,IUILogic
	{
		public DlgItemPopUpViewComponent View { get => this.GetComponent<DlgItemPopUpViewComponent>();}

		public Dictionary<int, EntityRef<Scroll_Item_entry> > ScrollItemEntries;

		public long ItemID = 0;
		public ItemContainerType ItemContainerType = ItemContainerType.Bag;


	}
}
