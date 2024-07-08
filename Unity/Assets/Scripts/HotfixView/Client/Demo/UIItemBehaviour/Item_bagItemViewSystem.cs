
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(Scroll_Item_bagItem))]
	public static partial class Scroll_Item_bagItemViewSystem 
	{
		[EntitySystem]
		private static void Awake(this Scroll_Item_bagItem self )
		{
		}

		[EntitySystem]
		private static void Destroy(this Scroll_Item_bagItem self )
		{
			self.DestroyWidget();
		}
	}
}
