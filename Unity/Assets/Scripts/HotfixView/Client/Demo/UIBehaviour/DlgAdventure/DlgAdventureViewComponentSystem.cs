
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(DlgAdventureViewComponent))]
	[FriendOfAttribute(typeof(ET.Client.DlgAdventureViewComponent))]
	public static partial class DlgAdventureViewComponentSystem
	{
		[EntitySystem]
		private static void Awake(this DlgAdventureViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}


		[EntitySystem]
		private static void Destroy(this DlgAdventureViewComponent self)
		{
			self.DestroyWidget();
		}
	}


}
