using System.Collections.Generic;

namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgAdventure :Entity,IAwake,IUILogic
	{

		public DlgAdventureViewComponent View { get => this.GetComponent<DlgAdventureViewComponent>();}

		public Dictionary<int, EntityRef<Scroll_Item_battleLevel>> ScrollItemBattleLevels;
	}
}
