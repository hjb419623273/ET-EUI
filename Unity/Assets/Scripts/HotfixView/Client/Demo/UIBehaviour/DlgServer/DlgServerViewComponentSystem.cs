
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(DlgServerViewComponent))]
	[FriendOfAttribute(typeof(ET.Client.DlgServerViewComponent))]
	public static partial class DlgServerViewComponentSystem
	{
		[EntitySystem]
		private static void Awake(this DlgServerViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}


		[EntitySystem]
		private static void Destroy(this DlgServerViewComponent self)
		{
			self.DestroyWidget();
		}
	}


}
