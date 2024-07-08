
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(DlgBagViewComponent))]
	[FriendOfAttribute(typeof(ET.Client.DlgBagViewComponent))]
	public static partial class DlgBagViewComponentSystem
	{
		[EntitySystem]
		private static void Awake(this DlgBagViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}


		[EntitySystem]
		private static void Destroy(this DlgBagViewComponent self)
		{
			self.DestroyWidget();
		}
	}


}
