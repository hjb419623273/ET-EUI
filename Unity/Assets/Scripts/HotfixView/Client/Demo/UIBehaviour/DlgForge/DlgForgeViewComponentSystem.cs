
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(DlgForgeViewComponent))]
	[FriendOfAttribute(typeof(ET.Client.DlgForgeViewComponent))]
	public static partial class DlgForgeViewComponentSystem
	{
		[EntitySystem]
		private static void Awake(this DlgForgeViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}


		[EntitySystem]
		private static void Destroy(this DlgForgeViewComponent self)
		{
			self.DestroyWidget();
		}
	}


}
