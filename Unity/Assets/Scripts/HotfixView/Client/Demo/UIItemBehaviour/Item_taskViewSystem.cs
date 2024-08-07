
using UnityEngine;
using UnityEngine.UI;
namespace ET.Client
{
	[EntitySystemOf(typeof(Scroll_Item_task))]
	public static partial class Scroll_Item_taskViewSystem 
	{
		[EntitySystem]
		private static void Awake(this Scroll_Item_task self )
		{
		}

		[EntitySystem]
		private static void Destroy(this Scroll_Item_task self )
		{
			self.DestroyWidget();
		}
	}
}
