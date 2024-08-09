
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(Scroll_Item_chat))]
	public static partial class Scroll_Item_chatViewSystem 
	{
		[EntitySystem]
		private static void Awake(this Scroll_Item_chat self )
		{
		}

		[EntitySystem]
		private static void Destroy(this Scroll_Item_chat self )
		{
			self.DestroyWidget();
		}
	}
}
