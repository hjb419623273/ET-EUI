
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(Scroll_Item_rank))]
	public static partial class Scroll_Item_rankViewSystem 
	{
		[EntitySystem]
		private static void Awake(this Scroll_Item_rank self )
		{
		}

		[EntitySystem]
		private static void Destroy(this Scroll_Item_rank self )
		{
			self.DestroyWidget();
		}
	}
}
