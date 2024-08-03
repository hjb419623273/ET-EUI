
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(DlgItemPopUpViewComponent))]
	[FriendOfAttribute(typeof(ET.Client.DlgItemPopUpViewComponent))]
	public static partial class DlgItemPopUpViewComponentSystem
	{
		[EntitySystem]
		private static void Awake(this DlgItemPopUpViewComponent self)
		{
			self.uiTransform = self.Parent.GetParent<UIBaseWindow>().uiTransform;
		}


		[EntitySystem]
		private static void Destroy(this DlgItemPopUpViewComponent self)
		{
			self.DestroyWidget();
		}
	}


}
