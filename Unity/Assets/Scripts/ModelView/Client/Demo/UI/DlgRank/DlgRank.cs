using System.Collections.Generic;

namespace ET.Client
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgRank :Entity,IAwake,IUILogic
	{

		public DlgRankViewComponent View { get => this.GetComponent<DlgRankViewComponent>();}

		public Dictionary<int, EntityRef<Scroll_Item_rank>> scrollItemRanks;

		public long rankTime;
	}
}
