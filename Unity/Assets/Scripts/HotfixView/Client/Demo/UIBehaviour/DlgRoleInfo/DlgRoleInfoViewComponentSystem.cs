
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(DlgRoleInfoViewComponent))]
	[FriendOfAttribute(typeof(ET.Client.DlgRoleInfoViewComponent))]
	public static partial class DlgRoleInfoViewComponentSystem
	{
		[EntitySystem]
		private static void Awake(this DlgRoleInfoViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}


		[EntitySystem]
		private static void Destroy(this DlgRoleInfoViewComponent self)
		{
			self.DestroyWidget();
		}
	}


}
